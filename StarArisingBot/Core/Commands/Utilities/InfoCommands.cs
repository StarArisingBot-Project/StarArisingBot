using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using StarArisingBot.Managers;
using StarArisingBot.System;
using StarArisingBotFramework.Attributes.Commands;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace StarArisingBot.Core.Commands
{
    [Category("Utilities")]
    public class InfoCommands : BaseCommandModule
    {
        [Command("BotInfo"), Aliases("InfoBot"), Description("Veja informações relacionadas ao Bot")]
        public async Task BotInfo(CommandContext ctx)
        {
            StringBuilder botInfosString = new();

            //Emojis
            DiscordEmoji dsharpPlusLogoEmoji = DiscordEmoji.FromGuildEmote(ctx.Client, 1001987744165269504);
            DiscordEmoji serverIconEmoji = DiscordEmoji.FromGuildEmote(ctx.Client, 1001988437232058420);
            DiscordEmoji githubIconEmoji = DiscordEmoji.FromGuildEmote(ctx.Client, 1001988757391671316);

            //Infos
            TimeSpan botUptime = SABBotUptimeManager.GetUptime();
            long currentRamUsage = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);

            //===============================//
            botInfosString.AppendLine("『 Algumas informações sobre mim e meu ambiente de desenvolvimento 』\n"); //Title

            //BOT SECTION
            botInfosString.AppendLine("🤖 • **[ BOT ]** • 🤖"); //Title
            botInfosString.AppendLine($"➥ **Versão:** {SABApplication.Version}");
            botInfosString.AppendLine($"➥ **Língua:** {SABApplication.Language}");

            //BOT STATS
            botInfosString.AppendLine("\n📊 • **[ ESTATÍSTICAS ]** • 📊"); //Title
            botInfosString.AppendLine($"➥ **Tempo de Atividade:** {botUptime.Days}d {botUptime.Hours}h {botUptime.Minutes}m {botUptime.Seconds}s {botUptime.Milliseconds}ms");

            //BOT CLIENT
            botInfosString.AppendLine("\n🖥️ • **[ CLIENTE ]** • 🖥️"); //Title
            botInfosString.AppendLine($"➥ **Ping:** {ctx.Client.Ping}ws");
            botInfosString.AppendLine($"➥ **Servidores:** {ctx.Client.Guilds.Count}");

            //DSHARPPLUS
            botInfosString.AppendLine($"\n{dsharpPlusLogoEmoji} • **[ DSHARPPLUS ]** • {dsharpPlusLogoEmoji}"); //Title
            botInfosString.AppendLine($"➥ **Versão:** {ctx.Client.VersionString}");

            //BOT HOST
            botInfosString.AppendLine($"\n{serverIconEmoji} • **[ HOST ]** • {serverIconEmoji}"); //Title
            botInfosString.AppendLine($"➥ **Uso de Mémoria:** {currentRamUsage}mb");
            botInfosString.AppendLine($"➥ **Plataform:** {Environment.OSVersion.Platform}");
            botInfosString.AppendLine($"➥ **Version:**  {Environment.OSVersion.VersionString}");
#if DEBUG
            botInfosString.AppendLine($"➥ **Ambiente:** Development");
#else
                botInfosString.AppendLine($"➥ **Ambiente:** Production ");
#endif

            //BOT GITHUB
            botInfosString.AppendLine($"\n{githubIconEmoji} • **[ GITHUB ]** • {githubIconEmoji}"); //Title
            botInfosString.AppendLine($"➥ [**StarArisingBot (Source)**](https://github.com/StarArisingBot-Project/StarArisingBot)");
            botInfosString.AppendLine($"➥ [**StarArisingBot (Project)**](https://github.com/StarArisingBot-Project/)");

            DiscordEmbedBuilder infoEmbed = new()
            {
                Title = "📀 • MINHAS INFORMAÇÕES • 📀",
                Description = botInfosString.ToString(),
                Thumbnail = new() { Url = ctx.Client.CurrentUser.AvatarUrl },
                Color = DiscordColor.Yellow,
                Footer = new() { Text = $"{ctx.Client.CurrentUser.Username} • {DateTime.Now.ToShortDateString()}" },
            };

            await ctx.RespondAsync(infoEmbed);
        }
    }
}
