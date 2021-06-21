using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Contract;

namespace Contract
{
    public interface IPost
    {
        void AddPost(string id,string postName, string postDescription, string sesId);
        List<Contract.Models.ContractPosts> GetPosts();
        Contract.Models.ContractPosts GetPost(string id);
        void createReview(string id, string text, int score, string postId);
        List<Contract.Models.ContractReview> GetReview(string postId);
        Int64 PostAmount(string id);
        void AddImageToDB(string postId, string path, string Id);
        void AddFavourite(string Id, string AccountId, string PostId);
        List<Contract.Models.ContractFavourite> GetFavourites(string UserId);
        void RemoveFavourite(string Id);
    }
}
