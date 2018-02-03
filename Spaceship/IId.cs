using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public interface IId<T>
    {
        Id<T> Id { get; }
    }
}
