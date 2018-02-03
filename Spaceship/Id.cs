using Equ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Id<T> : MemberwiseEquatable<Id<T>>
    {
        readonly Guid? _id = Guid.NewGuid();
    }
}
