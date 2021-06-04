using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.Factory;
using Data;
using Contract;
using System;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void GetPosts()
        {
            IPost post = GetClassPost();
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            post.AddPost(null, "postName", "postDescription ", account.LoginAccount("ja", "ja"));
            new Logic.Post().Index();
            test.cleanPost(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));

        }

        [TestMethod]
        public void GetPost()
        {
            IPost post = GetClassPost();
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            post.AddPost(null, "postName", "postDescription ", account.LoginAccount("ja", "ja"));
            new Logic.Post().ViewPost(test.getPostFromSesId(account.LoginAccount("ja", "ja")));
            test.cleanPost(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));

        }

        [TestMethod]
        public void GetReviews()
        {
            IPost post = GetClassPost();
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            post.AddPost(null, "postName", "postDescription ", account.LoginAccount("ja", "ja"));
            post.createReview(account.LoginAccount("ja", "ja"), "test text", 5, test.getPostFromSesId(account.LoginAccount("ja", "ja")));
            post.GetReview(account.LoginAccount("ja", "ja"));
            test.cleanReview(account.LoginAccount("ja", "ja"));
            test.cleanPost(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));


        }



        [TestMethod]
        public void OrderPost() {
            IPost post = GetClassPost();
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            post.AddPost(null, "postName", "postDescription ", account.LoginAccount("ja", "ja"));
            new Logic.Post().OrderPost("Order", test.getOrderFromSesId(account.LoginAccount("ja", "ja")), account.LoginAccount("ja", "ja"));
            test.cleanOrder(test.getOrderFromSesId(account.LoginAccount("ja", "ja")));
            test.cleanPost(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));
        }



        [TestMethod]
        public void CreateReview()
        {
            IPost post = GetClassPost();
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            post.AddPost(null, "postName", "postDescription ", account.LoginAccount("ja", "ja"));
            post.AddOrder(account.LoginAccount("ja", "ja"), test.getPostFromSesId(account.LoginAccount("ja", "ja")));
            new Logic.Post().createReview("test",5, test.getOrderFromSesId(account.LoginAccount("ja", "ja")), account.LoginAccount("ja", "ja"));
            test.cleanReview(account.LoginAccount("ja", "ja"));
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
            new Logic.Post().createReport(1, "test", test.getPostFromSesId(account.LoginAccount("ja", "ja")), account.LoginAccount("ja", "ja"));
            test.cleanReport(account.LoginAccount("ja", "ja"));
            test.cleanPost(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));

        }

        [TestMethod]
        public void Addpost()
        {
            IPost post = GetClassPost();
            IAccount account = GetClassAccount();
            Data.TestQuerries test = new TestQuerries();
            account.CreateAccount("ja", "ja");
            post.AddPost(null, "postName", "postDescription ", account.LoginAccount("ja", "ja"));
            test.cleanPost(account.LoginAccount("ja", "ja"));
            test.cleanAccount(account.LoginAccount("ja", "ja"));
        }
    }
}
