using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class BackPanel
    {
        public List<Order> orders { get; set; }
        public int earnings  { get; set; }
        public string userName { get; set; }

        public BackPanel() {
            orders = new List<Order>();
        }

        public BackPanel(Logic.Models.LogicBackPanel dto)
        {
            this.orders = new List<Order>();
            foreach (var t in dto.orders) {
                this.orders.Add(new Order() {buyer = new Account(t.buyer), buyerId = t.buyerId, chat = new ClientChat() {AccountName = t.chat.AccountName, ChatId = t.chatId, DateTime = t.chat.DateTime, Message = t.chat.Message, MessageId = t.chat.MessageId, Sender = t.chat.Sender }, chatId = t.chatId, orderId = t.orderId, orderMessage = t.orderMessage, post = new Posts() {  } });
            }
            earnings = dto.earnings;
            userName = dto.userName;
        }
    }
}
