using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using FixMath.NET;
using Lens;
using Spaceship.Model;

namespace Spaceship.Terminals
{
    public class HelmsmanTerminal : Terminal
    {
        public ImmutableList<Command> Commands { get; private set; }

        public HelmsmanTerminal(ITextChannel channel, Id<Player> playerId) : base(channel, playerId)
        {
            Commands = GetCommands();
        }

        private ImmutableList<Command> GetCommands()
        {
            return new[]
            {
                new Command(
                    "set heading",
                    (state, data) =>
                    {
                        var player = data.Message.GetPlayer(state);
                        var ship = player.GetShip(state);
                        var reply = "";
                        if (Fix.TryParse(data.ContentWithoutPrefix, out Fix heading))
                        {
                            var shipIndex = state.Ships.IndexById(player.ShipId);
                            state = state.Set(p => p.Ships[shipIndex].TargetDirection, heading);

                            var delta = heading - ship.Direction;

                            reply = $"Adjusting {(delta > 0 ? "left" : "right")} by {Fix.Abs(delta)}. Target heading is now {heading}.";
                        }
                        else
                        {
                            reply = "Unable to parse heading. Target heading unchanged.";
                        }

                        var textUi = DrawTextUi(state, reply, data.Message.Content);

                        data.Message.ReplyWith(textUi);

                        return state;
                    }),
                new Command(
                    "set speed",
                    (state, data) =>
                    {
                        var player = data.Message.GetPlayer(state);
                        var ship = player.GetShip(state);
                        var reply = "";
                        if (Fix.TryParse(data.ContentWithoutPrefix, out Fix speed))
                        {
                            var shipIndex = state.Ships.IndexById(player.ShipId);
                            state = state.Set(p => p.Ships[shipIndex].TargetSpeed, speed);

                            var delta = speed - ship.Direction;

                            reply = $"{(delta > 0 ? "Increasing" : "Decreasing")} speed by {Fix.Abs(delta)}. Target speed is now {speed}.";
                        }
                        else
                        {
                            reply = "Unable to parse speed. Target speed unchanged.";
                        }

                        var textUi = DrawTextUi(state, reply, data.Message.Content);

                        data.Message.ReplyWith(textUi);

                        return state;
                    })
            }.ToImmutableList();
        }

        public void ShowSplash(State state)
        {
            var player = state.Players.FirstById(PlayerId);
            var textUi = new char[player.TerminalSize.X, player.TerminalSize.Y];
            textUi.Fill('.');
            var splashText = 
$@"Welcome, {player.FirstName} {player.LastName} to the
..  __  __     __ 
.  / / / /__  / /___ ___  _____ ___ ___  ____ _ ___
  / /_/ / _ \/ / __ `__ \/ ___/ __ `__ \/ __ `/ __ \
 / __  /  __/ / / / / / (__  ) / / / / / /_/ / / / /
/_/ /_/\___/_/_/ /_/ /_/____/_/ /_/ /_/\__,_/_/ /_/
   ______                    _             __
  /_  __/__  _________ ___  (_)___  ____ _/ / TM
.  / / / _ \/ ___/ __ `__ \/ / __ \/ __ `/ / 
  / / /  __/ /  / / / / / / / / / / /_/ / / 
 /_/  \___/_/  /_/ /_/ /_/_/_/ /_/\__,_/_/ ";

            textUi.Write(splashText, new Int2(0, 1));

            textUi.Write("Type \"help\" to get a list of commands.", new Int2(0, player.TerminalSize.Y - 2));

            Channel.SendMessageAsync(Constants.TextBoxAffix + textUi.GetText() + Constants.TextBoxAffix);
        }

        public override State MessageRecieved(State currentState, IMessage message) =>
            Program.ExecuteCommands(Commands, currentState, message);

        public string DrawTextUi(State state, string responseMessage, string message)
        {
            var player = state.Players.FirstById(PlayerId);
            var ship = player.GetShip(state);
            var terminalSize = player.TerminalSize;

            var ui = new char[terminalSize.X, terminalSize.Y];
            ui.Fill('.');

            var screenStart = new Int2(1, 1);
            var screenEnd = terminalSize - new Int2(1, 6);

            ui.DrawRectangle(' ', screenStart, screenEnd);

            var table = new[] {
                new TextColumn().With(new[] { "", "HEADING", "SPEED" }, TextAlignment.Left),
                new TextColumn().With(new[] { "CURRENT", $"{ship.Direction: 0}°", $"{ship.Velocity.Length: 0.#} m/s"}, TextAlignment.Right),
                new TextColumn().With(new[] { "TARGET", $"{ship.TargetDirection: 0}°", $"{ship.TargetSpeed: 0.#} m/s" }, TextAlignment.Right)
            };

            ui.DrawTable(table, new Int2(1, screenEnd.Y + 1));

            return $"```{ui.GetText()}```";
        }

        public override void Update(State newState, State oldState)
        {
            //var newPlayer = newState.Players.First(item => item.ChannelId.Contains(channel.Id));
            //var oldPlayer = oldState.Players.First(item => item.ChannelId.Contains(channel.Id));
            //if (newPlayer.ChannelId != oldPlayer.ChannelId && newPlayer.ChannelId.HasValue)
            //{
            //    //ShowSplash(newState, )
            //}
        }
    }
}
