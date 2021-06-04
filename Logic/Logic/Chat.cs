using Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;


namespace Logic
{
    public class Chat
    {
        Contract.IChat Querries = GetClassChat();
        public List<Logic.Models.LogicClientChat> Index(string id, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                return LogicListDto.Messages(Querries.GetMessages(id, SessionId));
            }
            return new List<Logic.Models.LogicClientChat>();
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (Querries.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public bool SendMessage(string message, string ChatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.SendMessage(message, SessionId, ChatId);
                return true;
            }
            return false;
        }

        public bool createReport(int reportReasonform, string comment, string chatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.createReport(SessionId, Contract.reportTypes.post, Contract.reportReasons.scam, comment, chatId);

                return true;
            }
            return false;
        }
    }
}
