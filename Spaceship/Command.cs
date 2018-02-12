using Discord;
using Spaceship.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Command
    {
        /// <summary>
        /// Prefix to look for in messages.
        /// </summary>
        public string Prefix { get; }
        public Func<State, CommandData, State> CommandFunc { get; }

        public Command(string prefix, Func<State, CommandData, State> commandFunc)
        {
            Assert(prefix);
            Prefix = prefix;
            CommandFunc = commandFunc ?? ((state, data) => state);
        }

        public Command(string prefix, Action<State, CommandData> action)
        {
            Assert(prefix);
            Prefix = prefix;
            CommandFunc = (state, message) =>
            {
                action?.Invoke(state, message);
                return state;
            };
        }

        private void Assert(string prefix) =>
            DebugEx.Assert(
                prefix.All(item => !char.IsUpper(item)),
                "Prefixes must not have any upper case letters since messages are converted to lower case before comparing.");

        public override string ToString() => Prefix;
    }
}
