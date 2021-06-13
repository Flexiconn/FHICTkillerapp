using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Contract;

namespace Contract
{
    public interface IPost
    {
        string GetAccountId(string SessionId);
        Contract.Models.ContractAccount GetAccount(string id);
        void AddPost(IFormFile myImage, string postName, string postDescription, string sesId);
        List<Contract.Models.ContractPosts> GetPosts();
        string GetAccountName(string id);
        Contract.Models.ContractPosts GetPost(string id);
        void AddOrder(string id, string postId);
        bool CheckIfSignedIn(string Id);
        Contract.Models.ContractAccount GetProfileInfo(string Id);
        void createReview(string id, string text, int score, string postId);
        void createReport(string id, reportTypes reportType, reportReasons reportReason, string comment, string reportedId);
        List<Contract.Models.ContractReview> GetReview(string postId);
        bool PostLimitReached(string id);

        
    }
}
