using Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;
using System.Linq;
using Logic.Models;

namespace Logic
{
    public class ChatContainer
    {
        readonly Contract.IChat IChat;
        readonly Contract.IAccount IAccount;

        public List<Logic.Models.LogicClientChat> GetChat(string ChatId, string sessionId)
        {
            List<Logic.Models.LogicClientChat> list = null;
            if (CheckIfSignedIn(sessionId))
            {
                list = LogicListDto.Messages(IChat.GetMessages(ChatId, IAccount.GetAccountId(sessionId)));
                foreach (var t in list)
                {
                    if (IAccount.GetAccountId(sessionId) == t.Account.GetId())
                    {
                        t.SetSender(true);
                    }
                    else
                    {
                        t.SetSender(false);
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
            if (IAccount.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public bool SendMessage(string message, string ChatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IChat.SendMessage(DateTime.Now, Guid.NewGuid().ToString() ,message, IAccount.GetAccountId(SessionId), ChatId);
                return true;
            }
            return false;
        }

        

        public ChatContainer() {
            IChat = Factory.Factory.GetChatDAL();
            IAccount = Factory.Factory.GetAccountDAL();
        }
        public ChatContainer(string mode)
        {
            if (mode == "mock") {
                IChat = Factory.MockFactory.GetChatDAL();
                IAccount = Factory.MockFactory.GetAccountDAL();
            }
        }
    }
}
