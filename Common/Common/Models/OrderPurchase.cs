using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
    public class OrderPurchase
    {
        [Key]
        public Guid OrderId { get; set; }
        public Account Seller { get; set; }
        public Account Buyer { get; set; }
        public Chat Chat { get; set; }
        public Posts Post { get; set; }
        public string OrderRequest { get; set; }
    }
}
