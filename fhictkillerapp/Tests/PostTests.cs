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
        public void AddPostLimitNotReached() {
            var test = new Logic.PostContainer("Mock").AddPost(null, "Test", "Test", "test");
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void AddPostLimitReached()
        {
            var test = new Logic.PostContainer("Mock").AddPost(null, "Test", "Test", "4");
            Assert.IsFalse(test);
        }


        [TestMethod]
        public void GetPosts()
        {
            bool test = false;
            var list = new Logic.PostContainer("Mock").GetPostsList();
            if(list.Count > 0)
            {
                test = true;
            }
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void ViewPost()
        {
            bool test = false;
            var post = new Logic.PostContainer("Mock").ViewPost("test");
            if (post.PostId == "test")
            {
                test = true;
            }
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void ViewPostWrongId()
        {
            bool test = false;
            var post = new Logic.PostContainer("Mock").ViewPost("empty");
            if (post.PostId == "test")
            {
                test = true;
            }
            Assert.IsFalse(test);
        }

        [TestMethod]
        public void CheckIfSignedIn()
        {
            var check = new Logic.PostContainer("Mock").CheckIfSignedIn("test");

            Assert.IsTrue(check);
        }

        [TestMethod]
        public void CheckIfSignedInWithIncorrectSessionId()
        {
            var check = new Logic.PostContainer("Mock").CheckIfSignedIn("empty");

            Assert.IsFalse(check);
        }



        [TestMethod]
        public void SetFavourite()
        {
            var check = new Logic.PostContainer("Mock").FavouriteToggle("test","test");

            Assert.IsTrue(check);
        }

        [TestMethod]
        public void RemoveFavourite()
        {
            var check = new Logic.PostContainer("Mock").FavouriteToggle("test", "test");

            Assert.IsTrue(check);
        }
    }
}
