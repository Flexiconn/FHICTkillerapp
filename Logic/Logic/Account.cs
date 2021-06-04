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

        Contract.IAccount Querries = GetClassAccount();

        public Logic.Models.LogicmyAccountModel MyAccount(string SessionId)
        {
            
            if(CheckIfSignedIn(SessionId)){
                Console.WriteLine("Data: " + Querries.GetOrdersIncoming(SessionId).Count + Querries.GetOrders(SessionId).Count);
                return new Logic.Models.LogicmyAccountModel( new Contract.Models.myAccountModel() { PFP = Querries.GetPFP(SessionId), ordersIncoming = Querries.GetOrdersIncoming(SessionId), ordersOutgoing = Querries.GetOrders(SessionId), account = Querries.GetProfileInfo(SessionId) }) ;
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
            if (CheckIfSignedIn(SessionId))
            {
                Querries.AddFunds(amount, SessionId);
                return true;
            }
            return false;
        }

        public bool SetPFP(IFormFile pfp,string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.AddPFP(pfp, SessionId);
                return true;
            }
            return false;
        }

        public bool cancelOrder(string OrderId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (Querries.GetOwner(OrderId) == Querries.GetAccount(SessionId).SessionId)
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
                if (Querries.GetOwner(OrderId) == Querries.GetAccount(SessionId).SessionId)
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

    }
}
