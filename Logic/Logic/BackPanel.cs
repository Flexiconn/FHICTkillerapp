using Common;
using Common.Models;
using Data;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;

namespace Logic
{
    public class BackPanel
    {
        IBackPanel Querries = GetClassBackpanel();
        public Common.Models.BackPanel Index(string SessionId)
        {
            if (CheckIfSignedIn(SessionId)) { 
                return Querries.GetEarnings(SessionId);
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
                return Querries.getReports(SessionId);
            }
            return null;
        }

        public bool BanUser(string userId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.banUser(SessionId, userId);
                return true;
            }

            return false;
        }

        public bool BanUserByPost(string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                Querries.banUser(SessionId, Querries.GetPost(postId).PostAuthor);
                return true;
            }
            
            return false;
        }

        public Posts ViewReportPost(string reportId, string SessionId)
        {
            Posts post = new Posts();
            if (CheckIfSignedIn(SessionId))
            {
                post = Querries.GetPost(Querries.GetPostByReviewId(reportId));
                post.reviews.Add(Querries.GetReportReview(reportId));
                return post;
            }

            return null;
        }
    }
}
