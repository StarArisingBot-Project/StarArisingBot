#pragma warning disable CS8618

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using StarArisingBotFramework.Attributes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StarArisingBot.Core.Commands
{
    [Category("Utilities")]
    public class HelpCommands : BaseCommandModule
    {
        private CommandContext Context { get; set; }
        private Assembly CurrentAssembly { get; set; }
        private Dictionary<string, DiscordEmoji> HelpEmojisDictionary { get; } = new();

        private DiscordMessage mainMessage;

        [Command("Ajuda"), Aliases("Help"), Description("Abre o menu de ajuda")]
        public async Task Startup(CommandContext ctx)
        {
            //Infos
            Context = ctx;
            CurrentAssembly = typeof(HelpCommands).Assembly;

            HelpEmojisDictionary.Add("Commands", DiscordEmoji.FromGuildEmote(ctx.Client, 1002368869035429949));
            HelpEmojisDictionary.Add("Site", DiscordEmoji.FromUnicode("🌐"));
            HelpEmojisDictionary.Add("FAQ", DiscordEmoji.FromGuildEmote(ctx.Client, 1002369373568249926));
            HelpEmojisDictionary.Add("Support", DiscordEmoji.FromUnicode("👤"));
            HelpEmojisDictionary.Add("Guildelines", DiscordEmoji.FromUnicode("📜"));

            //Message
            mainMessage = await ctx.RespondAsync("《 **INICIANDO MENU DE AJUDA** 》\n");

            //Menu
            await ShowMenu();
        }

        //================================//
        private async Task ShowMenu()
        {
            StringBuilder helpEmbedContent = new();

            helpEmbedContent.AppendLine("**➤ Sou um bot Brasileiro em desenvolvimento, muito divertido e com varios comandos para te entreter!**");
            helpEmbedContent.AppendLine($"\n**:star: • Precisando de ajuda {Context.User.Username}? Aqui está um Roadmap para auxiliar você nas minhas funções! • :star:**\n");

            helpEmbedContent.AppendFormat("{0} • **[ Lista de Comandos ]** • {0}\n", HelpEmojisDictionary["Commands"]);
            helpEmbedContent.AppendLine("[Indisponível]");

            helpEmbedContent.AppendFormat("\n{0} • **[ Site do BOT ]** • {0}\n", HelpEmojisDictionary["Site"]);
            helpEmbedContent.AppendLine("[Indisponível]");

            helpEmbedContent.AppendFormat("\n{0} • **[ F.A.Q do BOT ]** • {0}\n", HelpEmojisDictionary["FAQ"]);
            helpEmbedContent.AppendLine("[Indisponível]");

            helpEmbedContent.AppendFormat("\n{0} • **[ Servidor de Suporte ]** • {0}\n", HelpEmojisDictionary["Support"]);
            helpEmbedContent.AppendLine("[Indisponível]");

            helpEmbedContent.AppendFormat("\n{0} • **[ Diretrizes da comunidade ]** • {0}\n", HelpEmojisDictionary["Guildelines"]);
            helpEmbedContent.AppendLine("[Indisponível]");

            //Embed
            DiscordEmbedBuilder helpEmbed = new()
            {
                Title = "**:jigsaw: │ MENU DE AJUDA │ :jigsaw:** \n",
                Description = $"{helpEmbedContent}\n ",
                ImageUrl = "https://cdn.discordapp.com/attachments/825874220692537385/880867867321597982/StarArising_Bot_Thumb_10000.png",

                Color = DiscordColor.Purple,
            };

            DiscordMessageBuilder helpMessageBuilder = new();
            helpMessageBuilder.AddEmbed(helpEmbed);
            helpMessageBuilder.AddComponents(new DiscordComponent[] {
                new DiscordLinkButtonComponent("https://google.com/", "Comandos", true, new(HelpEmojisDictionary["Commands"])),
                new DiscordLinkButtonComponent("https://google.com/", "Site do Bot", true, new(HelpEmojisDictionary["Site"])),
                new DiscordLinkButtonComponent("https://google.com/", "F.A.Q do Bot", true, new(HelpEmojisDictionary["FAQ"])),
            });

            helpMessageBuilder.AddComponents(new DiscordComponent[] {
                new DiscordLinkButtonComponent("https://google.com/", "Servidor de Suporte", true, new(HelpEmojisDictionary["Support"])),
                new DiscordLinkButtonComponent("https://google.com/", "Diretrizes da comunidade", true, new(HelpEmojisDictionary["Guildelines"])),
            });

            await mainMessage.ModifyAsync(helpMessageBuilder);
        }

        //================================//
        //Members
        private class CategoriesItems
        {
            public string Name { get; set; }
            public int CommandsCount { get; set; }
            private Type CommandType { get; }

            public CategoriesItems(string name, Type commandType)
            {
                Name = name;
                CommandType = commandType;

                AddCommands();
            }

            public void AddCommands()
            {
                CommandsCount += CommandType.GetMethods().Count(x => x.GetCustomAttribute<CommandAttribute>() != null);
            }
        }
    }
}
