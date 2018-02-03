using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixMath.NET
{
    public struct Int2
    {
        public int X { get; }
        public int Y { get; }

        public Fix Length => Fix.Sqrt(X * X + Y * Y);

        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"{X},{Y}";

        public static Int2 ComponentMin(params Int2[] points) =>
            new Int2(points.Min(item => item.X), points.Min(item => item.Y));

        public static Int2 ComponentMax(params Int2[] points) =>
            new Int2(points.Max(item => item.X), points.Max(item => item.Y));

        public static Int2 operator +(Int2 left, Int2 right) =>
            new Int2(left.X + right.X, left.Y + right.Y);
        public static Int2 operator -(Int2 left, Int2 right) =>
            new Int2(left.X - right.X, left.Y - right.Y);
        public static Int2 operator -(Int2 point) =>
            new Int2(-point.X, -point.Y);
        public static Int2 operator *(Int2 left, Int2 right) =>
            new Int2(left.X * right.X, left.Y * right.Y);
        public static Int2 operator /(Int2 left, Int2 right) =>
            new Int2(left.X / right.X, left.Y / right.Y);
    }
}
