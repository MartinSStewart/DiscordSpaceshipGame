using Equ;
using Lens;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Model
{
    public partial class Player : MemberwiseEquatable<Player>, IRecord
    {
        public ulong UserId { get; private set; }
        public Id<Role> RoleId { get; private set; }
        public string FirstName { get; private set; } = "Default";
        public string LastName { get; private set; } = "Name";
        /// <summary>
        /// Channel this player uses as a terminal.
        /// </summary>
        public Maybe<ulong> ChannelId { get; private set; }
        public Id<Ship> ShipId { get; private set; }

        public Player(Id<Role> roleId, Id<Ship> shipId)
        {
            RoleId = roleId;
            ShipId = shipId;
        }

        public Ship GetShip(State state) => 
            state.Collidables.OfType<Ship>().First(item => item.Id == ShipId);

        public Role GetRole(State state) => state.Roles.First(item => item.Id == RoleId);
    }
}
