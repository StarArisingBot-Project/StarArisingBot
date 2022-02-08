using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SAB.Bot.Commands;
using SAB.Behaviors;
using SAB.Experimental;
using System;
using System.IO;
using System.Threading.Tasks;
using StarArisingBot.Business.System;
using StarArisingBot.Minigames.HungerGames;
using StarArisingBot.MinigameEngine;

namespace SAB.Launchers
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
            SABCommandsBehavior commandsExecutionController = new SABCommandsBehavior();
            CommandsNextConfiguration commandsConfig = new()
            {
                StringPrefixes = new string[] { ":>", "=>"},

                EnableMentionPrefix = true,
                EnableDms = true,
                CaseSensitive = false,
                IgnoreExtraArguments = false,
                CommandExecutor = commandsExecutionController,
            };
            CommandsNextExtension commandsNext = client.UseCommandsNext(commandsConfig);

            commandsExecutionController.Client = client;
            commandsExecutionController.CommandsNext = commandsNext;

            #region Register Commands
            //==================//
            //Especial Commands
            commandsNext.RegisterCommands<EvalCommands>();

            //Normal Commands
            commandsNext.RegisterCommands<UtilitiesCommands>();
            commandsNext.RegisterCommands<InteractionCommands>();

            //Experimental
            commandsNext.RegisterCommands<TestCommands>();
            //==================//
            #endregion
            #region Register Minigames
            //==================//
            //Minigames
            commandsNext.RegisterCommands<HGCommands>();

            //Simulators

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
                ButtonBehavior = ButtonPaginationBehavior.Disable,
            });

            return await Task.FromResult(client);
        }

        //====================//
    }
}
