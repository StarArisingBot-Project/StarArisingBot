using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAB.Business.Instances.Minigames
{
    public class SABMinigameSessionInfos
    {
        /// <summary>
        /// The session name;
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The way the session is invoked.
        /// </summary>
        public MinigameSessionInvokeType InvokeType { get; internal set; }

        /// <summary>
        /// The session author type.
        /// </summary>
        public MinigameSessionAuthorType AuthorType { get; internal set; }

        /// <summary>
        /// The configuration of players in the session.
        /// </summary>
        public PlayersConfigInfos PlayersInfos { get; internal set; }

        /// <summary>
        /// Base class for minigame player configuration.
        /// </summary>
        public class PlayersConfigInfos
        {
            /// <summary>
            /// Amount of players who will play.
            /// </summary>
            public IEnumerable<DiscordUser> UsersPlaying { get; internal set; }

            /// <summary>
            /// Minimum number of players for the minigame to start.
            /// </summary>
            public uint MinPlayers { get; internal set; }

            /// <summary>
            /// Maximum number of players the minigame can have.
            /// <br/><br/>
            /// <remark>-1 means infinite players.</remark>
            /// </summary>
            public int MaxPlayers { get; internal set; }
        }
    }
}
