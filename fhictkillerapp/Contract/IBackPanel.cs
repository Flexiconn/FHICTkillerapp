using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IBackPanel
    {
        string GetAccountId(string SessionId);
        bool CheckIfAdmin(string id);
        Contract.Models.ContractPosts GetPost(string id);
        bool CheckIfSignedIn(string Id);
        Contract.Models.ContractBackPanel GetEarnings(string id);
        List<Contract.Models.ContractReport> getReports(string id);
        void banUser(string adminId, string userId);
        string GetPostByReviewId(string Id);
        Contract.Models.ContractReview GetReportReview(string reportId);

    }
}
