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
            new Logic.AccountContainer("mock").LoginAccount("Name","Password");
        }

        [TestMethod]
        public void CreateAccount()
        {
            new Logic.AccountContainer("mock").RegisterAccount("Name", "Password");

        }

        [TestMethod]
        public void AddFunds()
        {
            new Logic.AccountContainer("mock").AddfundsToAccount(20, "TestId");
        }

        [TestMethod]
        public void Myaccount()
        {
            new Logic.AccountContainer("mock").MyAccount("TestId");
        }

        [TestMethod]
        public void SetPfp()
        {
            new Logic.AccountContainer("mock").SetPFP(null, "TestId");
        }
    }
}
