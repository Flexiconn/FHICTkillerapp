using System;
using System.Collections.Generic;
using System.Text;
using Data;
using static Factory.Factory;
using Microsoft.AspNetCore.Http;
using Contract;
namespace Logic
{
    public class Account 
    {
        Contract.IAccount Querries;

        public Logic.Models.LogicmyAccountModel MyAccount(string SessionId)
        {
            
            if(CheckIfSignedIn(SessionId)){
                return new Logic.Models.LogicmyAccountModel( new Contract.Models.myAccountModel() { PFP = Querries.GetPFP(Querries.GetAccountId(SessionId)), ordersIncoming = Querries.GetOrdersIncoming(Querries.GetAccountId(SessionId)), ordersOutgoing = Querries.GetOrders(Querries.GetAccountId(SessionId)), account = Querries.GetProfileInfo(Querries.GetAccountId(SessionId)) }) ;
            }
            return new Models.LogicmyAccountModel();
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (Querries.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public void RegisterAccount(string Password, string Name)
        {
            Querries.CreateAccount(Password, Name);
        }


        public string LoginAccount(string Password, string Name)
        {
            return Querries.LoginAccount(Password, Name);
        }


        public bool AddfundsToAccount(int amount, string SessionId)
        {
            if (CheckIfSignedIn(Querries.GetAccountId(SessionId)))
            {
                Querries.AddFunds(amount, Querries.GetAccountId(SessionId));
                return true;
            }
            return false;
        }

        public bool SetPFP(IFormFile pfp,string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.AddPFP(pfp, Querries.GetAccountId(SessionId));
                return true;
            }
            return false;
        }

        public bool cancelOrder(string OrderId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (Querries.GetOwner(OrderId) == Querries.GetAccount(Querries.GetAccountId(SessionId)).SessionId)
                {
                    if (Querries.GetOrderStatus(OrderId) == "ordered")
                    {
                        Querries.ChangeOrderStatus(OrderId, "cancelled");
                        return true;
                    }

                    if (Querries.GetOrderStatus(OrderId) == "accepted")
                    {
                        Querries.ChangeOrderStatus(OrderId, "cancelled");
                        return true;
                    }
                }
                else
                {
                    if (Querries.GetOrderStatus(OrderId) == "ordered")
                    {
                        Querries.ChangeOrderStatus(OrderId, "cancelled");
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
                if (Querries.GetOwner(OrderId) == Querries.GetAccount(Querries.GetAccountId(SessionId)).SessionId)
                {
                    if (Querries.GetOrderStatus(OrderId) == "ordered")
                    {
                        Querries.ChangeOrderStatus(OrderId, "accepted");
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        public Account() {
            Querries = GetClassAccount();
        }

        public Account(string mode) {
            if (mode == "mock") {
                Querries = Factory.MockFactory.GetClassAccount();
            }
        }
    }
}
