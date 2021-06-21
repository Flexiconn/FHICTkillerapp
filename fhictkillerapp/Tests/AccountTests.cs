using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.MockFactory;
using Data;
using Contract;
using System;

namespace Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void LoginAccount()
        {
            bool test = false;
            var login = new Logic.AccountContainer("mock").LoginAccount("Name", "Password");
            if (login.Length > 6) {
                test = true;
            }
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void LoginAccountFail()
        {
            bool test = false;
            var login = new Logic.AccountContainer("mock").LoginAccount("empty", "Password");
            if (login.Length == 8)
            {
                test = true;
            }
            Assert.IsFalse(test);
        }

        [TestMethod]
        public void GetMyAccount()
        {
            bool test = false;
            var myacc = new Logic.AccountContainer("mock").GetMyAccountInfo("test");
            if (myacc.account.Id.Length > 1) {
                test = true;
            }
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void AddFundsToAccount()
        {
            var funds = new Logic.AccountContainer("mock").AddfundsToAccount(1, "test");
            
            Assert.IsTrue(funds);
        }

        [TestMethod]
        public void AddFundsToAccountBelow1()
        {
            var funds = new Logic.AccountContainer("mock").AddfundsToAccount(0, "test");

            Assert.IsFalse(funds);
        }

        [TestMethod]
        public void CheckIfSignedIn()
        {
            var check = new Logic.AccountContainer("mock").CheckIfSignedIn("test");

            Assert.IsTrue(check);
        }

        [TestMethod]
        public void CheckIfSignedInWithIncorrectSessionId()
        {
            var check = new Logic.AccountContainer("mock").CheckIfSignedIn("empty");

            Assert.IsFalse(check);
        }

        [TestMethod]
        public void UploadPFP()
        {
            var check = new Logic.AccountContainer("mock").SetPFP(null, "NaN");

            Assert.IsTrue(check);
        }

        [TestMethod]
        public void UploadPFPWhenPFPNull()
        {
            var check = new Logic.AccountContainer("mock").SetPFP(null, "test");

            Assert.IsFalse(check);
        }

        
    }
}
