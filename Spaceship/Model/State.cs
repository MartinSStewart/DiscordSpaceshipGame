using Equ;
using Lens;
using Spaceship.Terminals;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Model
{
    public partial class State : MemberwiseEquatable<State>, IState, IRecord
    {
        public bool IsStarted { get; private set; }
        public ImmutableList<Player> Players { get; private set; } = ImmutableList<Player>.Empty;
        public ImmutableList<Role> Roles { get; private set; } = new[] {
            new Role().With("Spectator", "Just spectating!"),
            new Role().With("Captain", "Issues commands to the rest of the crew.", 1),
            new Role().With("Nuclear Technician", "Maintains the ship's power systems.", 0, (channelId, playerId) => new HelmsmanTerminal(channelId, playerId)),
            new Role().With("Weapons Officer", "Controls the ship's mounted weaponry.", 1, (channelId, playerId) => new HelmsmanTerminal(channelId, playerId)),
            new Role().With("Radar Operator", "Monitors and identifies objects nearby the spaceship.", 1, (channelId, playerId) => new HelmsmanTerminal(channelId, playerId)),
            new Role().With("Helmsman", "Steers the spaceship.", 0, (channelId, playerId) => new HelmsmanTerminal(channelId, playerId))
        }.ToImmutableList();

        public Role DefaultRole => Roles.First();

        public ImmutableList<Ship> Ships { get; private set; } = ImmutableList<Ship>.Empty;
        public ImmutableList<Terminal> Terminals { get; private set; } = ImmutableList<Terminal>.Empty;

        public Id<Ship> DefaultShipId { get; }

        public State()
        {
            var playerShip = new Ship();
            DefaultShipId = playerShip.Id;
            Ships = Ships.Add(playerShip);
        }

        public bool IsValid() =>
            (!IsStarted || Players.Any()) &&
            Players.All(item => Roles.Any(role => role.Id == item.RoleId));
    }
}
