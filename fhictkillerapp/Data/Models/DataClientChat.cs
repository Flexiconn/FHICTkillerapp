using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class DataClientChat
    {
        public string AccountName { get; set; }
        public string MessageId { get; set; }
        public string ChatId { get; set; }
        public string Message { get; set; }
        public bool Sender { get; set; }
        public DateTime DateTime { get; set; }
    }
}
