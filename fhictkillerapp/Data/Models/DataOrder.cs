using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class DataOrder
    {
        public string orderId { get; set; }
        public DataAccount buyer { get; set; }
        public DataPosts post { get; set; }
        public DataClientChat chat { get; set; }
        public string orderMessage { get; set; }
        public string postId { get; set; }
        public string chatId { get; set; }
        public string buyerId { get; set; }

        public string status { get; set; }

        public DataOrder() {
            buyer = new DataAccount();
            post = new DataPosts();
        }
    }
}
