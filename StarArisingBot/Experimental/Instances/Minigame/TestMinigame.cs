using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;
using StarArisingBot.MinigameEngine;

namespace StarArisingBot.Experimental.Minigame
{
    public class TestMinigame : MinigameModule
    {
        protected override async Task OnStarted(params object[] minigameParams)
        {
            var message = await Context.RespondAsync("Reação");
            await message.CreateReactionAsync(DiscordEmoji.FromName(Client, ":star:"));

            var result = await message.WaitForReactionAsync(Context.User);
            if (!result.TimedOut)
            {
                await Context.RespondAsync("Clicou");
            }
        }

        protected override Task OnFinalized()
        {
            throw new NotImplementedException();
        }
    }
}
