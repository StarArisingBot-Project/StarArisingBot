using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SAB.Business.Instances.Minigames
{
    /// <summary>
    /// Represents a minigame module.
    /// <br/><br/>
    /// For a class to be considered a module it must always end with "Minigame". 
    /// </summary>
    public abstract class MinigameModule
    {
        /// <summary>
        /// The Context in which the minigame is being played out.
        /// </summary>
        protected CommandContext Context { get; private set; }

        /// <summary>
        /// The bot client.
        /// </summary>
        protected DiscordClient Client { get; private set; }

        /// <summary>
        /// The current minigame session.
        /// </summary>
        public SABMinigameSession Session { get; set; }

        //======================================//
        public delegate void MinigameStarted();
        public delegate Task MinigameCanceled();

        /// <summary>
        /// Event triggered after minigame launch. 
        /// </summary>
        public event MinigameStarted OnMinigameStarted;

        /// <summary>
        /// Event activated after canceling the minigame.
        /// </summary>
        public event MinigameCanceled OnMinigameCanceled;

        //======================================//
        /// <summary>
        /// The minigame startup process begins.
        /// </summary>
        /// <param name="context">The current Context in which the minigame is being invoked. </param>
        /// <param name="minigameParams">The parameters that will go to the minigame.</param>
        internal void Initialize(CommandContext context, params dynamic[] minigameParams)
        {
            //Set Context
            Context = context;
            Client = context.Client;

            //Start Thread
            new Thread(() => StartMinigameThread(minigameParams)).Start();

            //Start Events
            OnMinigameCanceled += OnCanceled;
            OnMinigameStarted?.Invoke();
        }
        private async void StartMinigameThread(params dynamic[] minigameParams)
        {
            await OnStarted(minigameParams).ConfigureAwait(false);
        }

        //============================//

        /// <summary>
        /// Starts the process of canceling the minigame.
        /// </summary>
        internal async void StartCancelProcess()
        {
            await OnMinigameCanceled?.Invoke();
        }

        //============================//
        /// <summary>
        /// The start of the minigame. 
        /// </summary>
        /// <param name="minigameParams">The minigame parameters.</param>
        protected abstract Task OnStarted(params dynamic[] minigameParams);

        /// <summary>
        /// When a cancel request is initiated, this method will be executed.
        /// </summary>
        protected abstract Task OnCanceled();

        /// <summary>
        /// Finish the current minigame. 
        /// </summary>
        protected async Task FinalizeMinigameAsync()
        {
            await Session.DisconnectAsync();
        }
    }
}
