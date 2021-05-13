﻿using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Account
    {


        public myAccountModel MyAccount(string SessionId)
        {
            
            if(CheckIfSignedIn(SessionId)){
                return new myAccountModel() { PFP = Querries.GetPFP(SessionId), ordersIncoming = Querries.GetOrdersIncoming(SessionId), ordersOutgoing = Querries.GetOrders(SessionId), account = Querries.GetProfileInfo(SessionId) };
            }
            return null;
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (Querries.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public void RegisterAccount(Account account)
        {
            Querries.CreateAccount(account);
        }


        public string LoginAccount(Account account)
        {
            return Querries.LoginAccount(account);
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

        public bool SetPFP(PFP pfpModel,string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.AddPFP(pfpModel, SessionId);
                return true;
            }
            return false;
        }

    }
}
