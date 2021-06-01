using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.Factory;
using Data;
using Common;
using Contract;

namespace Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestLogin()
        {
            IAccount account = GetClassAccount();
            account.LoginAccount("test","test" );
        }

        [TestMethod]
        public void CreateAccount()
        {
            IAccount account = GetClassAccount();
            account.CreateAccount("test","test");
        }

        [TestMethod]
        public void GetPFP()
        {
            IAccount account = GetClassAccount();
            account.GetPFP(account.LoginAccount("test", "test"));
        }

        [TestMethod]
        public void GetProfileInfo()
        {
            IAccount account = GetClassAccount();
            account.GetProfileInfo(account.LoginAccount("test", "test"));
        }

        [TestMethod]
        public void GetIncomingOrders()
        {
            IAccount account = GetClassAccount();
            account.GetOrdersIncoming(account.LoginAccount("test", "test"));
        }

        [TestMethod]
        public void GetOutgoingOrders()
        {
            IAccount account = GetClassAccount();
            account.GetOrders(account.LoginAccount("test", "test"));
        }

        [TestMethod]
        public void AddfundsTest()
        {
            IAccount account = GetClassAccount();
            account.AddFunds(10.0f, account.LoginAccount("test", "test"));
        }

        [TestMethod]
        public void CheckIfSignedIn()
        {
            IAccount account = GetClassAccount();
            account.CheckIfSignedIn(account.LoginAccount("test", "test"));
        }

        [TestMethod]
        public void GetAccountTest()
        {
            IAccount account = GetClassAccount();
            account.GetAccount(account.LoginAccount("test", "test"));
        }
    }
}
