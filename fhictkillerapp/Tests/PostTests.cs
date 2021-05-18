using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.Factory;
using Data;
using Data.Interfaces;
using Common;
using Common.Models;
using System;

namespace Tests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void GetPosts()
        {
            IPost post = GetClassPost();
            post.GetPosts();
        }

        [TestMethod]
        public void GetPost()
        {
            IPost post = GetClassPost();
            post.GetPost("test");
        }

        [TestMethod]
        public void GetReviews()
        {
            IPost post = GetClassPost();
            post.GetReview("test");
        }

        [TestMethod]
        public void GetProfileInfo()
        {
            IPost post = GetClassPost();
            post.GetProfileInfo("test");
        }

        [TestMethod]
        public void GetAccountName()
        {
            IPost post = GetClassPost();
            post.GetAccountName("test");
        }

        [TestMethod]
        public void GetAccount()
        {
            IPost post = GetClassPost();
            post.GetAccount("test");
        }

        [TestMethod]
        public void CreateReview()
        {
            IPost post = GetClassPost();
            post.createReview("test",new Review() { postId= Guid.NewGuid().ToString(), Account = post.GetAccount("test"), score = 5, text="test text" });
        }

        [TestMethod]
        public void CreateReport()
        {
            IPost post = GetClassPost();
            post.createReport("test", new Report() { id = Guid.NewGuid().ToString(), reportReason = (int)reportReasons.misLeading, ReportType = (int)reportTypes.post, status = "test", creatorId = post.GetAccount("test"), reportId = "test", reportComment = "test text" });
        }

        [TestMethod]
        public void Addpost()
        {
            IPost post = GetClassPost();
            post.AddPost(new PostUpload() {MyImage = null, PostName="Test", PostDescription="Test description", PostId = Guid.NewGuid().ToString() }, post.GetAccount("test").SessionId);
        }
    }
}
