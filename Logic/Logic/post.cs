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
        Contract.IPost Querries;
        public bool AddPost(IFormFile myImage, string postName, string postDescription, string SessionId)
        {
            if (CheckIfSignedIn(Querries.GetAccountId(SessionId)))
            {
                if (Querries.PostLimitReached(SessionId))
                {
                    Querries.AddPost(myImage, postName, postDescription, Querries.GetAccountId(SessionId));
                }
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
                order.buyer.Id = Querries.GetAccount(Querries.GetAccountId(SessionId)).Id;
                Querries.AddOrder(Querries.GetAccountId(SessionId), postId);

                return true;
            }

            return false;

        }


        public bool createReview(string text, int score, string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.createReview(Querries.GetAccountId(SessionId), text,  score,  postId);
                return true;
            }
            return false;
        }


        public bool createReport(int reportReasonform, string comment, string PostId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.createReport(Querries.GetAccountId(SessionId), Contract.reportTypes.post, Contract.reportReasons.scam, comment, PostId);
                return true;
            }
            return false;
        }

        public bool createReviewReport(string reviewId, string postIdstring, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {

                Querries.createReport(Querries.GetAccountId(SessionId), Contract.reportTypes.review, Contract.reportReasons.scam, "test" , reviewId);
                return true;
            }
            return false;
        }

        public Post() {
            Querries = GetClassPost();
        }
        public Post(string mode) {
            if (mode == "mock") {
                Querries = Factory.MockFactory.GetClassPost();
            }
        }

    }
}
