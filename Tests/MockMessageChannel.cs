﻿using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DiscordBotTests
{
    public class MockMessageChannel : ITextChannel
    {
        public static IUser CurrentUser { get; set; }


        public string Name => throw new NotImplementedException();

        public bool IsNsfw => throw new NotImplementedException();

        public DateTimeOffset CreatedAt => throw new NotImplementedException();
        
        public ulong Id { get; } = (ulong)new Random().Next();

        public Task DeleteMessagesAsync(IEnumerable<IMessage> messages, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMessagesAsync(IEnumerable<ulong> messageIds, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public IDisposable EnterTypingState(RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IMessage> GetMessageAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IReadOnlyCollection<IMessage>> GetMessagesAsync(int limit = 100, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IReadOnlyCollection<IMessage>> GetMessagesAsync(ulong fromMessageId, Direction dir, int limit = 100, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IReadOnlyCollection<IMessage>> GetMessagesAsync(IMessage fromMessage, Direction dir, int limit = 100, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<IMessage>> GetPinnedMessagesAsync(RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IReadOnlyCollection<IUser>> GetUsersAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IUserMessage> SendFileAsync(string filePath, string text = null, bool isTTS = false, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IUserMessage> SendFileAsync(Stream stream, string filename, string text = null, bool isTTS = false, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IUserMessage> SendMessageAsync(string text, bool isTTS = false, Embed embed = null, RequestOptions options = null)
        {
            var message = new MockMessage((MockUser)CurrentUser, this, text);
            return new Task<IUserMessage>(() => (IUserMessage)message);
        }

        public Task TriggerTypingAsync(RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task ModifyAsync(Action<TextChannelProperties> func, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IInviteMetadata> CreateInviteAsync(int? maxAge = 1800, int? maxUses = null, bool isTemporary = false, bool isUnique = false, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<IInviteMetadata>> GetInvitesAsync(RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task ModifyAsync(Action<GuildChannelProperties> func, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public OverwritePermissions? GetPermissionOverwrite(IRole role)
        {
            throw new NotImplementedException();
        }

        public OverwritePermissions? GetPermissionOverwrite(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemovePermissionOverwriteAsync(IRole role, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task RemovePermissionOverwriteAsync(IUser user, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task AddPermissionOverwriteAsync(IRole role, OverwritePermissions permissions, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task AddPermissionOverwriteAsync(IUser user, OverwritePermissions permissions, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerable<IReadOnlyCollection<IGuildUser>> IGuildChannel.GetUsersAsync(CacheMode mode, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        Task<IGuildUser> IGuildChannel.GetUserAsync(ulong id, CacheMode mode, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public List<MockMessage> MessagesSent { get; } = new List<MockMessage>();

        public string Topic => throw new NotImplementedException();

        public string Mention => throw new NotImplementedException();

        public int Position => throw new NotImplementedException();

        public IGuild Guild => throw new NotImplementedException();

        public ulong GuildId => throw new NotImplementedException();

        public IReadOnlyCollection<Overwrite> PermissionOverwrites => throw new NotImplementedException();
    }
}
