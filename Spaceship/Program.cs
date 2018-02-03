using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Lens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MoreLinq;
using Spaceship.Model;
using System.Collections.Immutable;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Spaceship
{
    public static class Program
    {
        public static DiscordSocketClient Client { get; private set; }
        public static bool GuildReady { get; private set; }
        private static State _gameState = new State();
        public static State GameState
        {
            get
            {
                lock (_stateLock)
                {
                    return _gameState;
                }
            }
            private set
            {
                lock (_stateLock)
                {
                    _gameState = value;
                }
            }
        }
        public static RestGuild Guild { get; private set; }
        public static List<RestTextChannel> Channels { get; private set; }
        public static RestTextChannel GeneralChannel { get; private set; }
        public static ulong OwnerId { get; private set; }
        private static readonly object _stateLock = new object();

        public static ImmutableList<Command> Commands { get; private set; }

        public static void Main(string[] args)
        {
            Commands = GetCommands();
            MainAsync(args).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(string[] args)
        {

            Client = new DiscordSocketClient(new DiscordSocketConfig());
            Client.Log += Logger;

            string token = File.ReadAllText(Path.Combine("..", "..", "..", "Token.txt")); // Remember to keep this private!
            OwnerId = ulong.Parse(File.ReadAllText(Path.Combine("..", "..", "..", "OwnerId.txt")));

            Client.MessageReceived += _client_MessageReceived;

            Client.Ready += Client_Ready;
            Client.UserJoined += Client_UserJoined;
            Client.UserLeft += Client_UserLeft;

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async static Task Client_UserLeft(SocketGuildUser arg)
        {
            if (arg.Guild.Id == Guild.Id)
            {
                SetState(state => state.Set(
                    p => p.Players,
                    v => v.Remove(v.FirstOrDefault(item => item.UserId == arg.Id))));
            }
        }

        private async static Task Client_UserJoined(SocketGuildUser arg)
        {
            if (arg.Guild.Id == Guild.Id)
            {
                SetState(state => state.Set(
                    p => p.Players,
                    v => v.Add(new Player(state.DefaultRole.Id, state.DefaultShip).With(arg.Id))));
            }
        }

        private async static Task Client_Ready()
        {
            await CreateGuildAndInvite();
        }

        private async static Task CreateGuildAndInvite()
        {
            // For some reason we need to delete old guilds last.  Otherwise GetUser will return null if the user was in those guilds.
            var oldGuilds = Client.Guilds.Where(item => item.OwnerId == Client.CurrentUser.Id).ToList();

            var voiceRegions = Client.VoiceRegions.ToList();
            Guild = await Client.CreateGuildAsync("TestServer", voiceRegions[11]);

            Channels = (await Guild.GetTextChannelsAsync()).ToList();
            GeneralChannel = Channels[0];

            var invite = await Channels.First().CreateInviteAsync();

            var user = Client.GetUser(OwnerId);

            await user.SendMessageAsync(invite.Url);

            foreach (var guild in oldGuilds)
            {
                await guild.DeleteAsync();
            }
        }

        private static ImmutableList<Command> GetCommands()
        {
            string ListRoles(State state) => $"The following roles are available: {state.Roles.Select(item => "\n" + item.Name).ToDelimitedString("")}";
            void SayHi(IMessage message) =>
                message.ReplyWith($"{new[] { "Hello", "Hi", "Salutations", "Greetings" }.RandomSubset(1).First()}, {message.Author.Username}.");

            var commands = new[]
            {
                new Command("hi", (state, data) => SayHi(data.Message)),
                new Command("hello", (state, data) => SayHi(data.Message)),
                new Command(
                    "set name",
                    (state, data) =>
                    {
                        if (data.ContentWithoutPrefix == "")
                        {
                            data.Message.ReplyWith($"No name specified. Please type \"{data.Prefix} {{First name}} {{Last name}}\".");
                            return state;
                        }

                        var name = data.ContentWithoutPrefix
                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Concat(new[] { "Default", "Name" })
                            .Select(item => item.Substring(0, Math.Min(item.Length, 10)))
                            .ToList();

                        var index = state.Players.FindIndex(item => item.UserId == data.Message.Author.Id);
                        DebugEx.Assert(index != -1);
                        var player = state.Players[index].With(firstName: name[0], lastName: name[1]);

                        var fullName = $"{player.FirstName} {player.LastName}";
                        Guild.GetUserAsync(player.UserId).Result.ModifyAsync(properties => properties.Nickname = fullName);
                        data.Message.ReplyWith(
                            $"{data.Message.Author.Username}, your name is now {fullName}.");

                        return state.Set(p => p.Players[index], player);
                    }),
                new Command(
                    "get name",
                    (state, data) =>
                    {
                        var player = state.Players.First(item => item.UserId == data.Message.Author.Id);
                        data.Message.ReplyWith(
                            $"{data.Message.Author.Username}, your name is {player.FirstName} {player.LastName}.");
                    }),
                new Command(
                    "list roles",
                    (state, data) => data.Message.ReplyWith(ListRoles(state))),
                new Command(
                    "set role",
                    (state, data) =>
                    {
                        if (state.IsStarted)
                        {
                            data.Message.ReplyWith($"{data.Message.Author.Username}, your role cannot be changed during a simulation.");
                        }

                        var roleText = data.ContentWithoutPrefix.ToLower();
                        if (roleText == "")
                        {
                            data.Message.ReplyWith($"No role specified. Please type \"{data.Prefix} {{Name of role}}\".\n{ListRoles(state)}");
                            return state;
                        }
                        var role = state.Roles.FirstOrDefault(item => item.Name.ToLower() == roleText);
                        if (role != null)
                        {
                            var index = state.Players.FindIndex(item => item.UserId == data.Message.Author.Id);
                            data.Message.ReplyWith($"{data.Message.Author.Username}, your role is now {role.Name}");
                            return state.Set(p => p.Players[index].RoleId, role.Id);
                        }
                        data.Message.ReplyWith(
                            $"{data.Message.Author.Username}, no role with that name exists. \n{ListRoles(state)}");
                        return state;
                    }),
                new Command(
                    "get role",
                    (state, data) => data.Message.ReplyWith(
                        $"{data.Message.Author.Username}, your role is {data.Message.GetPlayer(state).GetRole(state).Name}")),
                new Command(
                    "start sim",
                    (state, data) =>
                    {
                        if (data.Message.Author.Id == OwnerId)
                        {
                            if (state.IsStarted)
                            {
                                data.Message.ReplyWith("Simulation is already in progress.");
                            }
                            else
                            {
                                data.Message.ReplyWith("Beginning simulation");
                                foreach (var player in state.Players.Where(item => item.GetRole(state).HasChannel))
                                {
                                    CreateChannelAsync(player.GetRole(state).Name + "s-terminal", player.UserId);
                                }
                                return state.With(true);
                            }
                        }
                        return state;
                    })
            }.ToImmutableList();

            var helpCommand = new Command(
                "help",
                (state, data) =>
                    data.Message.ReplyWith(
                        $"The following commands are available: {commands.Select(item => "\n" + item.Prefix).ToDelimitedString("")}"));

            return commands.Add(helpCommand);
        }

        public async static Task CreateChannelAsync(string channelName, ulong userId)
        {
            var channel = await Guild.CreateTextChannelAsync(channelName, new RequestOptions());

            SetState(state =>
            {
                var index = state.Players.FindIndex(item => item.UserId == userId);
                return index == -1 ?
                    state :
                    state.Set(
                        p => p.Players[index].ChannelId,
                        channel.Id.ToMaybe());
            });
        }

        public static void ReplyWith(this IMessage message, string response, bool noIcon = false) =>
            message.Channel.SendMessageAsync($"{(noIcon ? "" : ":computer: ")} {response}");

        public static Player GetPlayer(this IMessage message, State state) =>
            state.Players.First(item => item.UserId == message.Author.Id);


        private static async Task _client_MessageReceived(SocketMessage arg)
        {
            if (arg.Source != MessageSource.User || arg.Author.Id == Client.CurrentUser.Id)
            {
                return;
            }

            if (arg.Channel.Id == GeneralChannel.Id)
            {
                var userMessage = arg.Content.ToLower().Trim();

                SetState(state =>
                {
                    var newState = state;
                    foreach (var command in Commands.Where(item => userMessage == item.Prefix || userMessage.StartsWith(item.Prefix + " ")))
                    {
                        newState = command.CommandFunc(state, new CommandData(arg, command.Prefix));
                    }
                    return newState;
                });
                return;
            }

            SetState(state =>
            {
                var player = GetPlayer(arg, state);
                if (arg.Channel.Id.ToMaybe() == player.ChannelId)
                {
                    return player.GetRole(GameState).Terminal.MessageRecieved(state, arg);
                }
                return state;
            });

            return;
        }

        private static void SetState(Func<State, State> stateChange)
        {
            lock (_stateLock)
            {
                GameState = stateChange(GameState);
            }
        }

        private static Task Logger(LogMessage message)
        {
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
            Console.ResetColor();

            // If you get an error saying 'CompletedTask' doesn't exist,
            // your project is targeting .NET 4.5.2 or lower. You'll need
            // to adjust your project's target framework to 4.6 or higher
            // (instructions for this are easily Googled).
            // If you *need* to run on .NET 4.5 for compat/other reasons,
            // the alternative is to 'return Task.Delay(0);' instead.
            return Task.CompletedTask;
        }
    }
}
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously