using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class ViewClientChat
    {
        public ViewAccount Account { get; set; }
        public string MessageId { get; set; }
        public string ChatId { get; set; }
        public string Message { get; set; }
        public bool Sender { get; set; }
        public DateTime DateTime { get; set; }

        public ViewClientChat() {
        
        }

        public ViewClientChat(Logic.Models.LogicClientChat dto) {
            this.Account = new ViewAccount(dto.Account);
            this.ChatId = dto.ChatId;
            this.DateTime = dto.DateTime;
            this.Message = dto.Message;
            this.MessageId = dto.MessageId;
            this.Sender = dto.Sender;
        }
    }
}
