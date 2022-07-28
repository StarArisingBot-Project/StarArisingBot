using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarArisingBot.MinigameEngine;
using DSharpPlus.Interactivity;
using DSharpPlus.EventArgs;
using DSharpPlus;
using StarArisingBot.Minigames.WhoSentTheMessage;
using StarArisingBotFramework.Attributes.Commands;

namespace StarArisingBot.Core.Commands
{
    [Category("Minigames")]
    public class WSTMCommands : BaseCommandModule
    {
        [Command("WhoSentTheMessage"), Aliases("WSTM"), Description("Tente adivinhar o autor da mensagem!")]
        public async Task WhoSentTheMessage(CommandContext ctx)
        {
            if(await MinigameInstanceClient.GetInstanceAsync<WSTMMinigame>().Result.GetSessionAsync(MinigameSessionAuthorType.User, ctx.User.Id) != null)
            {
                await ctx.RespondAsync($"<@{ctx.User.Id}> **VOCÊ JÁ INICIOU ESSE MINIGAME, TERMINE ANTES DE INICIAR OUTRO**");
                return;
            }

            await MinigameInstanceClient.GetInstanceAsync<WSTMMinigame>().Result.CreateNewSessionAsync(ctx, new WSTMMinigame(), new() {AuthorType = MinigameSessionAuthorType.User, InvokeType = MinigameSessionInvokeType.User });
        }
    }
}
