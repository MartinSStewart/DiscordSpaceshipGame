using Discord;
using FixMath.NET;
using Spaceship.Model;
using Spaceship.Terminals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public enum TextAlignment { Left, Right }

    public static class TextUi
    {
        /// <summary>
        /// Iterates through every element of a 2 dimensional array, left to right, top to bottom.
        /// </summary>
        public static IEnumerable<T> ToEnumerable<T>(this T[,] array2)
        {
            for (int x = 0; x < array2.GetLength(0); x++)
            {
                for (int y = 0; y < array2.GetLength(0); y++)
                {
                    yield return array2[x, y];
                }
            }
            yield break;
        }

        /// <summary>
        /// Creates a transposed shallow clone of a 2 dimensional array.
        /// </summary>
        public static T[,] Transpose<T>(this T[,] array2)
        {
            var transpose = new T[array2.GetLength(1), array2.GetLength(0)];
            for (int x = 0; x < array2.GetLength(0); x++)
            {
                for (int y = 0; y < array2.GetLength(1); y++)
                {
                    transpose[y, x] = array2[x, y];
                }
            }
            return transpose;
        }

        public static void Write(this char[,] charArray, string text, Int2 position, TextAlignment alignment = TextAlignment.Left)
        {
            int alignmentOffset(int i) => alignment == TextAlignment.Right
                ? text.Skip(i).TakeWhile(item => item != '\n').Count()
                : 0;

            if (position.Y < charArray.GetLength(1))
            {
                var newText = text.Replace("\r", "");
                var x = position.X - alignmentOffset(0);
                var y = position.Y;
                for (int i = 0; i < text.Length; i++)
                {
                    if (x < 0 || x > charArray.GetLength(0) || y < 0)
                    {
                        continue;
                    }
                    if (text[i] == '\n')
                    {
                        x = position.X - alignmentOffset(i + 1);
                        y++;
                    }
                    else 
                    {
                        charArray[x, y] = text[i];
                        x++;
                    }
                }
            }
        }

        public static void DrawTable(this char[,] charArray, ICollection<TextColumn> columns, Int2 topLeft)
        {
            var xOffset = 0;
            foreach (var column in columns)
            {
                var width = column.Width.ValueOr(column.Text.Max(item => item.Length));
                for (var cellIndex = 0; cellIndex < column.Text.Count; cellIndex++)
                {
                    var cell = column.Text.ElementAt(cellIndex);
                    DebugEx.Assert(cell.All(item => item != '\n'), "Line breaks are not supported yet.");

                    var rightAlignOffset = column.Alignment == TextAlignment.Right 
                        ? width 
                        : 0;
                    charArray.Write(
                        cell, 
                        topLeft + new Int2(xOffset + rightAlignOffset, cellIndex), 
                        column.Alignment);
                }

                xOffset += width + 1;
            }
        }

        public static void Fill(this char[,] charArray, char fillChar) => 
            charArray.DrawRectangle(
                fillChar, 
                new Int2(), 
                new Int2(charArray.GetLength(0), charArray.GetLength(1)));

        public static void DrawLine(this char[,] charArray, string lineTest, Int2 start, Int2 end)
        {
            throw new NotImplementedException();
        }

        public static void DrawRectangle(this char[,] charArray, char rectangleChar, Int2 start, Int2 end)
        {
            var (min, max) = charArray.GetBounds(start, end);
            for (int x = min.X; x < max.X; x++)
            {
                for (int y = min.Y; y < max.Y; y++)
                {
                    charArray[x, y] = rectangleChar;
                }
            }
        }

        public static void DrawRectangleOutline(this char[,] charArray, char rectangleChar, Int2 start, Int2 end)
        {
            var (min, max) = charArray.GetBounds(start, end);

            var outlineMin = Int2.ComponentMin(start, end);
            var outlineMax = Int2.ComponentMax(start, end);

            for (int x = min.X; x < max.X; x++)
            {
                for (int y = min.Y; y < max.Y; y++)
                {
                    if (x == outlineMin.X || x == outlineMax.X - 1 || y == outlineMin.Y || y == outlineMax.Y - 1)
                    {
                        charArray[x, y] = rectangleChar;
                    }
                }
            }
        }

        private static (Int2 Min, Int2 Max) GetBounds(this char[,] charArray, Int2 start, Int2 end) =>
            (Int2.ComponentMax(
                Int2.ComponentMin(start, end), 
                new Int2()), 
            Int2.ComponentMin(
                Int2.ComponentMax(start, end),
                new Int2(
                    charArray.GetLength(0), 
                    charArray.GetLength(1))));


        public static string GetText(this char[,] charArray)
        {
            var stringBuilder = new StringBuilder();
            for (int x = 0; x < charArray.GetLength(1); x++)
            {
                for (int y = 0; y < charArray.GetLength(0); y++)
                {
                    stringBuilder.Append(charArray[y, x]);
                }
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();
        }

        public static Player GetPlayer(this State state, IMessage message) => 
            state.Players.First(item => item.UserId == message.Author.Id);
    }
}
