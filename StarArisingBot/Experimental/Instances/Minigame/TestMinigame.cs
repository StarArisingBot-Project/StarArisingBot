using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using StarArisingBot.MinigameEngine;
using System;
using System.Threading.Tasks;

namespace StarArisingBot.Experimental.Minigame
{
    public class TestMinigame : MinigameModule
    {
        protected override async Task OnStarted(params object[] minigameParams)
        {
            DiscordMessage? message = await Context.RespondAsync("Reação");
            await message.CreateReactionAsync(DiscordEmoji.FromName(Client, ":star:"));

            InteractivityResult<DSharpPlus.EventArgs.MessageReactionAddEventArgs> result = await message.WaitForReactionAsync(Context.User);
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
