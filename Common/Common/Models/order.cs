using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
    public class order
    {
        [Key]
        public Guid orderId { get; set; }
        public Account buyer { get; set; }
        public Posts post { get; set; }
        public Chat chat { get; set; }
        public string orderMessage { get; set; }
        public string postId { get; set; }
        public string chatId { get; set; }
        public string status { get; set; }

        public order() {
            buyer = new Account();
            post = new Posts();
        }
    }
}
