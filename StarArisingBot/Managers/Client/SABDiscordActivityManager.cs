using DSharpPlus;
using DSharpPlus.Entities;
using SAB.System;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SAB.Managers
{
    public static class SABDiscordActivityManager
    {
        private static readonly DiscordActivity[] activityMessages =
        {
                new DiscordActivity("Pessoas usando :>Help para saber meus comandos.", ActivityType.ListeningTo),
                new DiscordActivity("Meu MiniGame :>RPGStart.", ActivityType.Playing),
                new DiscordActivity("Precisando de ajuda? Meu criador é o Starciad#0381!"),
                new DiscordActivity($"Estou na versão {SABApplication.Version}!", ActivityType.Watching),
                new DiscordActivity("Pessoas usando o :>Daily.", ActivityType.ListeningTo),
        };

        public static async Task StartAsync(DiscordClient client)
        {
            Thread activityChangerThread = new(() => ActivityChanger(client));
            activityChangerThread.Start();

            await Task.CompletedTask;
        }
        private static async void ActivityChanger(DiscordClient client)
        {
            while (true)
            {
                await client.UpdateStatusAsync(activityMessages[new Random().Next(0, activityMessages.Length)]);
                Thread.Sleep(10000);
            }
        }
    }
}
