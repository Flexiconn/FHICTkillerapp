using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.MockFactory;
using Data;
using Common;
using Contract;
using System;

namespace Tests
{
    [TestClass]
    public class BackPanelTests
    {
        [TestMethod]
        public void Index()
        {
            new Logic.BackPanel("mock").Index("TestId");
        }

        [TestMethod]
        public void Admin()
        {
            new Logic.BackPanel("mock").Admin("TestId");
        }

        [TestMethod]
        public void BanUser()
        {
            new Logic.BackPanel("mock").BanUser("BanId", "TestID");

        }

        [TestMethod]
        public void BanUserByPost()
        {
            new Logic.BackPanel("mock").BanUserByPost("PostId", "TestId");

        }

        [TestMethod]
        public void ViewReportPost()
        {
            new Logic.BackPanel("mock").ViewReportPost("ReportId", "TestId");
        }
    }
}
