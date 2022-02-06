using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAB.Business.Instances.Minigames
{
    /// <summary>
    /// The way the session is invoked.
    /// </summary>
    public enum MinigameSessionInvokeType
    {
        /// <summary>
        /// The session can be invoked without any limit.
        /// </summary>
        None,

        /// <summary>
        /// The invoked session is restricted by guilds only.
        /// </summary>
        Guild,

        /// <summary>
        /// The invoked session is restricted to channels only.
        /// </summary>
        Channel,

        /// <summary>
        /// The invoked session is restricted to users only. 
        /// </summary>
        User,
    }

    /// <summary>
    /// The author type of a session.
    /// </summary>
    public enum MinigameSessionAuthorType
    {
        /// <summary>
        /// The Author of the session will be the guild.
        /// </summary>
        Guild,

        /// <summary>
        /// The Author of the session will be the channel on which it was invoked.
        /// </summary>
        Channel,

        /// <summary>
        /// The Author of the session will be the user who invoked it.
        /// </summary>
        User,
    }

    /// <summary>
    /// Class for creating sessions.
    /// </summary>
    public class MinigameSessionBuilder
    {
        /// <summary>
        /// The session name;
        /// </summary>
        public Optional<string> Name { get; set; }

        /// <summary>
        /// The way the session is invoked.
        /// </summary>
        public MinigameSessionInvokeType InvokeType { get; set; }

        /// <summary>
        /// The session author type.
        /// </summary>
        public MinigameSessionAuthorType AuthorType { get; set; }

        /// <summary>
        /// The configuration of players in the session.
        /// </summary>
        public PlayersConfig PlayersSettings { get; set; }

        /// <summary>
        /// Base class for minigame player configuration.
        /// </summary>
        public class PlayersConfig
        {
            /// <summary>
            /// Amount of players who will play.
            /// </summary>
            public IEnumerable<DiscordUser> UsersPlaying { get; set; }

            /// <summary>
            /// Minimum number of players for the minigame to start.
            /// </summary>
            public uint MinPlayers { get; set; }

            /// <summary>
            /// Maximum number of players the minigame can have.
            /// <br/><br/>
            /// <remark>-1 means infinite players.</remark>
            /// </summary>
            public int MaxPlayers { get; set; }
        }

        //===============================================//
        /// <summary>
        /// Converts the constructor to readable information.
        /// </summary>
        /// <returns>Information for reading.</returns>
        internal SABMinigameSessionInfos ToSessionInfos()
        {
            SABMinigameSessionInfos infos = new()
            {
                Name = Name.Value,
                InvokeType = InvokeType,
                AuthorType = AuthorType,

            };

            if (PlayersSettings == null)
            {
                infos.PlayersInfos = new SABMinigameSessionInfos.PlayersConfigInfos();
            }
            else
            {
                infos.PlayersInfos = new SABMinigameSessionInfos.PlayersConfigInfos()
                {
                    UsersPlaying = PlayersSettings.UsersPlaying,
                    MinPlayers = PlayersSettings.MinPlayers,
                    MaxPlayers = PlayersSettings.MaxPlayers,
                };
            }

            return infos;
        }
    }
}
