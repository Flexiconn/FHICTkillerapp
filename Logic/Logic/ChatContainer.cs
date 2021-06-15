using Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;
using System.Linq;

namespace Logic
{
    public class ChatContainer
    {
        Contract.IChat IChat;
        public List<Logic.Models.LogicClientChat> GetChat(string ChatId, string sessionId)
        {
            List<Logic.Models.LogicClientChat> list = null;
            if (CheckIfSignedIn(sessionId))
            {
                list = LogicListDto.Messages(IChat.GetMessages(ChatId, IChat.GetAccountId(sessionId)));
                foreach (var t in list)
                {
                    if (IChat.GetAccount(IChat.GetAccountId(sessionId)).Id == ChatId)
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
                IChat.SendMessage(DateTime.Now, Guid.NewGuid().ToString() ,message, IChat.GetAccountId(SessionId), ChatId);
                return true;
            }
            return false;
        }

        public bool createReport(int reportReasonform, string comment, string chatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IChat.createReport(Guid.NewGuid().ToString(), IChat.GetAccountId(SessionId), Contract.reportTypes.post, Contract.reportReasons.scam, comment, chatId);

                return true;
            }
            return false;
        }

        public ChatContainer() {
            IChat = Factory.Factory.GetChatDAL();
        }
        public ChatContainer(string mode)
        {
            if (mode == "mock") {
                IChat = Factory.MockFactory.GetChatDAL();
            }
        }
    }
}
