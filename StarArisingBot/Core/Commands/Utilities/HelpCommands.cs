using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace StarArisingBot.Core.Commands
{
    public class HelpCommands : BaseCommandModule
    {
        [Command("Ajuda"), Aliases("Help"), Description("Abre o menu de ajuda")]
        public async Task Help(CommandContext ctx)
        {
            //============================//
            //Description
            StringBuilder helpEmbedContent = new();

            helpEmbedContent.AppendLine($"\n**:star: • Precisando de ajuda {ctx.User.Username}? Aqui está uma lista completa de todos os meus comandos! • :star:**\n");
            helpEmbedContent.AppendLine($"Selecione a categoria que deseja:");



            DiscordEmbedBuilder helpEmbed = new()
            {
                Title = "**:jigsaw: │ MENU DE AJUDA │ :jigsaw:** \n",
                Description = helpEmbedContent.ToString(),
                Thumbnail = new() { Url = ctx.User.AvatarUrl },
                ImageUrl = "https://cdn.discordapp.com/attachments/825874220692537385/880867867321597982/StarArising_Bot_Thumb_10000.png",
                Color = DiscordColor.Purple,
            };

            await ctx.RespondAsync(helpEmbed);
        }
    }
}
