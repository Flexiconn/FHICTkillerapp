using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IBackPanel
    {
        Posts GetPost(string id);
        bool CheckIfSignedIn(string Id);
        BackPanel GetEarnings(string id);
        List<Report> getReports(string id);
        void banUser(string adminId, string userId);
        string GetPostByReviewId(string Id);
        Review GetReportReview(string reportId);
    }
}
