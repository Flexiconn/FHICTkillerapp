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
            new Logic.Post("mock").Index();

        }

        [TestMethod]
        public void GetPost()
        {
            new Logic.Post("mock").ViewPost("TestPostId");
        }



        [TestMethod]
        public void OrderPost() {
            new Logic.Post("mock").OrderPost("TestMessage", "TestPostId", "TestId");
        }



        [TestMethod]
        public void CreateReview()
        {
            new Logic.Post("mock").createReview("TestText", 5, "TestPostId", "TestId");
        }

        [TestMethod]
        public void CreateReport()
        {
            new Logic.Post("mock").createReport(1, "TestComment", "TestPostId", "TestId");
        }

        [TestMethod]
        public void Addpost()
        {
            new Logic.Post("mock").AddPost(null,"TestPostName", "TestDescription", "TestId");
        }
    }
}
