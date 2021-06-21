using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class LogicClientChat
    {
        public LogicAccount Account { get; set; }
        public string MessageId { get; set; }
        public string ChatId { get; set; }
        public string Message { get; set; }
        public bool Sender { get; set; }
        public DateTime DateTime { get; set; }

        public LogicClientChat() {
        
        }

        public LogicClientChat(Contract.Models.ContractClientChat dto) {
            if (dto.account != null) {
                this.Account = new LogicAccount(dto.account);
            }
            this.ChatId = dto.ChatId;
            this.DateTime = dto.DateTime;
            this.Message = dto.Message;
            this.MessageId = dto.MessageId;
            this.Sender = dto.Sender;
        }

        public LogicClientChat(LogicAccount account, string messageId, string chatId, string message, bool sender, DateTime dateTime)
        {
            this.Account = account;
            this.ChatId = chatId;
            this.DateTime = dateTime;
            this.Message = message;
            this.MessageId = messageId;
            this.Sender = sender;
        }

        public LogicAccount GetAccount() {
            return Account;
        }

        public string GetMessage() {
            return Message;
        }

        public string GetMessageId()
        {
            return MessageId;
        }

        public bool GetSender()
        {
            return Sender;
        }

        public DateTime GetPostTime()
        {
            return DateTime;
        }

        public void SetSender(bool value)
        {
            Sender = value;
        }

    }
}
