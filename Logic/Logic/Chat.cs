using Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;
using System.Linq;

namespace Logic
{
    public class Chat
    {
        Contract.IChat Querries;
        public List<Logic.Models.LogicClientChat> Index(string id, string sessionId)
        {
            List<Logic.Models.LogicClientChat> list = null;
            if (CheckIfSignedIn(sessionId))
            {
                list = LogicListDto.Messages(Querries.GetMessages(id, Querries.GetAccountId(sessionId)));
                foreach (var t in list)
                {
                    if (Querries.GetAccount(Querries.GetAccountId(sessionId)).Id == id)
                    {
                        t.Sender = true;
                    }
                    else
                    {
                        t.Sender = false;
                    }
                }
            }
            var messages = from p in list
                           orderby p.DateTime
                          select p;
            return (List<Logic.Models.LogicClientChat>)messages.ToList();
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
                Querries.SendMessage(message, Querries.GetAccountId(SessionId), ChatId);
                return true;
            }
            return false;
        }

        public bool createReport(int reportReasonform, string comment, string chatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.createReport(Querries.GetAccountId(SessionId), Contract.reportTypes.post, Contract.reportReasons.scam, comment, chatId);

                return true;
            }
            return false;
        }

        public Chat() {
            Querries = GetClassChat();
        }
        public Chat(string mode)
        {
            if (mode == "mock") {
                Querries = Factory.MockFactory.GetClassChat();
            }
        }
    }
}
