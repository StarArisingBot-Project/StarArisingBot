using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAB.Business.Instances.Minigames
{
    /// <summary>
    /// Represents a type of status message.
    /// </summary>
    public enum StatusMessageType
    {
        /// <summary>
        /// It means success. The command executed did not have any errors. 
        /// </summary>
        Accepted,

        /// <summary>
        /// It means failure. The command executed had no error but something prevented it from working.
        /// </summary>
        Refused,

        /// <summary>
        /// It means error. The command executed had an error that the system could not handle. 
        /// </summary>
        Error,
    }

    /// <summary>
    /// Base class for communication between minigames. 
    /// </summary>
    public class MinigameStatusMessage
    {
        public MinigameStatusMessage(StatusMessageType statusMessages, string message)
        {
            Status = statusMessages;
            Message = message;
        }

        /// <summary>
        /// Success status the command obtained. 
        /// </summary>
        public StatusMessageType Status { get; private set; }

        /// <summary>
        /// Detailed message about execution. 
        /// </summary>
        public string Message { get; private set; }
    }
}
