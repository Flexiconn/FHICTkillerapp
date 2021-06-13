using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IChat
    {
        string GetAccountId(string sessionId);
        Contract.Models.ContractAccount GetAccount(string id);
        bool CheckIfSignedIn(string Id);
        void SendMessage(DateTime dateTime, string messageId, string Message, string Id, string chatid);
        List<Contract.Models.ContractClientChat> GetMessages(string chatId, string id);
        void createReport(string reportId, string id, Contract.reportTypes reportType, reportReasons reportReason, string comment, string reportedId);
    }
}
