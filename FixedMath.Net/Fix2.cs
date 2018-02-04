using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixMath.NET
{
    public struct Fix2
    {
        public Fix X { get; }
        public Fix Y { get; }

        public Fix Length => Fix.Sqrt(X * X + Y * Y);
        public Fix Area => X * Y;

        public Fix2(Fix x, Fix y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"{X},{Y}";

        public static Fix2 operator +(Fix2 left, Fix2 right) =>
            new Fix2(left.X + right.X, left.Y + right.Y);
        public static Fix2 operator -(Fix2 left, Fix2 right) =>
            new Fix2(left.X - right.X, left.Y - right.Y);
        public static Fix2 operator -(Fix2 point) =>
            new Fix2(-point.X, -point.Y);
        public static Fix2 operator *(Fix2 left, Fix2 right) =>
            new Fix2(left.X * right.X, left.Y * right.Y);
        public static Fix2 operator /(Fix2 left, Fix2 right) =>
            new Fix2(left.X / right.X, left.Y / right.Y);

        public static implicit operator Fix2(Int2 point) =>
            new Fix2(point.X, point.Y);
        public static explicit operator Int2(Fix2 point) =>
            new Int2((int)point.X, (int)point.Y);
    }
}
