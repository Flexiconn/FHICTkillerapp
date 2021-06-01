using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Contract;

namespace Contract
{
    public interface IPost
    {
        void start();
        Contract.Models.Account GetAccount(string id);
        void AddPost(IFormFile myImage, string postName, string postDescription, string sesId);
        List<Contract.Models.Posts> GetPosts();
        string GetAccountName(string id);
        Contract.Models.Posts GetPost(string id);
        void AddOrder(string id, string postId);
        bool CheckIfSignedIn(string Id);
        Contract.Models.Account GetProfileInfo(string Id);
        void createReview(string id, string text, int score, string postId);
        void createReport(string id, reportTypes reportType, reportReasons reportReason, string comment, string reportedId);
        List<Contract.Models.Review> GetReview(string postId);
    }
}
