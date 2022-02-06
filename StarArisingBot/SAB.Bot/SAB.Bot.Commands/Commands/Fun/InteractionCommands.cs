using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;

namespace SAB.Bot.Commands
{
    public class InteractionCommands : BaseCommandModule
    {
        [Command("Avatar")]
        public async Task Avatar(CommandContext ctx)
        {
            DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder()
            {
                Title = $"🖼️ • Avatar de {ctx.User.Username} • 🖼️",
                Description = $"**Para baixar a imagem clique [Aqui]({ctx.User.AvatarUrl})**",

                ImageUrl = $"{ctx.User.AvatarUrl}",
                Color = new DiscordColor("#517798"),
            };

            await ctx.RespondAsync(embedBuilder);
        }

        [Command("Avatar")]
        public async Task Avatar(CommandContext ctx, DiscordMember member)
        {
            DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder()
            {
                Title = $"🖼️ • Avatar de {member.Username} • 🖼️",
                Description = $"**Para baixar a imagem clique [Aqui]({member.AvatarUrl})**",

                ImageUrl = $"{member.GetAvatarUrl(ImageFormat.Auto, 2048)}",
                Color = new DiscordColor("#517798"),
            };

            await ctx.RespondAsync(embedBuilder);
        }
    }
}
