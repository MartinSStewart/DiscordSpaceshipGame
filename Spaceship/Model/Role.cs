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

namespace Spaceship.Model
{
    public partial class Role : MemberwiseEquatable<Role>, IId<Role>, IRecord
    {
        public Id<Role> Id { get; } = new Id<Role>();
        public string Name { get; private set; } = "";
        public string Description { get; private set; } = "";
        /// <summary>
        /// The maximum number of players who can have this role.
        /// </summary>
        public uint MaxSlots { get; private set; }
        public bool HasChannel => CreateTerminal != null;
        /// <summary>
        /// A function that creates a new terminal for a given player.
        /// </summary>
        public Func<ITextChannel, Id<Player>, Terminal> CreateTerminal { get; private set; }
    }
}
