using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.Factory;
using Data;
using Data.Interfaces;
using Common;
using Common.Models;

namespace Tests
{
    [TestClass]
    public class ChatTests
    {
        [TestMethod]
        public void SendMessage()
        {
            IChat account = GetClassChat();
            account.SendMessage(new ClientChat() {AccountName="Test",  })
        }

        
    }
}
