using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using SAB.Managers;
using System.Threading.Tasks;
using StarArisingBot.MinigameEngine;
using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.Interactivity.Extensions;

namespace SAB.Experimental
{
    public class MinigameCommand : BaseCommandModule
    {
        [Command("StartMinigame")]
        public async Task StartMinigame(CommandContext ctx)
        {
            MinigameStatusMessage result = await MinigameInstanceClient.GetInstanceAsync(typeof(TestMinigame)).Result.CreateNewSessionAsync(ctx, new TestMinigame(), new MinigameSessionBuilder()
            {
                Name = null,
                AuthorType = MinigameSessionAuthorType.Guild,
                InvokeType = MinigameSessionInvokeType.Guild,
            });
        }

        [Command("StartMinigameParams")]
        public async Task StartMinigameParams(CommandContext ctx)
        {
            MinigameStatusMessage result = await MinigameInstanceClient.GetInstanceAsync(typeof(TestMinigame)).Result.CreateNewSessionAsync(ctx, new TestMinigame(), 
            new MinigameSessionBuilder()
            {
                Name = null,
                AuthorType = MinigameSessionAuthorType.Guild,
                InvokeType = MinigameSessionInvokeType.Guild,
            }, "Este é o parametro", "Este é o parametro 2", "Este é o parametro 3");
        }

        [Command("StopMinigame")]
        public async Task StopMinigame(CommandContext ctx)
        {
            await MinigameInstanceClient.GetInstanceAsync(typeof(TestMinigame)).Result.DisconnectSessionAsync(ctx.Guild.Id);
        }

        [Command("ActiveMinigame")]
        public async Task ActiveMinigame(CommandContext ctx)
        {
            string activeSessions = "";

            foreach (MinigameSession session in MinigameInstanceClient.GetInstanceAsync(typeof(TestMinigame)).Result.Sessions.Values)
            {
                activeSessions += $"{session.Context.User.Username} \n";
            }

            await ctx.Channel.SendMessageAsync($"Sessões ativas no momentos: \n\n {activeSessions}");
        }
    }
}
