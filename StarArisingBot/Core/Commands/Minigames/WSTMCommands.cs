using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using StarArisingBot.MinigameEngine;
using StarArisingBot.Minigames.WhoSentTheMessage;
using StarArisingBotFramework.Attributes.Commands;
using System.Threading.Tasks;

namespace StarArisingBot.Core.Commands
{
    [Category("Minigames")]
    public class WSTMCommands : BaseCommandModule
    {
        [Command("WhoSentTheMessage"), Aliases("WSTM"), Description("Tente adivinhar o autor da mensagem!")]
        public async Task WhoSentTheMessage(CommandContext ctx)
        {
            if (await MinigameInstanceClient.GetInstanceAsync<WSTMMinigame>().Result.GetSessionAsync(MinigameSessionAuthorType.User, ctx.User.Id) != null)
            {
                await ctx.RespondAsync($"<@{ctx.User.Id}> **VOCÊ JÁ INICIOU ESSE MINIGAME, TERMINE ANTES DE INICIAR OUTRO**");
                return;
            }

            await MinigameInstanceClient.GetInstanceAsync<WSTMMinigame>().Result.CreateNewSessionAsync(ctx, new WSTMMinigame(), new() { AuthorType = MinigameSessionAuthorType.User, InvokeType = MinigameSessionInvokeType.User });
        }
    }
}
