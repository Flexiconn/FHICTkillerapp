using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class order
    {
        public Guid orderId { get; set; }
        public Account buyer { get; set; }
        public Posts post { get; set; }
        public ClientChat chat { get; set; }
        public string orderMessage { get; set; }
        public string postId { get; set; }
        public string chatId { get; set; }
        public string buyerId { get; set; }

        public string status { get; set; }

        public order() {
            buyer = new Account();
            post = new Posts();
        }
    }
}
