using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class LogicReview
    {
        public string reviewId { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public LogicAccount Account { get; set; }
        public string postId {get;set;}

        public LogicReview() {
        
        
        }

        public LogicReview(Contract.Models.Review dto)
        {
            this.Account = new LogicAccount() { Balance = dto.Account.Balance, Id = dto.Account.Balance, Name = dto.Account.Name, Password = dto.Account.Password, SessionId = dto.Account.SessionId };
            this.postId = dto.postId;
            this.reviewId = dto.reviewId;
            this.score = dto.score;
            this.text = dto.text;
        }
    }
}
