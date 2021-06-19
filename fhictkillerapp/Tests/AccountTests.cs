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

        
    }
}
