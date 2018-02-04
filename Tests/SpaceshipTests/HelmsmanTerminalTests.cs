using NUnit.Framework;
using Spaceship.Model;
using Spaceship.Terminals;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTests.SpaceshipTests
{
    [TestFixture]
    public class HelmsmanTerminalTests
    {
        MockUser _user;
        MockMessageChannel _channel;
        State _state;
        HelmsmanTerminal _terminal;

        [SetUp]
        public void SetUp()
        {
            _user = new MockUser();

            _channel = new MockMessageChannel();
            MockMessageChannel.CurrentUser = new MockUser { Username = "Bot", IsBot = true }; ;

            _state = new State();
            var role = _state.Roles.First(item => item.Name == "Helmsman");
            _state = _state.With(
                true,
                _state.Players.Add(new Player(_user.Id, role.Id, _state.DefaultShipId)));

            _terminal = (HelmsmanTerminal)role.CreateTerminal(_channel, _state.Players[0].Id);
        }

        [Test]
        public void Test()
        {
            _terminal.MessageRecieved(_state, new MockMessage(_user, _channel, "a"));

            var botMessage = _channel.MessagesSent[1];
        }
    }
}
