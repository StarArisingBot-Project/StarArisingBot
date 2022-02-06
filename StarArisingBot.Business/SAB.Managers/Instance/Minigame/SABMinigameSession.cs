using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAB.Business.Instances.Minigames
{
    /// <summary>
    /// Represents a minigame that is running.
    /// </summary>
    public sealed class SABMinigameSession
    {
        /// <summary>
        /// The base module of the minigame executed.
        /// </summary>
        internal MinigameModule MinigameModule { get; private set; }

        /// <summary>
        /// The author ID of the current session.
        /// </summary>
        public ulong SectionID { get; internal set; }

        /// <summary>
        /// The current instance where the session is connected.
        /// </summary>
        internal SABMinigameInstance CurrentInstance { get; private set; }

        /// <summary>
        /// The context in which the session is running.
        /// </summary>
        public CommandContext Context { get; private set; }

        /// <summary>
        /// The date this session was created.
        /// </summary>
        public DateTime CreationTimestamp { get; private set; }

        /// <summary>
        /// Current session information.
        /// </summary>
        public SABMinigameSessionInfos SessionInfos { get; private set; }

        //======================================//
        public delegate void SessionDisconnected();
        public event SessionDisconnected OnSessionDisconnected;

        //======================================//

        public SABMinigameSession(CommandContext context, SABMinigameInstance currentInstance, MinigameModule minigameModule, MinigameSessionBuilder sessionBuilder = null)
        {
            Context = context;
            CurrentInstance = currentInstance;
            MinigameModule = minigameModule;
            MinigameModule.Session = this;

            CreationTimestamp = DateTime.Now;

            if(sessionBuilder == null)
            {
                SessionInfos = new SABMinigameSessionInfos();
            }
            else
            {
                SessionInfos = sessionBuilder.ToSessionInfos();
            }
        }
        internal async Task DisconnectAsync()
        {
            MinigameModule.StartCancelProcess();
            await CurrentInstance.RemoveSessionAsync(SectionID);
        }
    }
}
