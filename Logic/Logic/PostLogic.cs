using Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;

namespace Logic
{
    public class PostLogic
    {
        Contract.IPost IPost;
        public bool AddPost(IFormFile myImage, string postName, string postDescription, string SessionId)
        {
            if (CheckIfSignedIn(IPost.GetAccountId(SessionId)))
            {
                if (IPost.PostLimitReached(SessionId))
                {
                    IPost.AddPost(myImage, postName, postDescription, IPost.GetAccountId(SessionId));
                }
                return true;
            }
            else {
                return false;
            }

        }



        public List<Logic.Models.LogicPosts> GetPostsList()
        {
            return LogicListDto.Posts(IPost.GetPosts());
        }


        public Logic.Models.LogicPosts ViewPost(string Id)
        {
            Logic.Models.LogicPosts post = new Logic.Models.LogicPosts(IPost.GetPost(Id));
            post.reviews = LogicListDto.Reviews(IPost.GetReview(Id));
            return post;
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (IPost.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public bool OrderPost(string orderMessage, string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Data.Models.DataOrder order = new Data.Models.DataOrder();
                order.post.PostId = postId;
                order.buyer.Id = IPost.GetAccount(IPost.GetAccountId(SessionId)).Id;
                IPost.AddOrder(IPost.GetAccountId(SessionId), postId);

                return true;
            }

            return false;

        }


        public bool createReview(string text, int score, string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IPost.createReview(IPost.GetAccountId(SessionId), text,  score,  postId);
                return true;
            }
            return false;
        }


        public bool createReport(int reportReasonform, string comment, string PostId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IPost.createReport(IPost.GetAccountId(SessionId), Contract.reportTypes.post, Contract.reportReasons.scam, comment, PostId);
                return true;
            }
            return false;
        }

        public bool createReviewReport(string reviewId, string postIdstring, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {

                IPost.createReport(IPost.GetAccountId(SessionId), Contract.reportTypes.review, Contract.reportReasons.scam, "test" , reviewId);
                return true;
            }
            return false;
        }

        public PostLogic() {
            IPost = Factory.Factory.GetPostDAL();
        }
        public PostLogic(string mode) {
            if (mode == "mock") {
                IPost = Factory.MockFactory.GetClassPost();
            }
        }

    }
}
