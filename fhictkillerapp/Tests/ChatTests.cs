using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.Factory;
using Data;
using Common;
using Contract;
using Common.Models;

namespace Tests
{
    [TestClass]
    public class ChatTests
    {
        [TestMethod]
        public void SendMessage()
        {
            new Logic.ChatLogic("mock").SendMessage("TestMessage", "TestChatId", "TestId");
        }

        [TestMethod]
        public void GetMessages()
        {
            new Logic.ChatLogic("mock").Index("TestChatId", "TestId");
        }

        [TestMethod]
        public void CreateReport()
        {
            new Logic.ChatLogic("mock").createReport(1, "TestComment", "TestChatId", "TestId");
        }
    }
}
