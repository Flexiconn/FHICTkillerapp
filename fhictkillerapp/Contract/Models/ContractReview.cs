using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Models
{
    public class ContractReview
    {
        public string reviewId { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public ContractAccount Account { get; set; }
        public ContractPosts post {get;set;}
    }
}
