using Common;
using Common.Models;
using Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;

namespace Logic
{
    public class Post
    {
        Contract.IPost Querries = GetClassPost();
        public bool AddPost(IFormFile myImage, string postName, string postDescription, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.AddPost(myImage, postName, postDescription, SessionId);
                return true;
            }
            else {
                return false;
            }

        }



        public List<Logic.Models.LogicPosts> Index()
        {
            return LogicListDto.Posts(Querries.GetPosts());
        }


        public Logic.Models.LogicPosts ViewPost(string Id)
        {
            Logic.Models.LogicPosts post = new Logic.Models.LogicPosts(Querries.GetPost(Id));
            post.reviews = LogicListDto.Reviews(Querries.GetReview(Id));
            return post;
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (Querries.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public bool OrderPost(string orderMessage, string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Data.Models.order order = new Data.Models.order();
                order.post.PostId = postId;
                order.buyer.Id = Querries.GetAccount(SessionId).Id;
                Querries.AddOrder(SessionId, postId);

                return true;
            }

            return false;

        }


        public bool createReview(string text, int score, string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.createReview(Querries.GetAccount(SessionId).Id, text,  score,  postId);
                return true;
            }
            return false;
        }


        public bool createReport(int reportReasonform, string comment, string PostId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.createReport(SessionId, Contract.reportTypes.post, Contract.reportReasons.scam, comment, PostId);
                return true;
            }
            return false;
        }

        public bool createReviewReport(string reviewId, string postIdstring, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {

                Querries.createReport(SessionId, Contract.reportTypes.review, Contract.reportReasons.scam, "test" , reviewId);
                return true;
            }
            return false;
        }
    }
}
