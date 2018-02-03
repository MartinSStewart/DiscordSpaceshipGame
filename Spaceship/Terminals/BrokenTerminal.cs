using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Spaceship.Model;

namespace Spaceship.Terminals
{
    public class BrokenTerminal : ITerminal
    {
        public State Initialize(State currentState, ITextChannel channel)
        {
            return currentState;
        }

        public State MessageRecieved(State currentState, IMessage message)
        {
            return currentState;
        }
    }
}
