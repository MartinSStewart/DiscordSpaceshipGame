using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var text = "// This code was auto generated.\n\n" + WithGen.Generate(
                args[0], 
                new[] { "bool", "uint", "int", "long", "ulong", "float", "double", "Int32", "UInt32", "Int64", "UInt64", "Float", "Double", "NumRange", "Maybe", "Vector", "Fix", "Fix2", "Int2" });

            File.WriteAllText(args[1], text);
        }
    }
}
