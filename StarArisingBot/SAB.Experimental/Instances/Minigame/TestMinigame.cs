using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using SAB.Business.Instances.Minigames;
using System;
using System.Threading.Tasks;

namespace SAB.Experimental
{
    public class TestMinigame : MinigameModule
    {
        protected override async Task OnStarted(params object[] minigameParams)
        {
            await Context.Channel.SendMessageAsync($"**Instancia Iniciada**");
        }

        protected override async Task OnCanceled()
        {
            await Context.Channel.SendMessageAsync("**Instancia Terminada!**");
        }
    }
}
