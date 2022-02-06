using DSharpPlus;
using SAB.Business.Instances.Minigames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SAB.Managers
{
    /// <summary>
    /// Manager responsible for controlling the flow of minigames.
    /// </summary>
    public static class SABMinigameInstanceManager
    {
        /// <summary>
        /// Represents the instance of each loaded minigame module.
        /// </summary>
        public static SABMinigameInstance[] MinigameInstances { get; private set; }

        private static Assembly CurrentAssembly { get; set; }
        private static DiscordClient Client { get; set; }

        //======================================================//

        /// <summary>
        /// Starts the initial manager settings.
        /// </summary>
        /// <param name="client">The current Client connected.</param>
        public static async Task StartAsync(DiscordClient client)
        {
            Client = client;
            CurrentAssembly = typeof(SABMinigameInstanceManager).Assembly;

            await GetMinigamesInstances();
            await StartMinigamesModules();

            //==========================//
            MinigameConsoleDebug();
        }

        private static void MinigameConsoleDebug()
        {
            string minigames = "";
            foreach (SABMinigameInstance minigame in MinigameInstances)
            {
                minigames += $"> {minigame.MinigameModule.Name} \n";
            }

            Console.WriteLine($"\n=================\n" +

                              $"INSTANCES LOADED: \n\n" +

                              $"{minigames}" +

                              $"\n=================\n");
        }

        private static Task GetMinigamesInstances()
        {
            List<SABMinigameInstance> instances = new();
            foreach (Type minigame in CurrentAssembly.GetTypes().Where(x => x.IsSubclassOf(typeof(MinigameModule))))
            {
                if (minigame.Name.EndsWith("Minigame"))
                {
                    instances.Add(new SABMinigameInstance(minigame));
                }
            }

            MinigameInstances = instances.ToArray();
            return Task.CompletedTask;
        }
        private static Task StartMinigamesModules()
        {
            return Task.CompletedTask;
        }

        //======================================================//
        /// <summary>
        /// Get the active instance of a minigame.
        /// </summary>
        /// <param name="minigameModuleType">The name of the desired instance.</param>
        /// <returns>The required instance.</returns>
        public static async Task<SABMinigameInstance> GetInstanceAsync(Type minigameModuleType)
        {
            return await Task.FromResult(MinigameInstances.Where(x => x.MinigameModule == minigameModuleType).FirstOrDefault());
        }
    }
}
