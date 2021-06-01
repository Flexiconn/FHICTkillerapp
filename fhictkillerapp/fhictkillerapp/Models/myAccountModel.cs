using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class myAccountModel
    {
        public string PFP { get; set; }
        public List<Order> ordersIncoming { get; set; }
        public List<Order> ordersOutgoing { get; set; }
        public Account account { get; set; }

        public myAccountModel() {
            ordersIncoming = new List<Order>();
            ordersOutgoing = new List<Order>();
        }

        public myAccountModel(Logic.Models.LogicmyAccountModel dto) {
            this.ordersIncoming = new List<Order>();
            this.ordersOutgoing = new List<Order>();
            foreach (var t in dto.ordersIncoming)
            {
                this.ordersIncoming.Add(new Order() { buyer = new Account(t.buyer), buyerId = t.buyerId, chat = new ClientChat() { AccountName = t.chat.AccountName, ChatId = t.chatId, DateTime = t.chat.DateTime, Message = t.chat.Message, MessageId = t.chat.MessageId, Sender = t.chat.Sender }, chatId = t.chatId, orderId = t.orderId, orderMessage = t.orderMessage, post = new Posts() { } });
            }

            foreach (var t in dto.ordersOutgoing)
            {
                this.ordersOutgoing.Add(new Order() { buyer = new Account(t.buyer), buyerId = t.buyerId, chat = new ClientChat() { AccountName = t.chat.AccountName, ChatId = t.chatId, DateTime = t.chat.DateTime, Message = t.chat.Message, MessageId = t.chat.MessageId, Sender = t.chat.Sender }, chatId = t.chatId, orderId = t.orderId, orderMessage = t.orderMessage, post = new Posts() { } });
            }
            this.PFP = dto.PFP;
            this.account = new Account(dto.account);
        }
    }
}
