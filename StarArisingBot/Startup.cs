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
            CurrentClient = await SABBotLauncher.StartBotSettingsAsync();
            await SABEventsLauncher.StartDiscordEventsAsync(CurrentClient);

            //==================================//

            await CurrentClient.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
