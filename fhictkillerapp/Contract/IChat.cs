using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IChat
    {
        Contract.Models.ContractAccount GetAccount(string id);
        void SendMessage(DateTime dateTime, string messageId, string Message, string Id, string chatid);
        List<Contract.Models.ContractClientChat> GetMessages(string chatId, string id);
    }
}
