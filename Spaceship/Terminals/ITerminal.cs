using Discord;
using Spaceship.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Terminals
{
    public interface ITerminal
    {
        State Initialize(State currentState, ITextChannel channel);
        State MessageRecieved(State currentState, IMessage message);
    }
}
