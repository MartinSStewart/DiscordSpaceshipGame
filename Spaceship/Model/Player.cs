using Equ;
using FixMath.NET;
using Lens;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Model
{
    public partial class Player : MemberwiseEquatable<Player>, IState, IId<Player>
    {
        public Id<Player> Id { get; } = new Id<Player>();
        public ulong UserId { get; }
        public Id<Role> RoleId { get; private set; }
        public string FirstName { get; private set; } = "Default";
        public string LastName { get; private set; } = "Name";
        public Id<Ship> ShipId { get; private set; }
        public Int2 TerminalSize { get; private set; } = new Int2(63, 28);

        public Player(ulong userId, Id<Role> roleId, Id<Ship> shipId)
        {
            UserId = userId;
            RoleId = roleId;
            ShipId = shipId;
        }

        public Ship GetShip(State state) => state.Ships.FirstById(ShipId);

        public Role GetRole(State state) => state.Roles.FirstById(RoleId);

        public bool IsValid() => 
            TerminalSize.X > 0 && 
            TerminalSize.Y > 0 &&
            TerminalSize.Area <= Constants.MessageMaxCharacters - Constants.TextBoxAffix.Length * 2;
    }
}
