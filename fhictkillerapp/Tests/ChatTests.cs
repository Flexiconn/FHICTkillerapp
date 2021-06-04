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
            IChat chat = GetClassChat();
            IPost post = GetClassPost();
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            post.AddPost(null, "postName", "postDescription ", account.LoginAccount("ja", "ja"));
            post.AddOrder(account.LoginAccount("ja", "ja"), test.getPostFromSesId(account.LoginAccount("ja", "ja")));
            new Logic.Chat().SendMessage("test message", test.getChatFromSesId(account.LoginAccount("ja", "ja")), account.LoginAccount("ja", "ja"));
            test.cleanOrder(test.getOrderFromSesId(account.LoginAccount("ja", "ja")));
            test.cleanPost(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));
        }

        [TestMethod]
        public void GetMessages()
        {
            IChat chat = GetClassChat();
            IPost post = GetClassPost();
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            post.AddPost(null, "postName", "postDescription ", account.LoginAccount("ja", "ja"));
            new Logic.Chat().Index( test.getChatFromSesId(account.LoginAccount("ja", "ja")), account.LoginAccount("ja", "ja"));
            test.cleanOrder(test.getOrderFromSesId(account.LoginAccount("ja", "ja")));
            test.cleanPost(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));
        }

        [TestMethod]
        public void CreateReport()
        {
            IPost post = GetClassPost();
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            post.AddPost(null, "postName", "postDescription ", account.LoginAccount("ja", "ja"));
            new Logic.Chat().createReport(1, "test", test.getPostFromSesId(account.LoginAccount("ja", "ja")), account.LoginAccount("ja", "ja"));
            test.cleanReport(account.LoginAccount("ja", "ja"));
            test.cleanPost(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));

        }
    }
}
