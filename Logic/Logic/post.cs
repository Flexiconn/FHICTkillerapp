using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    class Post
    {
        
        public void AddPost(PostUpload postUpload, string SessionId)
        {

            postUpload.PostId = Guid.NewGuid().ToString();
            Querries.AddPost(postUpload, SessionId);
        }



        public List<Posts> Index()
        {
            return Querries.GetPosts();
        }


        public Posts ViewPost(string Id)
        {
            Posts post = new Posts();
            post = Querries.GetPost(Id);
            post.reviews = Querries.GetReview(Id);
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
                order order = new order();
                order.post.PostId = postId;
                order.buyer.Id = Querries.GetAccount(HttpContext.Session.GetString("SessionId")).Id;
                Querries.AddOrder(order);
                return true;
            }

            return false;

        }


        public bool createReview(Review review, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                review.Account = Querries.GetAccount(HttpContext.Session.GetString("SessionId"));
                Querries.createReview(HttpContext.Session.GetString("SessionId"), review);
                return true;
            }
            return false;
        }


        public bool createReport(int reportReasonform, string comment, string PostId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {

                Report report = new Report() { reportReason = reportReasonform, ReportType = (int)reportTypes.post, reportComment = comment, reportId = PostId };
                report.creatorId = Querries.GetAccount(HttpContext.Session.GetString("SessionId"));
                Querries.createReport(HttpContext.Session.GetString("SessionId"), report);
                return true;
            }
            return false;
        }

        public bool createReviewReport(string reviewId, string postIdstring, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {

                Report report = new Report() { ReportType = (int)reportTypes.review, reportId = reviewId };
                report.creatorId = Querries.GetAccount(HttpContext.Session.GetString("SessionId"));
                Querries.createReport(HttpContext.Session.GetString("SessionId"), report);
                return true;
            }
            return false;
        }
    }
}
