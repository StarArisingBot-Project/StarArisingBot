using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;
using StarArisingBot.MinigameEngine;
using StarArisingBot.Minigames.HungerGames;
using StarArisingBot.Managers;

namespace StarArisingBot.Launchers
{
    public static class SABEventsLauncher
    {
        public static async Task StartDiscordEventsAsync(DiscordClient client)
        {
            await StartClientEventsAsync(client);
            await Task.CompletedTask;
        }
        private static Task StartClientEventsAsync(DiscordClient client)
        {
            client.Ready += DiscordEvents.ClientReady;

            return Task.CompletedTask;
        }

        private static class DiscordEvents
        {
            //Discord Events
            public static async Task ClientReady(DiscordClient sender, ReadyEventArgs e)
            {
                await StartManagers(sender).ConfigureAwait(false);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(@"\\ BOT READY //");
            }

            //Bot Components Start
            private static async Task StartManagers(DiscordClient client)
            {
                //Discord Activity
                await SABDiscordActivityManager.StartAsync(client).ConfigureAwait(false);
            }
        }
    }
}
