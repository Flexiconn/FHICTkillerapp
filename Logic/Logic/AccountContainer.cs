using System;
using System.Collections.Generic;
using System.Text;
using Data;
using static Factory.Factory;
using Microsoft.AspNetCore.Http;
using Contract;
namespace Logic
{
    public class AccountContainer 
    {
        Contract.IAccount IAccount;

        public Logic.Models.LogicmyAccountModel GetMyAccountInfo(string SessionId)
        {
            
            if(CheckIfSignedIn(SessionId)){
                return new Logic.Models.LogicmyAccountModel( new Contract.Models.ContractmyAccountModel() { PFP = IAccount.GetPFP(IAccount.GetAccountId(SessionId)), ordersIncoming = IAccount.GetOrdersIncoming(IAccount.GetAccountId(SessionId)), ordersOutgoing = IAccount.GetOrders(IAccount.GetAccountId(SessionId)), account = IAccount.GetProfileInfo(IAccount.GetAccountId(SessionId)) }) ;
            }
            return new Models.LogicmyAccountModel();
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (IAccount.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public void RegisterAccount(string Password, string Name)
        {
            IAccount.CreateAccount(Password, Name, Guid.NewGuid().ToString());
        }


        public string LoginAccount(string Password, string Name)
        {
            if (IAccount.LoginAccountCheck(Password, Name).Id != null) {
                return IAccount.SetSessionId(IAccount.LoginAccountCheck(Password, Name).Id, Guid.NewGuid().ToString());
            }
            return "Failed";
        }


        public bool AddfundsToAccount(int amount, string SessionId)
        {
            if (CheckIfSignedIn(IAccount.GetAccountId(SessionId)))
            {
                IAccount.AddFunds(amount, IAccount.GetAccountId(SessionId));
                return true;
            }
            return false;
        }

        public bool SetPFP(IFormFile pfp,string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IAccount.AddPFP(FileControl.AddFileToSystem(pfp,$"pfp/{IAccount.GetAccountId(SessionId)}/"), IAccount.GetAccountId(SessionId), Guid.NewGuid().ToString());
                return true;
            }
            return false;
        }

        public bool cancelOrder(string OrderId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (IAccount.GetOrderOwner(OrderId) == IAccount.GetAccount(IAccount.GetAccountId(SessionId)).SessionId)
                {
                    if (IAccount.GetOrderStatus(OrderId) == "ordered")
                    {
                        IAccount.ChangeOrderStatus(OrderId, "cancelled");
                        return true;
                    }

                    if (IAccount.GetOrderStatus(OrderId) == "accepted")
                    {
                        IAccount.ChangeOrderStatus(OrderId, "cancelled");
                        return true;
                    }
                }
                else
                {
                    if (IAccount.GetOrderStatus(OrderId) == "ordered")
                    {
                        IAccount.ChangeOrderStatus(OrderId, "cancelled");
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        public bool AcceptOrder(string OrderId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (IAccount.GetOrderOwner(OrderId) == IAccount.GetAccount(IAccount.GetAccountId(SessionId)).SessionId)
                {
                    if (IAccount.GetOrderStatus(OrderId) == "ordered")
                    {
                        IAccount.ChangeOrderStatus(OrderId, "accepted");
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        public AccountContainer() {
            IAccount = Factory.Factory.GetAccountDAL();
        }

        public AccountContainer(string mode) {
            if (mode == "mock") {
                IAccount = Factory.MockFactory.GetAccountDAL();
            }
        }
    }
}
