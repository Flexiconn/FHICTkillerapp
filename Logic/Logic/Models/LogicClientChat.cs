using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class LogicClientChat
    {
        public string AccountName { get; set; }
        public string MessageId { get; set; }
        public string ChatId { get; set; }
        public string Message { get; set; }
        public bool Sender { get; set; }
        public DateTime DateTime { get; set; }

        public LogicClientChat() {
        
        }

        public LogicClientChat(Contract.Models.ClientChat dto) {
            this.AccountName = dto.AccountName;
            this.ChatId = dto.ChatId;
            this.DateTime = dto.DateTime;
            this.Message = dto.Message;
            this.MessageId = dto.MessageId;
            this.Sender = dto.Sender;
        }
    }
}
