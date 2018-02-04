using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using FixMath.NET;
using Spaceship.Model;

namespace Spaceship.Terminals
{
    public class HelmsmanTerminal : ITerminal
    {
        public ImmutableList<Command> Commands { get; } = new[]
        {
            new Command(
                "set heading",
                (state, data) =>
                {
                    if (Fix.TryParse(data.ContentWithoutPrefix, out Fix heading))
                    {

                    }
                    return state;
                })
        }.ToImmutableList();


        public State Initialize(State currentState, ITextChannel channel)
        {
            channel.SendMessageAsync(@"```
    __  __     __
   / / / /__  / /___ ___  _____ ___ ___  ____ _ ___
  / /_/ / _ \/ / __ `__ \/ ___/ __ `__ \/ __ `/ __ \
 / __  /  __/ / / / / / (__  ) / / / / / /_/ / / / /
/_/ /_/\___/_/_/ /_/ /_/____/_/ /_/ /_/\__,_/_/ /_/
   ______                    _             __
  /_  __/__  _________ ___  (_)___  ____ _/ / TM
   / / / _ \/ ___/ __ `__ \/ / __ \/ __ `/ / 
  / / /  __/ /  / / / / / / / / / / /_/ / / 
 /_/  \___/_/  /_/ /_/ /_/_/_/ /_/\__,_/_/ ```");
            return currentState;
        }

        public State MessageRecieved(State currentState, IMessage message)
        {
            var player = currentState.GetPlayer(message);
            var ship = player.GetShip(currentState);
            var terminalSize = player.TerminalSize;

            var content = message.Content.Trim().ToLower();
            if (content.StartsWith("set heading"))
            {
                //var shipIndex = currentState.Collidables.FindIndex(currentState.PlayerShipId)
                //currentState.Collidables[]
            }

            var ui = new char[terminalSize.X, terminalSize.Y];
            ui.Fill('.');

            var screenStart = new Int2(1, 1);
            var screenEnd = terminalSize - new Int2(1, 6);

            ui.DrawRectangle(' ', screenStart, screenEnd);

            ui.Write($"HEADING {ship.Direction}°", new Int2(2, screenEnd.Y + 1));
            ui.Write($"SPEED   {ship.Velocity.Length} m/s", new Int2(2, screenEnd.Y + 2));
            ui.Write($"> {content}", new Int2(2, screenEnd.Y + 4));

            message.Channel.SendMessageAsync($"```{ui.GetText()}```");

            return currentState;
        }
    }
}
