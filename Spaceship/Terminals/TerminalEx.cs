using Discord;
using FixMath.NET;
using Spaceship.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Terminals
{
    public static class TerminalEx
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

        public static void Write(this char[,] charArray, string text, Int2 position)
        {
            if (position.Y >= charArray.GetLength(1))
            {
                var x = position.X;
                var y = position.Y;
                for (int i = 0; i < text.Length; i++)
                {
                    if (x < 0 || x > charArray.GetLength(0) || y < 0)
                    {
                        continue;
                    }
                    if (text[i] == '\n')
                    {
                        x = position.X;
                        y++;
                    }
                    else if (text[i] != '\r')
                    {
                        charArray[x, y] = text[i];
                        x++;
                    }
                }
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
            for (int x = min.X; x < max.X; x++)
            {
                charArray[x, min.Y] = rectangleChar;
                charArray[x, max.Y] = rectangleChar;
            }

            for (int y = min.Y; y < max.Y; y++)
            {
                charArray[min.X, y] = rectangleChar;
                charArray[max.X, y] = rectangleChar;
            }
        }

        private static (Int2 Min, Int2 Max) GetBounds(this char[,] charArray, Int2 start, Int2 end) =>
            (Int2.ComponentMax(
                Int2.ComponentMin(start, end), 
                new Int2()), 
            Int2.ComponentMin(
                Int2.ComponentMax(start, end) + new Int2(1, 1),
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
