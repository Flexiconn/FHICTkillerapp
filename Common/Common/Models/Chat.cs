using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
    public class Chat
    {
        [Key]
        public Guid MessageId { get; set; }
        public string ChatId { get; set; }
        public Account Account { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
