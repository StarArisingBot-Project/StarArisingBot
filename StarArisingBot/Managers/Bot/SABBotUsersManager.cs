using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using StarArisingBot.Models.Users;

namespace StarArisingBot.Managers
{
    public static class SABBotUsersManager
    {
        private readonly static Dictionary<ulong, UserHistory> userHistories = new();

        public static Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public static Task<bool> UserTriedUseCommand(DiscordUser user)
        {
            UserHistory? userHistory = userHistories.GetValueOrDefault(user.Id);
            if(userHistory == null)
            {
                userHistories.TryAdd(user.Id, new UserHistory(user));
                return Task.FromResult(true);
            }

            return Task.FromResult(userHistory.TryUseCommand());
        }

        public static Task<UserHistory?> GetUserHistoryAsync(DiscordUser user)
        {
            return Task.FromResult(userHistories.GetValueOrDefault(user.Id));
        }
    }
}
