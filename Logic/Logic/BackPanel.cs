using Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;

namespace Logic
{
    public class BackPanel
    {
        Contract.IBackPanel Querries;
        public Logic.Models.LogicBackPanel Index(string SessionId)
        {
            if (CheckIfSignedIn(SessionId)) { 
                return new Logic.Models.LogicBackPanel(Querries.GetEarnings(Querries.GetAccountId(SessionId)));
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
                if (Querries.CheckIfAdmin(Querries.GetAccountId(SessionId)))
                {
                    return LogicListDto.Reports(Querries.getReports(SessionId));
                }
            }
            return new List<Logic.Models.LogicReport>();
        }

        public bool BanUser(string userId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (Querries.CheckIfAdmin(Querries.GetAccountId(SessionId)))
                {
                    Querries.banUser(Querries.GetAccountId(SessionId), userId);
                    return true;
                }
            }

            return false;
        }

        public bool BanUserByPost(string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (Querries.CheckIfAdmin(Querries.GetAccountId(SessionId)))
                {
                    Querries.banUser(Querries.GetAccountId(SessionId), Querries.GetPost(postId).PostAuthor);
                    return true;
                }
            }
            
            return false;
        }

        public Logic.Models.LogicPosts ViewReportPost(string reportId, string SessionId)
        {
            Logic.Models.LogicPosts post = new Logic.Models.LogicPosts();
            if (CheckIfSignedIn(SessionId))
            {
                if (Querries.CheckIfAdmin(Querries.GetAccountId(SessionId)))
                {
                    post = new Logic.Models.LogicPosts(Querries.GetPost(Querries.GetPostByReviewId(reportId)));
                    post.reviews.Add(new Logic.Models.LogicReview(Querries.GetReportReview(reportId)));
                    return post;
                }
            }

            return new Logic.Models.LogicPosts();
        }

        public BackPanel() {
            Querries = GetClassBackpanel();
        }
        public BackPanel(string mode) {
            if (mode == "mock") {
                Querries = Factory.MockFactory.GetClassBackpanel();
            }
        }

    }
}
