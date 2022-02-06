using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Executors;
using System;
using System.Threading.Tasks;

namespace SAB.Behaviors
{
    public class SABCommandsBehavior : ICommandExecutor
    {
        public DiscordClient Client { get; set; }
        public CommandsNextExtension CommandsNext { get; set; }

        //===============================//

        public async Task ExecuteAsync(CommandContext ctx)
        {
            await CommandsNext.ExecuteCommandAsync(ctx);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
