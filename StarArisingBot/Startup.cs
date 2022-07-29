using DSharpPlus;
using StarArisingBot.Launchers;
using System.Threading.Tasks;

namespace StarArisingBot
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
