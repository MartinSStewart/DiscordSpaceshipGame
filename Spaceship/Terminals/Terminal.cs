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
    public abstract class Terminal
    {
        public ITextChannel Channel { get; }
        public Id<Player> PlayerId { get; }

        public Terminal(ITextChannel channel, Id<Player> playerId)
        {
            DebugEx.Assert(channel != null);
            DebugEx.Assert(playerId != null);
            Channel = channel;
            PlayerId = playerId;
        }

        public abstract State MessageRecieved(State currentState, IMessage message);
        public abstract void Update(State newState, State oldState);
    }
}
