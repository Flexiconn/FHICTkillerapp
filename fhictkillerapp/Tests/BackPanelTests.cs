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
            new Logic.BackPanelLogic("mock").Index("TestId");
        }

        [TestMethod]
        public void Admin()
        {
            new Logic.BackPanelLogic("mock").Admin("TestId");
        }

        [TestMethod]
        public void BanUser()
        {
            new Logic.BackPanelLogic("mock").BanUser("BanId", "TestID");

        }

        [TestMethod]
        public void BanUserByPost()
        {
            new Logic.BackPanelLogic("mock").BanUserByPost("PostId", "TestId");

        }

        [TestMethod]
        public void ViewReportPost()
        {
            new Logic.BackPanelLogic("mock").ViewReportPost("ReportId", "TestId");
        }
    }
}
