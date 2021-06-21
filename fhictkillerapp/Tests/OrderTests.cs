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
            var check = new Logic.OrderContainer("mock").OrderPost("empty", "test", "test");

            Assert.IsTrue(check);
        }

        [TestMethod]
        public void OrderPostNonExistantPost()
        {
            var check = new Logic.OrderContainer("mock").OrderPost("empty", "empty", "test");

            Assert.IsFalse(check);
        }

        [TestMethod]
        public void AcceptOrder()
        {
            var check = new Logic.OrderContainer("mock").AcceptOrder("test","test");

            Assert.IsFalse(check);
        }

        [TestMethod]
        public void GetOrders()
        {
            var test = false;
            var orders = new Logic.OrderContainer("mock").GetOrders("test");
            if (orders.ordersIncoming.Count > 0) {
                test = true;
            }
            Assert.IsTrue(test);
        }
    }
}
