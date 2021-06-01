using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class Review
    {
        public string reviewId { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public Account Account { get; set; }
        public string postId {get;set;}

        public Review() {
        
        
        }

        public Review(Logic.Models.LogicReview dto)
        {
            this.Account = new Account() { Balance = dto.Account.Balance, Id = dto.Account.Balance, Name = dto.Account.Name, Password = dto.Account.Password, SessionId = dto.Account.SessionId };
            this.postId = dto.postId;
            this.reviewId = dto.reviewId;
            this.score = dto.score;
            this.text = dto.text;
        }
    }
}
