using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IAccount
    {
        string GetAccountId(string SessionId);

        Contract.Models.ContractAccount GetAccount(string id);
        void CreateAccount(string Password, string Name, string Id);
        Contract.Models.ContractAccount LoginAccountCheck(string Password, string Name);
        string SetSessionId(string Id, string newSessionId);
        bool CheckIfSignedIn(string Id);
        Contract.Models.ContractAccount GetProfileInfo(string Id);
        List<Contract.Models.Contractorder> GetOrders(string Id);
        List<Contract.Models.Contractorder> GetOrdersIncoming(string Id);
        void AddFunds(float amount, string id);
        void AddPFP(IFormFile pfp, string Id);
        string GetPFP(string Id);
        string GetOrderStatus(string OrderId);
        string ChangeOrderStatus(string OrderId, string Status);
        string GetOwner(string OrderId);
    }
}
