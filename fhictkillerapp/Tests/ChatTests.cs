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
            new Logic.ChatContainer("mock").SendMessage("TestMessage", "TestChatId", "TestId");
        }

        [TestMethod]
        public void GetMessages()
        {
        //    new Logic.ChatContainer("mock").Index("TestChatId", "TestId");
        }

        [TestMethod]
        public void CreateReport()
        {
            new Logic.ChatContainer("mock").createReport(1, "TestComment", "TestChatId", "TestId");
        }
    }
}
