using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockData
{
    public class MockBackPanelDbConnection : Contract.IBackPanel
    {
        public Contract.Models.ContractBackPanel GetEarnings(string id)
        {
            return new Contract.Models.ContractBackPanel() { earnings = 50, orders = new List<Contract.Models.Contractorder>() { new Contract.Models.Contractorder() { } } }
        }

        public bool CheckIfAdmin(string id) {
            bool admin = false;
            if (id == "admin") {
                admin = true;
            }
            return admin;
        }
        
        

        public void banUser(string adminId, string userId)
        {

        }

        

        public string GetPostByReviewId(string Id)
        {
            return "test";
            
        }

        public Contract.Models.ContractReview GetReportReview(string reportId)
        {
            return new Contract.Models.ContractReview(){Account = new Contract.Models.ContractAccount(), post = new Contract.Models.ContractPosts(), reviewId = "TestId", score = 5 , text = "reviewText" };
        }
    }
}
