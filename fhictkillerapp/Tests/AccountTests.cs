using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.MockFactory;
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
            new Logic.AccountLogic("mock").LoginAccount("Name","Password");
        }

        [TestMethod]
        public void CreateAccount()
        {
            new Logic.AccountLogic("mock").RegisterAccount("Name", "Password");

        }

        [TestMethod]
        public void AddFunds()
        {
            new Logic.AccountLogic("mock").AddfundsToAccount(20, "TestId");
        }

        [TestMethod]
        public void Myaccount()
        {
            new Logic.AccountLogic("mock").MyAccount("TestId");
        }

        [TestMethod]
        public void SetPfp()
        {
            new Logic.AccountLogic("mock").SetPFP(null, "TestId");
        }
    }
}
