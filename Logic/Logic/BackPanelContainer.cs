using Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;

namespace Logic
{
    public class BackPanelContainer
    {
        readonly Contract.IBackPanel IBackPanel;
        readonly Contract.IAccount IAccount;
        readonly Contract.IReport IReport;
        readonly Contract.IPost IPost;

        public Logic.Models.LogicBackPanel GetBackPanelInfo(string SessionId)
        {
            if (CheckIfSignedIn(SessionId)) { 
                return new Logic.Models.LogicBackPanel(IBackPanel.GetEarnings(IAccount.GetAccountId(SessionId)));
            }
            return new Logic.Models.LogicBackPanel();
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (IAccount.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }
        

        public List<Logic.Models.LogicReport> GetAdminPanelInfo(string SessionId)
        {

            if (CheckIfSignedIn(SessionId)) {
                if (IBackPanel.CheckIfAdmin(IAccount.GetAccountId(SessionId)))
                {
                    return LogicListDto.Reports(IReport.getReports(SessionId));
                }
            }
            return new List<Logic.Models.LogicReport>();
        }

        public bool BanUser(string userId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (IBackPanel.CheckIfAdmin(IAccount.GetAccountId(SessionId)))
                {
                    IBackPanel.banUser(IAccount.GetAccountId(SessionId), userId);
                    return true;
                }
            }

            return false;
        }

        public bool BanUserByPost(string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (IBackPanel.CheckIfAdmin(IAccount.GetAccountId(SessionId)))
                {
                    IBackPanel.banUser(IAccount.GetAccountId(SessionId), IPost.GetPost(postId).PostAuthor.Id);
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
                if (IBackPanel.CheckIfAdmin(IAccount.GetAccountId(SessionId)))
                {
                    post = new Logic.Models.LogicPosts(IPost.GetPost(IBackPanel.GetPostByReviewId(reportId)));
                    post.reviews.Add(new Logic.Models.LogicReview(IBackPanel.GetReportReview(reportId)));
                    return post;
                }
            }

            return new Logic.Models.LogicPosts();
        }

        public BackPanelContainer() {
            IBackPanel = Factory.Factory.GetBackpanelDAL();
            IAccount = Factory.Factory.GetAccountDAL();
            IReport = Factory.Factory.GetReportDAL();
            IPost = Factory.Factory.GetPostDAL();
        }

        public BackPanelContainer(string mode) {
            if (mode == "mock") {
                IBackPanel = Factory.MockFactory.GetBackpanelDAL();
                IAccount = Factory.MockFactory.GetAccountDAL();
                IReport = Factory.MockFactory.GetReportDAL();
                IPost = Factory.MockFactory.GetPostDAL();
            }
        }

    }
}
