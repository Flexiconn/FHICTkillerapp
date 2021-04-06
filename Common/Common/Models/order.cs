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
    }
}
