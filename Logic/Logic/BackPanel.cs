using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class BackPanel
    {
        public string Index(string SessionId)
        {
            if (CheckIfSignedIn(SessionId)) { 
            return connection.GetEarnings(SessionId);
            }
            return null;
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (Querries.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }


        public List<Report> Admin(string SessionId)
        {
            if (CheckIfSignedIn(SessionId)) {
                return connection.getReports(SessionId);
            }
            return null;
        }

        public bool BanUser(string userId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                connection.banUser(SessionId, userId);
                return true;
            }

            return false;
        }

        public bool BanUserByPost(string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                connection.banUser(SessionId, connection.GetPost(postId).PostAuthor);
                return true;
            }
            
            return false;
        }

        public Posts ViewReportPost(string reportId, string SessionId)
        {
            Posts post = new Posts();
            if (CheckIfSignedIn(SessionId))
            {
                post = connection.GetPost(connection.GetPostByReviewId(reportId));
                post.reviews.Add(connection.GetReportReview(reportId));
                return post;
            }

            return null;
        }
    }
}
