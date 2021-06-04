using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.Factory;
using Data;
using Common;
using Contract;
using System;

namespace Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestLogin()
        {
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();

            account.CreateAccount("ja", "ja");
            new Logic.Account().LoginAccount("ja", "ja");
            test.cleanAccount(account.LoginAccount("ja", "ja"));
        }

        [TestMethod]
        public void CreateAccount()
        {
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();

            new Logic.Account().RegisterAccount("ja", "ja");
            test.cleanAccount(account.LoginAccount("ja", "ja"));
        }

        [TestMethod]
        public void AddFunds()
        {
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            new Logic.Account().AddfundsToAccount(10, account.LoginAccount("ja","ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));
        }

        [TestMethod]
        public void Myaccount()
        {
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            new Logic.Account().MyAccount(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));
        }

        [TestMethod]
        public void SetPfp()
        {
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            new Logic.Account().SetPFP(null, account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));
        }
    }
}
