using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IAccount
    {
        void start();
        Account GetAccount(string id);
        void CreateAccount(Account account);
        string LoginAccount(Account account);
        bool CheckIfSignedIn(string Id);
        Account GetProfileInfo(string Id);
        List<order> GetOrders(string Id);
        List<order> GetOrdersIncoming(string Id);
        void AddFunds(float amount, string id);
        void AddPFP(PFP pfp, string Id);
        string GetPFP(string Id);
    }
}
