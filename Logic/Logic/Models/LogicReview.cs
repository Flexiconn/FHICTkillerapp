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

        public LogicReview() {
        
        
        }

        public LogicReview(Contract.Models.ContractReview dto)
        {
            if (dto.Account != null)
            {
                this.Account = new LogicAccount(dto.Account.Balance, dto.Account.Balance, dto.Account.SessionId, dto.Account.Name, dto.Account.Password);
            }
            this.reviewId = dto.reviewId;
            this.score = dto.score;
            this.text = dto.text;
        }

        public LogicAccount GetAccount() {
            return Account;
        }



        public int GetScore()
        {
            return score;
        }

        public string GetId()
        {
            return reviewId;
        }

        public string GetText()
        {
            return text;
        }
    }
}
