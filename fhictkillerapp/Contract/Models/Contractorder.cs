using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contract.Models
{
    public class Contractorder
    {
        public string orderId { get; set; }
        public ContractAccount buyer { get; set; }
        public ContractPosts post { get; set; }
        public ContractClientChat chat { get; set; }
        public string orderMessage { get; set; }
        public string postId { get; set; }
        public string chatId { get; set; }
        public string buyerId { get; set; }

        public string status { get; set; }

        public Contractorder() {
            buyer = new ContractAccount();
            post = new ContractPosts();
        }
    }
}
