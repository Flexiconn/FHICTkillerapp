using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
    public class ChatClient
    {
        [Key]
        public string MessageId { get; set; }
        public string AccountName { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

        public ChatClient(string messageId, string accountName, string message, DateTime dateTime) {
            this.AccountName = accountName;
            this.DateTime = dateTime;
            this.MessageId = messageId;
            this.Message = message;
        }
    }
}
