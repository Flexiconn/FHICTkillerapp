using System;
using System.Collections.Generic;
using System.Text;
using Data;
using static Factory.Factory;
using Microsoft.AspNetCore.Http;
using Contract;
using Logic.Models;

namespace Logic
{
    public class AccountContainer 
    {
        readonly Contract.IAccount IAccount;

        public Logic.Models.LogicmyAccountModel GetMyAccountInfo(string SessionId)
        {
            
            if(CheckIfSignedIn(SessionId)){
                return new Logic.Models.LogicmyAccountModel( new Contract.Models.ContractmyAccountModel(IAccount.GetPFP(IAccount.GetAccountId(SessionId)),IAccount.GetAccount(IAccount.GetAccountId(SessionId))));
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
            if (IAccount.LoginAccountCheck(Password, Name).Id != null && IAccount.LoginAccountCheck(Password, Name).Id.Length > 4) {
                return IAccount.SetSessionId(new LogicAccount(IAccount.LoginAccountCheck(Password, Name)).GetId(), Guid.NewGuid().ToString());
            }
            return "Failed";
        }


        public bool AddfundsToAccount(int amount, string SessionId)
        {
            if (amount > 0)
            {
                if (CheckIfSignedIn(IAccount.GetAccountId(SessionId)))
                {
                    IAccount.AddFunds(amount, IAccount.GetAccountId(SessionId));
                    return true;
                }
            }
            return false;
        }

        public bool SetPFP(IFormFile pfp,string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (pfp != null || SessionId == "NaN")
                {
                    IAccount.AddPFP(FileControl.AddFileToSystem(pfp, $"pfp/{IAccount.GetAccountId(SessionId)}/"), IAccount.GetAccountId(SessionId), Guid.NewGuid().ToString());
                    return true;
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
