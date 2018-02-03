using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTests
{
    public class MockUser : IUser
    {
        public string AvatarId => throw new NotImplementedException();

        public string Discriminator => throw new NotImplementedException();

        public ushort DiscriminatorValue => throw new NotImplementedException();

        public bool IsBot { get; set; }

        public bool IsWebhook => throw new NotImplementedException();

        public string Username { get; set; } = "A Username";

        public DateTimeOffset CreatedAt => throw new NotImplementedException();

        public ulong Id { get; } = BitConverter.ToUInt64(Guid.NewGuid().ToByteArray(), 0);

        public string Mention => throw new NotImplementedException();

        public Game? Game => throw new NotImplementedException();

        public UserStatus Status => throw new NotImplementedException();

        public string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
        {
            throw new NotImplementedException();
        }

        public Task<IDMChannel> GetOrCreateDMChannelAsync(RequestOptions options = null)
        {
            throw new NotImplementedException();
        }
    }
}
