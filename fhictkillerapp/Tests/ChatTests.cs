using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.Factory;
using Data;
using Contract;

namespace Tests
{
    [TestClass]
    public class ChatTests
    {
        [TestMethod]
        public void SendMessage()
        {
            var test = new Logic.ChatContainer("mock").SendMessage("TestMessage", "TestChatId", "test");
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void SendMessageWhenNotLoggedIn()
        {
            var test = new Logic.ChatContainer("mock").SendMessage("TestMessage", "TestChatId", "empty");
            Assert.IsFalse(test);
        }

        [TestMethod]
        public void GetMessages()
        {
            var test = new Logic.ChatContainer("mock").GetChat("Test", "Test");
            Assert.IsNotNull(test);
        }



        [TestMethod]
        public void CheckIfSignedIn()
        {
            new Logic.ChatContainer("mock").CheckIfSignedIn("test");
        }
    }
}
