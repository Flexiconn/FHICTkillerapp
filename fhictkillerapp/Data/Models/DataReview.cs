using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class DataReview
    {
        public string reviewId { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public DataAccount Account { get; set; }
        public string postId {get;set;}
    }
}
