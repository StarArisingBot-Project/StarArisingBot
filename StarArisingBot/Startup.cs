using DSharpPlus;
using SAB.Launchers;
using System;
using System.Threading.Tasks;

namespace SAB.System
{
    public class Startup
    {
        private DiscordClient CurrentClient { get; set; }

        public async Task RunAsync()
        {
            Console.WriteLine("=> Inciando RunAsync()");

            CurrentClient = await SABBotLauncher.StartBotSettingsAsync();
            await SABEventsLauncher.StartDiscordEventsAsync(CurrentClient);

            Console.WriteLine("=> eventos pronto.");
            Console.WriteLine("=> iniciando bot.");

            //==================================//

            await CurrentClient.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
