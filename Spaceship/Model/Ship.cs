using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equ;
using FixMath.NET;
using Lens;

namespace Spaceship.Model
{
    public partial class Ship : MemberwiseEquatable<Ship>, ICollidable, IState, IId<Ship>
    {
        public Id<Ship> Id { get; } = new Id<Ship>();

        /// <summary>
        /// Ship position in meters relative to map origin.
        /// </summary>
        public Fix2 Position { get; private set; }
        /// <summary>
        /// Ship velocity in meters per second.
        /// </summary>
        public Fix2 Velocity { get; private set; }
        /// <summary>
        /// Ship heading in radians. 0 degrees is north and increasing values move counter clockwise.
        /// </summary>
        public Fix Direction { get; private set; }
        public Fix TargetDirection { get; private set; }
        public Fix TargetSpeed { get; private set; }
        public Fix MaxTargetSpeed { get; private set; }
        /// <summary>
        /// Ship size in meters.
        /// </summary>
        public Fix Size { get; } = 1;

        public bool IsValid() => Size > 0;
    }
}
