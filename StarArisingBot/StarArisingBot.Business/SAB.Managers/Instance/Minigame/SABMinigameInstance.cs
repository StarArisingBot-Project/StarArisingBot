using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAB.Business.Instances.Minigames
{
    /// <summary>
    /// The instance of a minigame.
    /// </summary>
    public sealed class SABMinigameInstance
    {
        /// <summary>
        /// The minigame module.
        /// </summary>
        public Type MinigameModule { get; private set; }

        /// <summary>
        /// Active sessions of the current Instance.
        /// </summary>
        public Dictionary<ulong, SABMinigameSession> Sessions { get; private set; }

        public SABMinigameInstance(Type minigameModule)
        {
            MinigameModule = minigameModule;
            Sessions = new();
        }

        //=============================================//

        /// <summary>
        /// Creates an active session in the current instance. 
        /// </summary>
        /// <param name="context">The context in which the session is being created.</param>
        /// <param name="minigame">The class of the mini game that is logged in.</param>
        /// <param name="minigameParams">The parameters that will be passed to the minigame.</param>
        /// <returns>The status of success.</returns>
        public async Task<MinigameStatusMessage> CreateNewSessionAsync(CommandContext context, MinigameModule minigame, MinigameSessionBuilder builder, params dynamic[] minigameParams)
        {
            SABMinigameSession activeSessionFound = null;
            switch (builder.AuthorType)
            {
                case MinigameSessionAuthorType.Guild:
                    activeSessionFound = Sessions.Where(x => x.Key == context.Guild.Id).FirstOrDefault().Value;
                    break;

                case MinigameSessionAuthorType.Channel:
                    activeSessionFound = Sessions.Where(x => x.Key == context.Channel.Id).FirstOrDefault().Value;
                    break;

                case MinigameSessionAuthorType.User:
                    activeSessionFound = Sessions.Where(x => x.Key == context.User.Id).FirstOrDefault().Value;
                    break;
            }

            if (activeSessionFound == null)
            {
                if(builder.AuthorType == MinigameSessionAuthorType.Guild)
                {
                    CreateSession(context.Guild.Id);
                }
                else if(builder.AuthorType == MinigameSessionAuthorType.Channel)
                {
                    CreateSession(context.Channel.Id);
                }
                else if (builder.AuthorType == MinigameSessionAuthorType.User)
                {
                    CreateSession(context.User.Id);
                }

                return await Task.FromResult(new MinigameStatusMessage(StatusMessageType.Accepted, "Session created successfully."));
            }
            else
            {
                return await Task.FromResult(new MinigameStatusMessage(StatusMessageType.Refused, "The session already exists."));
            }

            void CreateSession(ulong id)
            {
                Sessions.Add(id, new SABMinigameSession(context, this, minigame, builder));
                var session = Sessions.GetValueOrDefault(id);

                session.SectionID = id;
                session.MinigameModule.Initialize(context, minigameParams);
            }
        }

        /// <summary>
        /// Remove and disconnect an active session from the current Instance.
        /// </summary>
        /// <param name="sectionId">The ID of the author responsible for creating the session.</param>
        public async Task DisconnectSessionAsync(ulong sectionId)
        {
            SABMinigameSession session = await GetSessionAsync(sectionId);

            await session.DisconnectAsync();
            Sessions.Remove(session.SectionID);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Remove an active session from the current Instance.
        /// </summary>
        /// <param name="sectionId">The ID of the author responsible for creating the session.</param>
        public async Task RemoveSessionAsync(ulong sectionId)
        {
            SABMinigameSession session = await GetSessionAsync(sectionId);
            Sessions.Remove(session.SectionID);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Get an active session.
        /// </summary>
        /// <param name="authorID">The ID of the author responsible for creating the session.</param>
        /// <returns>Returns an active session.</returns>
        public async Task<SABMinigameSession> GetSessionAsync(ulong sectionID)
        {
            return await Task.FromResult(Sessions.Where(x => x.Value.Context.Guild.Id == sectionID).FirstOrDefault().Value);
        }

        /// <summary>
        /// Get an active session.
        /// </summary>
        /// <param name="authorType">The session author type.</param>
        /// <param name="authorID">The ID of the author responsible for creating the session.</param>
        /// <returns>Returns an active session.</returns>
        public async Task<SABMinigameSession> GetSessionAsync(MinigameSessionAuthorType authorType, ulong sectionID)
        {
            return await Task.FromResult(Sessions.Where(x => x.Value.SessionInfos.AuthorType == authorType).Where(x => x.Value.SectionID == sectionID).FirstOrDefault().Value);
        }
    }
}
