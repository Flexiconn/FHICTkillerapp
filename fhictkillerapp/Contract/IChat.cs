using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IChat
    {
        string GetAccountId(string sessionId);
        Contract.Models.Account GetAccount(string id);
        bool CheckIfSignedIn(string Id);
        void SendMessage(string Message, string id, string chatid);
        List<Contract.Models.ClientChat> GetMessages(string chatId, string id);
        void createReport(string id, Contract.reportTypes reportType, reportReasons reportReason, string comment, string reportedId);
    }
}
