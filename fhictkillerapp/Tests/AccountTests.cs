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
            new Logic.Account("mock").LoginAccount("Name","Password");
        }

        [TestMethod]
        public void CreateAccount()
        {
            new Logic.Account("mock").RegisterAccount("Name", "Password");

        }

        [TestMethod]
        public void AddFunds()
        {
            new Logic.Account("mock").AddfundsToAccount(20, "TestId");
        }

        [TestMethod]
        public void Myaccount()
        {
            new Logic.Account("mock").MyAccount("TestId");
        }

        [TestMethod]
        public void SetPfp()
        {
            new Logic.Account("mock").SetPFP(null, "TestId");
        }
    }
}
