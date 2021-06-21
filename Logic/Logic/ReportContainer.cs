using System;
using System.Collections.Generic;
using System.Text;
using Data;
using static Factory.Factory;
using Microsoft.AspNetCore.Http;
using Contract;
using Logic.Models;

namespace Logic
{
    public class ReportContainer 
    {
        readonly Contract.IReport IReport;
        readonly Contract.IAccount IAccount;
        

        public bool CheckIfSignedIn(string SessionId)
        {
            if (IAccount.CheckIfSignedIn(SessionId))
            {
                return true;
            }
            return false;
        }

        public bool createPostReport(int reportReasonform, string comment, string PostId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IReport.createReport(Guid.NewGuid().ToString(), IAccount.GetAccountId(SessionId), Contract.reportTypes.post, Contract.reportReasons.scam, comment, PostId);
                return true;
            }
            return false;
        }

        public bool createReviewReport(string reviewId, string postIdstring, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {

                IReport.createReport(Guid.NewGuid().ToString(), IAccount.GetAccountId(SessionId), Contract.reportTypes.review, Contract.reportReasons.scam, "test", reviewId);
                return true;
            }
            return false;
        }

        public bool createChatReport(int reportReasonform, string comment, string chatId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IReport.createReport(Guid.NewGuid().ToString(), IAccount.GetAccountId(SessionId), Contract.reportTypes.post, Contract.reportReasons.scam, comment, chatId);

                return true;
            }
            return false;
        }

        public ReportContainer() {
            IReport = Factory.Factory.GetReportDAL();
            IAccount = Factory.Factory.GetAccountDAL();
        }

        public ReportContainer(string mode) {
            if (mode == "mock") {
                IReport = Factory.MockFactory.GetReportDAL();
                IAccount = Factory.MockFactory.GetAccountDAL();
            }
        }
    }
}
