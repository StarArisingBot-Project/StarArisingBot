using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Executors;
using StarArisingBot.Managers;
using StarArisingBot.Models.Users;
using DSharpPlus;

namespace StarArisingBot.Executors
{
    public class SABCommandExecutor : ICommandExecutor
    {
        Task ICommandExecutor.ExecuteAsync(CommandContext ctx)
        {
            if(!SABBotUsersManager.UserTriedUseCommand(ctx.User).Result)
            {
                _ = Task.Run(() => ctx.RespondAsync($"🔥 》<@{ctx.User.Id}> **Você está usando meus comandos rápido Demais!**\n" +
                                                    $"Espere **{SABBotUsersManager.GetUserHistoryAsync(ctx.User).Result.DelayTimeRemaining} Segundos** antes de usar um comando novamente!"));
                return Task.CompletedTask;
            }

            _ = Task.Run(() => ctx.CommandsNext.ExecuteCommandAsync(ctx).ConfigureAwait(false));
            return Task.CompletedTask;
        }

        void IDisposable.Dispose()
        { }
    }
}
