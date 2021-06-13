using Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;

namespace Logic
{
    public class BackPanelContainer
    {
        Contract.IBackPanel IBackPanel;
        public Logic.Models.LogicBackPanel GetBackPanelInfo(string SessionId)
        {
            if (CheckIfSignedIn(SessionId)) { 
                return new Logic.Models.LogicBackPanel(IBackPanel.GetEarnings(IBackPanel.GetAccountId(SessionId)));
            }
            return new Logic.Models.LogicBackPanel();
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (IBackPanel.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }
        

        public List<Logic.Models.LogicReport> GetAdminPanelInfo(string SessionId)
        {

            if (CheckIfSignedIn(SessionId)) {
                if (IBackPanel.CheckIfAdmin(IBackPanel.GetAccountId(SessionId)))
                {
                    return LogicListDto.Reports(IBackPanel.getReports(SessionId));
                }
            }
            return new List<Logic.Models.LogicReport>();
        }

        public bool BanUser(string userId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (IBackPanel.CheckIfAdmin(IBackPanel.GetAccountId(SessionId)))
                {
                    IBackPanel.banUser(IBackPanel.GetAccountId(SessionId), userId);
                    return true;
                }
            }

            return false;
        }

        public bool BanUserByPost(string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (IBackPanel.CheckIfAdmin(IBackPanel.GetAccountId(SessionId)))
                {
                    IBackPanel.banUser(IBackPanel.GetAccountId(SessionId), IBackPanel.GetPost(postId).PostAuthor);
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
                if (IBackPanel.CheckIfAdmin(IBackPanel.GetAccountId(SessionId)))
                {
                    post = new Logic.Models.LogicPosts(IBackPanel.GetPost(IBackPanel.GetPostByReviewId(reportId)));
                    post.reviews.Add(new Logic.Models.LogicReview(IBackPanel.GetReportReview(reportId)));
                    return post;
                }
            }

            return new Logic.Models.LogicPosts();
        }

        public BackPanelContainer() {
            IBackPanel = Factory.Factory.GetBackpanelDAL();
        }
        public BackPanelContainer(string mode) {
            if (mode == "mock") {
                IBackPanel = Factory.MockFactory.GetClassBackpanel();
            }
        }

    }
}
