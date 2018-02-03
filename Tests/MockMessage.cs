using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTests
{
    public class MockMessage : IMessage
    {
        public MessageType Type => throw new NotImplementedException();

        public MessageSource Source => throw new NotImplementedException();

        public bool IsTTS => throw new NotImplementedException();

        public bool IsPinned => throw new NotImplementedException();

        public string Content { get; set; } = "";

        public DateTimeOffset Timestamp => throw new NotImplementedException();

        public DateTimeOffset? EditedTimestamp => throw new NotImplementedException();

        public IMessageChannel Channel { get; }

        public IUser Author { get; }

        public IReadOnlyCollection<IAttachment> Attachments => throw new NotImplementedException();

        public IReadOnlyCollection<IEmbed> Embeds => throw new NotImplementedException();

        public IReadOnlyCollection<ITag> Tags => throw new NotImplementedException();

        public IReadOnlyCollection<ulong> MentionedChannelIds => throw new NotImplementedException();

        public IReadOnlyCollection<ulong> MentionedRoleIds => throw new NotImplementedException();

        public IReadOnlyCollection<ulong> MentionedUserIds => throw new NotImplementedException();

        public DateTimeOffset CreatedAt => throw new NotImplementedException();

        public ulong Id => throw new NotImplementedException();

        public MockMessage(IUser author, MockMessageChannel channel, string content)
        {
            Author = author;
            Channel = channel;
            Content = content;

            channel.MessagesSent.Add(this);
        }

        public Task DeleteAsync(RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => $"{Author.Username}: {Content}";
    }
}
