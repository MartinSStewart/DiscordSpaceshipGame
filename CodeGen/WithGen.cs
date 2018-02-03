using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using MoreLinq;
using System.IO;
using System;
using Lens;

namespace CodeGen
{
    public static class WithGen
    {
        public static string Generate(string projectPath, string[] structs)
        {
            string RelativePath(string path) => path.Substring(projectPath.Length);

            var files = Directory
                .GetFiles(projectPath, "*.cs", SearchOption.AllDirectories)
                .Where(item =>
                    !new[] { "obj", "bin" }
                        .Contains(RelativePath(item)
                        .Split(new[] { '\\', '/' })[0]) && Path.GetFileName(item) != "RecordWith.cs");

            return files.SelectMany(file =>
            {
                var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));
                var root = (CompilationUnitSyntax)tree.GetRoot();

                const string with = "With";

                return root
                    .DescendantNodes()
                    .OfType<NamespaceDeclarationSyntax>()
                    .SelectMany(@namespace => @namespace
                        .DescendantNodes()
                        .OfType<ClassDeclarationSyntax>()
                        // We skip over any class that has manually implemented a With method.
                        .Where(@class => @class.DescendantNodes().OfType<MethodDeclarationSyntax>().All(item => item.Identifier.Text != with))
                        .Where(@class => @class.BaseList?.Types.Any(item => item.ToString() == nameof(IRecord) || item.ToString() == nameof(IState)) ?? false)
                        .Select(@class =>
                        {
                            var usings = $"{@namespace.Usings}\n{root.Usings}".Replace("\n", "\n    ");

                            // Get all public, non-static, properties with a getter and setter from this class.
                            var properties = @class
                                    .DescendantNodes()
                                    .OfType<PropertyDeclarationSyntax>()
                                    .Where(property => property.Modifiers.Any(item => item.Text == "public") && property.Modifiers.All(item => item.Text != "static"))
                                    .Where(property =>
                                    {
                                        var childNodes = property.AccessorList?.ChildNodes().OfType<AccessorDeclarationSyntax>() ?? new AccessorDeclarationSyntax[0];
                                        return childNodes.Any(accessor => accessor.Keyword.Text == "get") && childNodes.Any(accessor => accessor.Keyword.Text == "set");
                                    })
                                    .ToList();

                            

                            string ParameterName(string propertyName) => char.ToLower(propertyName[0]) + propertyName.Substring(1);

                            var parameters = properties
                                .Select(property =>
                                {
                                    // Figuring out if a type is a struct or class in a more robust manner isn't possible with the syntax tree alone. For now we'll just list all the structs that might be used.
                                    var isStruct = structs.Contains(property.Type.ToString().TakeWhile(item => item != '<').ToDelimitedString(""));
                                    return $"{property.Type}{(isStruct ? "?" : "")} {ParameterName(property.Identifier.Text)} = null";
                                })
                                .ToDelimitedString(", ");

                            const string clone = "clone";

                            var assignments = properties
                                .Select(property =>
                                {
                                    var propertyName = property.Identifier.Text;
                                    return $"{clone}.{propertyName} = {ParameterName(propertyName)} ?? {propertyName};";
                                })
                                .ToDelimitedString("\n            ");

                            var className = @class.Identifier.Text;

                            var validate = "";
                            if (@class.BaseList?.Types.Any(item => item.ToString() == nameof(IState)) ?? false)
                            {
                                validate = 
$@"if (!{clone}.{nameof(IState.IsValid)}())
            {{
                DebugEx.Fail();
            }}
";
                            }

                            return 
$@"namespace {@namespace.Name}
{{
    {usings}

    {@class.Modifiers} class {className}
    {{
        public {className} {with}({parameters})
        {{
            var {clone} = ({className})MemberwiseClone();

            {assignments}

            {validate}
            return {clone};
        }}
    }}
}}";
                        }));
            })
            .ToDelimitedString("\n\n");
        }
    }
}
