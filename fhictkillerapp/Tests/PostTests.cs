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
            new Logic.PostLogic("mock").Index();

        }

        [TestMethod]
        public void GetPost()
        {
            new Logic.PostLogic("mock").ViewPost("TestPostId");
        }



        [TestMethod]
        public void OrderPost() {
            new Logic.PostLogic("mock").OrderPost("TestMessage", "TestPostId", "TestId");
        }



        [TestMethod]
        public void CreateReview()
        {
            new Logic.PostLogic("mock").createReview("TestText", 5, "TestPostId", "TestId");
        }

        [TestMethod]
        public void CreateReport()
        {
            new Logic.PostLogic("mock").createReport(1, "TestComment", "TestPostId", "TestId");
        }

        [TestMethod]
        public void Addpost()
        {
            new Logic.PostLogic("mock").AddPost(null,"TestPostName", "TestDescription", "TestId");
        }
    }
}
