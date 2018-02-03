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
        public uint MaxSlots { get; private set; }
        public bool HasChannel => Terminal != null;
        public ITerminal Terminal { get; private set; }
    }
}
