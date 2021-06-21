using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class ReportTests
    {
        [TestMethod]
        public void CreateChatReport()
        {
            var test = new Logic.ReportContainer("mock").createChatReport(1, "TestComment", "TestChatId", "test");
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void CreatePostReport()
        {
            var test = new Logic.ReportContainer("mock").createPostReport(1, "TestComment", "TestPostId", "test");
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void CreateReviewReport()
        {
            var test = new Logic.ReportContainer("mock").createReviewReport( "TestComment", "TestChatId", "test");
            Assert.IsTrue(test);
        }
    }
}
