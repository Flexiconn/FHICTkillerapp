using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ClientChat
    {
        public string MessageId { get; set; }
        public string ChatId { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
