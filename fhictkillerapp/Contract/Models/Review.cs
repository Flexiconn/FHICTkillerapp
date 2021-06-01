using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Models
{
    public class Review
    {
        public string reviewId { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public Account Account { get; set; }
        public string postId {get;set;}
    }
}
