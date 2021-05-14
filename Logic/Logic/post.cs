using Common;
using Common.Models;
using Data;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Post
    {
        IPost Querries = new Connection();
        public bool AddPost(PostUpload postUpload, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                postUpload.PostId = Guid.NewGuid().ToString();
                Querries.AddPost(postUpload, SessionId);
                return true;
            }
            else {
                return false;
            }

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
                order.buyer.Id = Querries.GetAccount(SessionId).Id;
                Querries.AddOrder(order);
                return true;
            }

            return false;

        }


        public bool createReview(Common.Models.Review review, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                review.Account = Querries.GetAccount(SessionId);
                Querries.createReview(SessionId, review);
                return true;
            }
            return false;
        }


        public bool createReport(int reportReasonform, string comment, string PostId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {

                Report report = new Report() { reportReason = reportReasonform, ReportType = (int)reportTypes.post, reportComment = comment, reportId = PostId };
                report.creatorId = Querries.GetAccount(SessionId);
                Querries.createReport(SessionId, report);
                return true;
            }
            return false;
        }

        public bool createReviewReport(string reviewId, string postIdstring, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {

                Report report = new Report() { ReportType = (int)reportTypes.review, reportId = reviewId };
                report.creatorId = Querries.GetAccount(SessionId);
                Querries.createReport(SessionId, report);
                return true;
            }
            return false;
        }
    }
}
