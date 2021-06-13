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
        void AddPost(string id,string postName, string postDescription, string sesId);
        List<Contract.Models.ContractPosts> GetPosts();
        string GetAccountName(string id);
        Contract.Models.ContractPosts GetPost(string id);
        void AddOrder(string orderId, string id, string postId, string chatId);
        bool CheckIfSignedIn(string Id);
        Contract.Models.ContractAccount GetProfileInfo(string Id);
        void createReview(string id, string text, int score, string postId);
        void createReport(string id, reportTypes reportType, reportReasons reportReason, string comment, string reportedId);
        List<Contract.Models.ContractReview> GetReview(string postId);
        Int64 PostAmount(string id);
        void AddImageToDB(string postId, string path, string Id);



    }
}
