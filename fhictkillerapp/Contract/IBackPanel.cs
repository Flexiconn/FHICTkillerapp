using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IBackPanel
    {
        bool CheckIfAdmin(string id);
        Contract.Models.ContractBackPanel GetEarnings(string id);
        void banUser(string adminId, string userId);
        string GetPostByReviewId(string Id);
        Contract.Models.ContractReview GetReportReview(string reportId);

    }
}
