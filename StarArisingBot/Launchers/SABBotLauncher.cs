using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using StarArisingBot.Business.System;
using StarArisingBot.MinigameEngine;
using StarArisingBot.Core.Commands;
using StarArisingBot.Experimental.Minigame;

//Minigames
using StarArisingBot.Minigames.HungerGames;
using StarArisingBot.Minigames.WhoSentTheMessage;

namespace StarArisingBot.Launchers
{
    public static class SABBotLauncher
    {
        private static SABBotTokens botTokens = new();

        //====================//
        //BOT SETTINGS ACTIONS//
        public static async Task<DiscordClient> StartBotSettingsAsync()
        {
            string root = Directory.GetCurrentDirectory();
            string dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);

            botTokens = new SABBotTokens()
            {
                BotToken = Environment.GetEnvironmentVariable("BOT_TOKEN")
            };

            //===================================================//

            DiscordClient client;

            client = await BuildClient();
            client = await BuildCommands(client);
            client = await BuildInteractivity(client);
            await BuildMinigames(client);

            //===================================================//

            return await Task.FromResult(client);
        }

        private static async Task<DiscordClient> BuildClient()
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
            DiscordClient client = new(ClientConfig);

            return await Task.FromResult(client);
        }
        private static async Task<DiscordClient> BuildCommands(DiscordClient client)
        {
            CommandsNextConfiguration commandsConfig = new()
            {
                StringPrefixes = new string[] { ":>", "=>" },

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

            //Normal Commands
            commandsNext.RegisterCommands<UtilitiesCommands>();
            commandsNext.RegisterCommands<InfoCommands>();
            commandsNext.RegisterCommands<InteractionCommands>();

            //Experimental
            commandsNext.RegisterCommands<TestCommands>();
            commandsNext.RegisterCommands<MinigameCommand>();
            //==================//

            #endregion

            return await Task.FromResult(client);
        }
        private static async Task<DiscordClient> BuildInteractivity(DiscordClient client)
        {
            client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromSeconds(30),

                ResponseBehavior = InteractionResponseBehavior.Ack,
            });

            return await Task.FromResult(client);
        }
        private static async Task BuildMinigames(DiscordClient client)
        {
            await MinigameInstanceClient.StartAsync(client);
            var commandsNext = client.GetCommandsNext();

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
