using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class ViewReview
    {
        public string reviewId { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public ViewAccount Account { get; set; }
        public ViewPosts Post {get;set;}
        public string postId { get; set; }
        public ViewReview() {
        
        
        }

        public ViewReview(Logic.Models.LogicReview dto)
        {
            this.Account = new ViewAccount() { Balance = dto.Account.Balance, Id = dto.Account.Balance, Name = dto.Account.Name, Password = dto.Account.Password, SessionId = dto.Account.SessionId };
            this.reviewId = dto.reviewId;
            this.score = dto.score;
            this.text = dto.text;
        }
    }
}
