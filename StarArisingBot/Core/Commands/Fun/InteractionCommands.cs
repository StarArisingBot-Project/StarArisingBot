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
            await Avatar(ctx, ctx.User);
        }

        [Command("Avatar")]
        public async Task Avatar(CommandContext ctx, DiscordUser user)
        {
            DiscordEmbedBuilder embedBuilder = new()
            {
                Title = $"🖼️ • Avatar de {user.Username} • 🖼️",
                Description = $"**Para baixar a imagem clique [Aqui]({user.AvatarUrl})**",

                ImageUrl = $"{user.GetAvatarUrl(ImageFormat.Auto, 2048)}",
                Color = new DiscordColor("#517798"),
            };

            await ctx.RespondAsync(embedBuilder);
        }
    }
}
