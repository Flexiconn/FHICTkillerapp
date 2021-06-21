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
            new Logic.ReportContainer("mock").createChatReport(1, "TestComment", "TestChatId", "TestId");
        }
    }
}
