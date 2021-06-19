using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Factory.MockFactory;
using Data;
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
            new Logic.BackPanelContainer("mock").Index("TestId");
        }

        [TestMethod]
        public void Admin()
        {
            new Logic.BackPanelContainer("mock").Admin("TestId");
        }

        [TestMethod]
        public void BanUser()
        {
            new Logic.BackPanelContainer("mock").BanUser("BanId", "TestID");

        }

        [TestMethod]
        public void BanUserByPost()
        {
            new Logic.BackPanelContainer("mock").BanUserByPost("PostId", "TestId");

        }

        [TestMethod]
        public void ViewReportPost()
        {
            new Logic.BackPanelContainer("mock").ViewReportPost("ReportId", "TestId");
        }
    }
}
