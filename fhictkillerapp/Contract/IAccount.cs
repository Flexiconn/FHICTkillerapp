
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IAccount
    {
        string GetAccountId(string SessionId);

        Contract.Models.Account GetAccount(string id);
        void CreateAccount(string Password, string Name);
        string LoginAccount(string Password, string Name);
        bool CheckIfSignedIn(string Id);
        Contract.Models.Account GetProfileInfo(string Id);
        List<Contract.Models.order> GetOrders(string Id);
        List<Contract.Models.order> GetOrdersIncoming(string Id);
        void AddFunds(float amount, string id);
        void AddPFP(IFormFile pfp, string Id);
        string GetPFP(string Id);
        string GetOrderStatus(string OrderId);
        string ChangeOrderStatus(string OrderId, string Status);
        string GetOwner(string OrderId);
    }
}
