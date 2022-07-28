using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace StarArisingBot.Core.Commands
{
    public class UtilitiesCommands : BaseCommandModule
    {
        [Command("Convite"), Aliases("invite", "invitation"), Description("Mostra meu Link de Convite para me Adicionar no seu Servidor!")]
        public async Task BotInvite(CommandContext ctx)
        {
            DiscordEmbedBuilder InviteEmbed = new()
            {
                Color = DiscordColor.Yellow,
                Title = ":star: • Menssagem de Convite • :star:",
                Description = $"**Está gostando de mim {ctx.Member.DisplayName}? Então porquê me adicione no seu servidor?** \n\n" +
                              $"**➤ Eu sou um bot Brasileiro em desenvolvimento, muito divertido e com varios comandos!** \n" +

                              $"• Posso ajudar você a criar suas historias de um jeito mais divertido com meus **Geradores**, tenho comandos de **Minigames** que podem ser jogados com varias pessoas e tenho também varios outros sistemas interessantes que tenho certeza que irá gostar e te viciar! \n\n" +

                              "**Gostou do que você leu? Me adicione no seu servidor então!** \n\n" +
                              $"<:Emerald:880247541554364466> • **Convite do Bot** • <:Emerald:880247541554364466> \n [Clique aqui!](https://discordapp.com/oauth2/authorize?client_id=855527328809484328&scope=bot&permissions=8) \n\n" +
                              $"<:Ambar:880247541097189376> • **Convite do Servidor de suporte** • <:Ambar:880247541097189376> \n [Indisponível]",

                ImageUrl = "https://cdn.discordapp.com/attachments/825874220692537385/880867867321597982/StarArising_Bot_Thumb_10000.png",
            };

            await ctx.RespondAsync(InviteEmbed);
        }
    }
}
