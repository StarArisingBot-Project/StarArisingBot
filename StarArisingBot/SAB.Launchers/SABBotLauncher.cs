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
using SAB.Business.System;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SAB.Launchers
{
    public static class SABBotLauncher
    {
        //====================//
        //BOT SETTINGS ACTIONS//
        public static async Task<DiscordClient> StartBotSettingsAsync()
        {
            Console.WriteLine("=> Inciando  StartBotSettingsAsync()");
            Console.WriteLine("=> Pegando Token");

            //Bot Token
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            BotTokenDeserialize configJson = JsonConvert.DeserializeObject<BotTokenDeserialize>(File.ReadAllText(@$"{projectPath}\SAB.System\Files\BotToken.json"));

            Console.WriteLine("=> Token pego");
            //===================================================//

            DiscordClient client;
            client = await BuildClient(configJson);
            client = await BuildCommands(client, configJson);
            client = await BuildInteractivity(client);

            //===================================================//

            Console.WriteLine("=> client pronto.");
            return await Task.FromResult(client);
        }

        private static async Task<DiscordClient> BuildClient(BotTokenDeserialize botToken)
        {
            Console.WriteLine("=> Iniciando client");

            DiscordConfiguration ClientConfig = new()
            {
                Token = botToken.Token,
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

            Console.WriteLine("=> Client iniciado");
            return await Task.FromResult(client);
        }
        private static async Task<DiscordClient> BuildCommands(DiscordClient client, BotTokenDeserialize botToken)
        {
            Console.WriteLine("=> Iniciando comandos.");

            SABCommandsBehavior commandsExecutionController = new SABCommandsBehavior();

            CommandsNextConfiguration commandsConfig = new()
            {
                StringPrefixes = botToken.Prefix,

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
            //Especial Commands
            commandsNext.RegisterCommands<EvalCommands>();

            //Normal Commands
            commandsNext.RegisterCommands<UtilitiesCommands>();
            commandsNext.RegisterCommands<InteractionCommands>();

            //Minigames Commands

            //Experimental
            commandsNext.RegisterCommands<TestCommands>();
            #endregion

            Console.WriteLine("=> Termino de comandos.");
            return await Task.FromResult(client);
        }
        private static async Task<DiscordClient> BuildInteractivity(DiscordClient client)
        {
            Console.WriteLine("=> Iniciando interatividade.");
            client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromSeconds(30),

                ResponseBehavior = InteractionResponseBehavior.Ack,
                ButtonBehavior = ButtonPaginationBehavior.Disable,
            });

            Console.WriteLine("=> termino interatividade.");
            return await Task.FromResult(client);
        }

        //====================//
    }
}
