using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IPost
    {
        void start();
        Account GetAccount(string id);
        void AddPost(PostUpload insertPost, string sesId);
        List<Posts> GetPosts();
        string GetAccountName(string id);
        Posts GetPost(string id);
        void AddOrder(order order);
        bool CheckIfSignedIn(string Id);
        Account GetProfileInfo(string Id);
        void createReview(string id, Review review);
        void createReport(string id, Report report);
        List<Review> GetReview(string postId);
    }
}
