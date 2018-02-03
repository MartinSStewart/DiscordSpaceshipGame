using Discord;
using Equ;
using Lens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class CommandData
    {
        public string ContentWithoutPrefix => Message.Content.Substring(Prefix.Length).Trim();
        public string Content => Message.Content.Trim();
        public string Prefix { get; }
        public IMessage Message { get; }

        public CommandData(IMessage message, string prefix)
        {
            Message = message;
            Prefix = prefix;
        }
    }
}
