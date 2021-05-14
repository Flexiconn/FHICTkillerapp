using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IChat
    {
            Account GetAccount(string id);
            bool CheckIfSignedIn(string Id);
            void SendMessage(Chat Message, string id, string chatId);
            List<ClientChat> GetMessages(string chatId, string id);
            void createReport(string id, Report report);

    }
}
