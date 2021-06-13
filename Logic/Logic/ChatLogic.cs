using Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;
using System.Linq;

namespace Logic
{
    public class ChatLogic
    {
        Contract.IChat IChat;
        public List<Logic.Models.LogicClientChat> GetChat(string id, string sessionId)
        {
            List<Logic.Models.LogicClientChat> list = null;
            if (CheckIfSignedIn(sessionId))
            {
                list = LogicListDto.Messages(IChat.GetMessages(id, IChat.GetAccountId(sessionId)));
                foreach (var t in list)
                {
                    if (IChat.GetAccount(IChat.GetAccountId(sessionId)).Id == id)
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
            if (IChat.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public bool SendMessage(string message, string ChatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IChat.SendMessage(message, IChat.GetAccountId(SessionId), ChatId);
                return true;
            }
            return false;
        }

        public bool createReport(int reportReasonform, string comment, string chatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IChat.createReport(IChat.GetAccountId(SessionId), Contract.reportTypes.post, Contract.reportReasons.scam, comment, chatId);

                return true;
            }
            return false;
        }

        public ChatLogic() {
            IChat = Factory.Factory.GetChatDAL();
        }
        public ChatLogic(string mode)
        {
            if (mode == "mock") {
                IChat = Factory.MockFactory.GetClassChat();
            }
        }
    }
}
