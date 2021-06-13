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
            new Logic.PostContainer("mock").Index();

        }

        [TestMethod]
        public void GetPost()
        {
            new Logic.PostContainer("mock").ViewPost("TestPostId");
        }



        [TestMethod]
        public void OrderPost() {
            new Logic.PostContainer("mock").OrderPost("TestMessage", "TestPostId", "TestId");
        }



        [TestMethod]
        public void CreateReview()
        {
            new Logic.PostContainer("mock").createReview("TestText", 5, "TestPostId", "TestId");
        }

        [TestMethod]
        public void CreateReport()
        {
            new Logic.PostContainer("mock").createReport(1, "TestComment", "TestPostId", "TestId");
        }

        [TestMethod]
        public void Addpost()
        {
            new Logic.PostContainer("mock").AddPost(null,"TestPostName", "TestDescription", "TestId");
        }
    }
}
