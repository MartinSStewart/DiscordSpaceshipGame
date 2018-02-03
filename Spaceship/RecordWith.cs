// This code was auto generated.

namespace Spaceship.Model
{
    
    using Equ;
    using Lens;
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class Player
    {
        public Player With(ulong? userId = null, Id<Role> roleId = null, string firstName = null, string lastName = null, Maybe<ulong>? channelId = null, Id<Ship> shipId = null)
        {
            var clone = (Player)MemberwiseClone();

            clone.UserId = userId ?? UserId;
            clone.RoleId = roleId ?? RoleId;
            clone.FirstName = firstName ?? FirstName;
            clone.LastName = lastName ?? LastName;
            clone.ChannelId = channelId ?? ChannelId;
            clone.ShipId = shipId ?? ShipId;

            
            return clone;
        }
    }
}

namespace Spaceship.Model
{
    
    using Equ;
    using Lens;
    using Spaceship.Terminals;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class Role
    {
        public Role With(string name = null, string description = null, uint? maxSlots = null, ITerminal terminal = null)
        {
            var clone = (Role)MemberwiseClone();

            clone.Name = name ?? Name;
            clone.Description = description ?? Description;
            clone.MaxSlots = maxSlots ?? MaxSlots;
            clone.Terminal = terminal ?? Terminal;

            
            return clone;
        }
    }
}

namespace Spaceship.Model
{
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Equ;
    using FixMath.NET;
    using Lens;

    public partial class Ship
    {
        public Ship With(Fix2? position = null, Fix2? velocity = null, Fix? direction = null)
        {
            var clone = (Ship)MemberwiseClone();

            clone.Position = position ?? Position;
            clone.Velocity = velocity ?? Velocity;
            clone.Direction = direction ?? Direction;

            if (!clone.IsValid())
            {
                DebugEx.Fail();
            }

            return clone;
        }
    }
}

namespace Spaceship.Model
{
    
    using Equ;
    using Lens;
    using Spaceship.Terminals;
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class State
    {
        public State With(bool? isStarted = null, ImmutableList<Player> players = null, ImmutableList<Role> roles = null, ImmutableHashSet<ICollidable> collidables = null)
        {
            var clone = (State)MemberwiseClone();

            clone.IsStarted = isStarted ?? IsStarted;
            clone.Players = players ?? Players;
            clone.Roles = roles ?? Roles;
            clone.Collidables = collidables ?? Collidables;

            if (!clone.IsValid())
            {
                DebugEx.Fail();
            }

            return clone;
        }
    }
}