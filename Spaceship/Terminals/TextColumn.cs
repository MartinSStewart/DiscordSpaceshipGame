using Lens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Terminals
{
    public partial class TextColumn : IRecord
    {
        public ICollection<string> Text { get; private set; } = new string[0];
        public TextAlignment Alignment { get; private set; }
        public Maybe<int> Width { get; private set; }
    }
}
