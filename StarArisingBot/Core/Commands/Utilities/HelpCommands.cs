using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using StarArisingBotFramework.Attributes.Commands;

namespace StarArisingBot.Core.Commands
{
    [Category("Utilities")]
    public class HelpCommands : BaseCommandModule
    {
        private Assembly currentAssembly;

        private DiscordMessage mainMessage;

        [Command("Ajuda"), Aliases("Help"), Description("Abre o menu de ajuda")]
        public async Task Startup(CommandContext ctx)
        {
            currentAssembly = typeof(HelpCommands).Assembly;

            //============================//
            //Description
            StringBuilder helpEmbedContent = new();

            helpEmbedContent.AppendLine($"**➤ Sou um bot Brasileiro em desenvolvimento, muito divertido e com varios comandos para te entreter!**");
            helpEmbedContent.AppendLine($"\n**:star: • Precisando de ajuda {ctx.User.Username}? Aqui está um Roadmap para auxiliar você nas minhas funções! • :star:**\n");

            helpEmbedContent.AppendLine($"**[ Lista de Comandos ]**");
            helpEmbedContent.AppendLine($"_Veja a lista completa de Comandos dísponíveis_\n");

            helpEmbedContent.AppendLine($"**[ Site do BOT ]**");
            helpEmbedContent.AppendLine($"[Indisponível]\n");

            helpEmbedContent.AppendLine($"**[ F.A.Q do BOT ]**");
            helpEmbedContent.AppendLine($"[Indisponível]\n");

            helpEmbedContent.AppendLine($"**[ Servidor de Suporte ]**");
            helpEmbedContent.AppendLine($"[Indisponível]\n");

            helpEmbedContent.AppendLine($"**[ Diretrizes da comunidade ]**");
            helpEmbedContent.AppendLine($"[Indisponível]\n");

            //Categorys
            //List<CategoriesItems> commandsCategorys = new();
            //foreach (Type assemblyType in currentAssembly.DefinedTypes)
            //{
            //    CategoryAttribute? categoryAttribute = assemblyType.GetCustomAttribute<CategoryAttribute>();
            //    if (categoryAttribute == null) continue;

            //    CategoriesItems? result = commandsCategorys.Find(x => x.Name == categoryAttribute.Name);
            //    if(result?.Name == null)
            //    {
            //        commandsCategorys.Add(new(categoryAttribute.Name, assemblyType));
            //        continue;
            //    }

            //    result.AddCommands();
            //}

            //foreach (CategoriesItems category in commandsCategorys)
            //{
            //    helpEmbedContent.AppendLine($"• {category.Name} ({category.CommandsCount})");
            //}

            //Embed
            DiscordEmbedBuilder helpEmbed = new()
            {
                Title = "**:jigsaw: │ MENU DE AJUDA │ :jigsaw:** \n",
                Description = $"{helpEmbedContent}\n ",
                ImageUrl = "https://cdn.discordapp.com/attachments/825874220692537385/880867867321597982/StarArising_Bot_Thumb_10000.png",

                Color = DiscordColor.Purple,
            };

            await ctx.RespondAsync(helpEmbed);
        }

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
