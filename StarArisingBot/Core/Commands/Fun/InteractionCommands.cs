using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using StarArisingBotFramework.Attributes.Commands;
using System.Threading.Tasks;

namespace StarArisingBot.Core.Commands
{
    [Category("Fun")]
    public class InteractionCommands : BaseCommandModule
    {
        [Command("Avatar"), Description("Veja o seu Avatar ou o Avatar algum usuário!")]
        public async Task Avatar(CommandContext ctx)
        {
            DiscordEmbedBuilder embedBuilder = new()
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
