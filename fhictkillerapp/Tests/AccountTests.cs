using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.Factory;
using Data;
using Data.Interfaces;
using Common;

namespace Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestLogin()
        {
            IAccount account = GetClassAccount();
            account.LoginAccount(new Common.Models.Account() { Name = "test", Password = "test" });
        }

        [TestMethod]
        public void CreateAccount()
        {
            IAccount account = GetClassAccount();
            account.CreateAccount(new Common.Models.Account() { Name = "test", Password = "test", Balance = "0"});
        }

        [TestMethod]
        public void GetPFP()
        {
            IAccount account = GetClassAccount();
            account.GetPFP(account.LoginAccount(new Common.Models.Account() { Name = "test", Password = "test" }));
        }

        [TestMethod]
        public void GetProfileInfo()
        {
            IAccount account = GetClassAccount();
            account.GetProfileInfo(account.LoginAccount(new Common.Models.Account() { Name = "test", Password = "test" }));
        }

        [TestMethod]
        public void GetIncomingOrders()
        {
            IAccount account = GetClassAccount();
            account.GetOrdersIncoming(account.LoginAccount(new Common.Models.Account() { Name = "test", Password = "test" }));
        }

        [TestMethod]
        public void GetOutgoingOrders()
        {
            IAccount account = GetClassAccount();
            account.GetOrders(account.LoginAccount(new Common.Models.Account() { Name = "test", Password = "test" }));
        }

        [TestMethod]
        public void AddfundsTest()
        {
            IAccount account = GetClassAccount();
            account.AddFunds(10.0f,account.LoginAccount(new Common.Models.Account() { Name = "test", Password = "test" }));
        }

        [TestMethod]
        public void CheckIfSignedIn()
        {
            IAccount account = GetClassAccount();
            account.CheckIfSignedIn(account.LoginAccount(new Common.Models.Account() { Name = "test", Password = "test" }));
        }

        [TestMethod]
        public void GetAccountTest()
        {
            IAccount account = GetClassAccount();
            account.GetAccount(account.LoginAccount(new Common.Models.Account() { Name = "test", Password = "test" }));
        }
    }
}
