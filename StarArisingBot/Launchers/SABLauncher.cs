using System;
using System.IO;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using StarArisingBot.Core.Commands;
using StarArisingBot.Experimental.Minigame;
using StarArisingBot.MinigameEngine;

using StarArisingBot.Executors;

//Minigames
using StarArisingBot.Minigames.HungerGames;
using StarArisingBot.Minigames.WhoSentTheMessage;
using StarArisingBot.System;

namespace StarArisingBot.Launchers
{
    public static class SABLauncher
    {
        private static SABBotTokens botTokens = new();
        private static DiscordClient client = null;

        //====================//
        //BOT SETTINGS ACTIONS//
        public static async Task<DiscordClient> StartBotSettingsAsync()
        {
            string root = Directory.GetCurrentDirectory();
            string dotenvPath = Path.Combine(root, ".env");
            DotEnv.Load(dotenvPath);

            botTokens = new SABBotTokens()
            {
                BotToken = Environment.GetEnvironmentVariable("BOT_TOKEN"),
            };

            //===================================================//

            await BuildClient();
            await BuildCommands();
            await BuildInteractivity();
            await BuildMinigames();

            //===================================================//

            return await Task.FromResult(client);
        }

        private static Task BuildClient()
        {
            DiscordConfiguration ClientConfig = new()
            {
                Token = botTokens.BotToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                UseRelativeRatelimit = true,
                MessageCacheSize = 1024,
                AlwaysCacheMembers = true,
                GatewayCompressionLevel = GatewayCompressionLevel.Stream,

                Intents = DiscordIntents.Guilds
                          | DiscordIntents.GuildPresences
                          | DiscordIntents.GuildMessageTyping
                          | DiscordIntents.GuildMessages
                          | DiscordIntents.GuildMessageReactions
                          | DiscordIntents.GuildMembers
                          | DiscordIntents.GuildInvites
                          | DiscordIntents.GuildIntegrations
                          | DiscordIntents.DirectMessages
                          | DiscordIntents.DirectMessageReactions
                          | DiscordIntents.DirectMessageTyping
                          | DiscordIntents.GuildWebhooks
            };

            client = new(ClientConfig);
            return Task.CompletedTask;
        }
        private static Task BuildCommands()
        {
            CommandsNextConfiguration commandsConfig = new()
            {
                StringPrefixes = new string[] { ":>", "=>" },
                CommandExecutor = new SABCommandExecutor(),

                DmHelp = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                EnableDms = true,
                CaseSensitive = false,
                IgnoreExtraArguments = false,
            };

            CommandsNextExtension commandsNext = client.UseCommandsNext(commandsConfig);

            #region Register Commands

            //==================//
            //Especial Commands
            commandsNext.RegisterCommands<EvalCommands>();
            commandsNext.RegisterCommands<TestCommands>();

            //Fun Commands
            commandsNext.RegisterCommands<InteractionCommands>();

            //Utilities Commands
            commandsNext.RegisterCommands<UtilitiesCommands>();
            commandsNext.RegisterCommands<InfoCommands>();
            commandsNext.RegisterCommands<HelpCommands>();

            //Experimental
            commandsNext.RegisterCommands<MinigameCommand>();
            //==================//

            #endregion

            return Task.CompletedTask;
        }
        private static Task BuildInteractivity()
        {
            client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromSeconds(30),

                ResponseBehavior = InteractionResponseBehavior.Ack,
            });

            return Task.CompletedTask;
        }
        private static async Task BuildMinigames()
        {
            await MinigameInstanceClient.StartAsync(client);
            CommandsNextExtension? commandsNext = client.GetCommandsNext();

            //Minigame Commands
            commandsNext.RegisterCommands<HGCommands>();
            commandsNext.RegisterCommands<WSTMCommands>();

            //Minigame Register
            MinigameInstanceClient.RegisterMinigameModule<HGMinigame>();
            MinigameInstanceClient.RegisterMinigameModule<WSTMMinigame>();
        }

        //====================//
    }
}
