using Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;

namespace Logic
{
    public class BackPanel
    {
        Contract.IBackPanel Querries = GetClassBackpanel();
        public Logic.Models.LogicBackPanel Index(string SessionId)
        {
            if (CheckIfSignedIn(SessionId)) { 
                return new Logic.Models.LogicBackPanel(Querries.GetEarnings(SessionId));
            }
            return new Logic.Models.LogicBackPanel();
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (Querries.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }
        

        public List<Logic.Models.LogicReport> Admin(string SessionId)
        {

            if (CheckIfSignedIn(SessionId)) {

                return LogicListDto.Reports(Querries.getReports(SessionId));
            }
            return new List<Logic.Models.LogicReport>();
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

        public Logic.Models.LogicPosts ViewReportPost(string reportId, string SessionId)
        {
            Logic.Models.LogicPosts post = new Logic.Models.LogicPosts();
            if (CheckIfSignedIn(SessionId))
            {
                post = new Logic.Models.LogicPosts(Querries.GetPost(Querries.GetPostByReviewId(reportId)));
                post.reviews.Add(new Logic.Models.LogicReview(Querries.GetReportReview(reportId)));
                return post;
            }

            return new Logic.Models.LogicPosts();
        }

        
    }
}
