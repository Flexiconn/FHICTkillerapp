using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IBackPanel
    {
        string GetAccountId(string SessionId);
        bool CheckIfAdmin(string id);
        Contract.Models.Posts GetPost(string id);
        bool CheckIfSignedIn(string Id);
        Contract.Models.BackPanel GetEarnings(string id);
        List<Contract.Models.Report> getReports(string id);
        void banUser(string adminId, string userId);
        string GetPostByReviewId(string Id);
        Contract.Models.Review GetReportReview(string reportId);

    }
}
