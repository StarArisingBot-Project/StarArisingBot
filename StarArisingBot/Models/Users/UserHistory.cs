using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarArisingBot.Models.Users
{
    public class UserHistory
    {
        public DiscordUser User { get; private set; }
        public DateTime DalayTime { get; private set; }
        public int DelayTimeRemaining
        {
            get
            {
                return Math.Clamp((int)Math.Round((DalayTime - DateTime.Now).TotalSeconds), 0, int.MaxValue);
            }
        }
        public bool IsDelay
        {
            get
            {
                return DelayTimeRemaining > 0;
            }
        }

        public UserHistory(DiscordUser user)
        {
            User = user;
        }
        public bool TryUseCommand()
        {
            if (IsDelay)
            {
                DalayTime = DateTime.Now.AddSeconds((DelayTimeRemaining + 1) * 2);
                return false;
            }

            DalayTime = DateTime.Now.AddSeconds(2f);
            return true;
        }
    }
}
