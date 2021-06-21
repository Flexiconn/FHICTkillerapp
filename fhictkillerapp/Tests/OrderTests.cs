using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void CancelOrderWhenNotAccepted()
        {
            var check = new Logic.OrderContainer("mock").cancelOrder("owner", "buyer");

            Assert.IsFalse(check);
        }

        [TestMethod]
        public void CancelOrderWhenAccepted()
        {
            var check = new Logic.OrderContainer("mock").cancelOrder("owner", "buyer");

            Assert.IsFalse(check);
        }

        [TestMethod]
        public void CancelOrderWhenAcceptedAssSeller()
        {
            var check = new Logic.OrderContainer("mock").cancelOrder("owner", "owner");

            Assert.IsFalse(check);
        }

        [TestMethod]
        public void OrderPost()
        {
            var check = new Logic.OrderContainer("Mock").OrderPost("empty", "test", "test");

            Assert.IsTrue(check);
        }

        [TestMethod]
        public void OrderPostNonExistantPost()
        {
            var check = new Logic.OrderContainer("Mock").OrderPost("empty", "wrong", "test");

            Assert.IsFalse(check);
        }
    }
}
