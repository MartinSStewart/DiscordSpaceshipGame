using System;
using System.Collections.Generic;
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
        public static Int2 TerminalSize { get; } = new Int2(70, 30);

        /// <summary>
        /// Width to height ratio of monospaced characters in Discord.
        /// </summary>
        public static Fix CharacterRatio { get; } = 231 / (Fix)480;

        public State Initialize(State currentState, ITextChannel channel)
        {
            channel.SendMessageAsync(@"```
    __  __     __
   / / / /__  / /__ ___  ________ ___  ____ ____
  / /_/ / _ \/ /__ `__ \/ ___/__ `__ \/ __ `/__ \
 / __  /  __/ // / / / (__  )/ / / / / /_/ // / /
/_/ /_/\___/_// /_/ /_/____// /_/ /_/\__,_// /_/
   ______                    _             __
  /_  __/__  _________ ___  (_)___  ____ _/ / TM
   / / / _ \/ ___/ __ `__ \/ / __ \/ __ `/ / 
  / / /  __/ /  / / / / / / / / / / /_/ / / 
 /_/  \___/_/  /_/ /_/ /_/_/_/ /_/\__,_/_/ ```");
            return currentState;
        }

        public State MessageRecieved(State currentState, IMessage message)
        {
            var ui = new char[TerminalSize.X, TerminalSize.Y];
            ui.Fill('.');

            var player = currentState.GetPlayer(message);
            var ship = player.GetShip(currentState);

            ui.Write($"HEADING {ship.Direction}°", new Int2(2, 2));
            ui.Write($"SPEED   {ship.Velocity.Length} m/s", new Int2(2, 4));

            var screenStart = new Int2(2, 6);
            var screenEnd = TerminalSize - new Int2(2, 2);

            ui.DrawRectangle(' ', screenStart, screenEnd);

            message.Channel.SendMessageAsync($"```{ui.GetText()}```");

            var content = message.Content.Trim().ToLower();
            if (content.StartsWith("set heading"))
            {
                //var shipIndex = currentState.Collidables.FindIndex(currentState.PlayerShipId)
                //currentState.Collidables[]
            }
            return currentState;
        }
    }
}
