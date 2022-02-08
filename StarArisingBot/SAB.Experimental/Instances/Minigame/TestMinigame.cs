using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;
using StarArisingBot.MinigameEngine;

namespace SAB.Experimental
{
    public class TestMinigame : MinigameModule
    {
        protected override Task OnFinalized()
        {
            throw new NotImplementedException();
        }

        protected override async Task OnStarted(params object[] minigameParams)
        {
            await Context.Channel.SendMessageAsync($"**Instancia Iniciada**");
        }
    }
}
