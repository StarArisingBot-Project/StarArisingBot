using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarArisingBot.Managers
{
    public static class SABBotUptimeManager
    {
        private static DateTime startDateTime;

        public static async Task StartAsync()
        {
            startDateTime = DateTime.Now;

            await Task.CompletedTask;
        }

        public static TimeSpan GetUptime()
        {
            return DateTime.Now - startDateTime;
        }
    }
}
