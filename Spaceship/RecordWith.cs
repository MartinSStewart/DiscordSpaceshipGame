// This code was auto generated.

namespace Spaceship.Model
{
    
    using Equ;
    using FixMath.NET;
    using Lens;
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class Player
    {
        public Player With(Id<Role> roleId = null, string firstName = null, string lastName = null, Id<Ship> shipId = null, Int2? terminalSize = null)
        {
            var clone = (Player)MemberwiseClone();

            clone.RoleId = roleId ?? RoleId;
            clone.FirstName = firstName ?? FirstName;
            clone.LastName = lastName ?? LastName;
            clone.ShipId = shipId ?? ShipId;
            clone.TerminalSize = terminalSize ?? TerminalSize;

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
    
    using Discord;
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
        public Role With(string name = null, string description = null, uint? maxSlots = null, Func<ITextChannel, Id<Player>, Terminal> createTerminal = null)
        {
            var clone = (Role)MemberwiseClone();

            clone.Name = name ?? Name;
            clone.Description = description ?? Description;
            clone.MaxSlots = maxSlots ?? MaxSlots;
            clone.CreateTerminal = createTerminal ?? CreateTerminal;

            
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
        public Ship With(Fix2? position = null, Fix2? velocity = null, Fix? direction = null, Fix? targetDirection = null, Fix? targetSpeed = null, Fix? maxTargetSpeed = null)
        {
            var clone = (Ship)MemberwiseClone();

            clone.Position = position ?? Position;
            clone.Velocity = velocity ?? Velocity;
            clone.Direction = direction ?? Direction;
            clone.TargetDirection = targetDirection ?? TargetDirection;
            clone.TargetSpeed = targetSpeed ?? TargetSpeed;
            clone.MaxTargetSpeed = maxTargetSpeed ?? MaxTargetSpeed;

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
        public State With(bool? isStarted = null, ImmutableList<Player> players = null, ImmutableList<Role> roles = null, ImmutableList<Ship> ships = null, ImmutableList<Terminal> terminals = null)
        {
            var clone = (State)MemberwiseClone();

            clone.IsStarted = isStarted ?? IsStarted;
            clone.Players = players ?? Players;
            clone.Roles = roles ?? Roles;
            clone.Ships = ships ?? Ships;
            clone.Terminals = terminals ?? Terminals;

            if (!clone.IsValid())
            {
                DebugEx.Fail();
            }

            return clone;
        }
    }
}