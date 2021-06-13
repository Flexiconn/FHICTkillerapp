using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class ViewmyAccountModel
    {
        public string PFP { get; set; }
        public List<ViewOrder> ordersIncoming { get; set; }
        public List<ViewOrder> ordersOutgoing { get; set; }
        public ViewAccount account { get; set; }

        public ViewmyAccountModel() {
            ordersIncoming = new List<ViewOrder>();
            ordersOutgoing = new List<ViewOrder>();
        }

        public ViewmyAccountModel(Logic.Models.LogicmyAccountModel dto) {
            this.ordersIncoming = new List<ViewOrder>();
            this.ordersOutgoing = new List<ViewOrder>();
            if (dto.ordersIncoming.Count > 0)
            {
                foreach (var t in dto.ordersIncoming)
                {
                    this.ordersIncoming.Add(new ViewOrder() { buyer = new ViewAccount(t.buyer), buyerId = t.buyerId, chat = new ViewClientChat() { AccountName = t.chat.AccountName, ChatId = t.chatId, DateTime = t.chat.DateTime, Message = t.chat.Message, MessageId = t.chat.MessageId, Sender = t.chat.Sender }, chatId = t.chatId, orderId = t.orderId, orderMessage = t.orderMessage, post = new ViewPosts() { } });
                }
            }

            if (dto.ordersOutgoing.Count > 0)
            {
                foreach (var t in dto.ordersOutgoing)
                {
                    this.ordersOutgoing.Add(new ViewOrder() { buyer = new ViewAccount(t.buyer), buyerId = t.buyerId, chat = new ViewClientChat() { AccountName = t.chat.AccountName, ChatId = t.chatId, DateTime = t.chat.DateTime, Message = t.chat.Message, MessageId = t.chat.MessageId, Sender = t.chat.Sender }, chatId = t.chatId, orderId = t.orderId, orderMessage = t.orderMessage, post = new ViewPosts() { } });
                }
            }
            this.PFP = dto.PFP;
            this.account = new ViewAccount(dto.account);
        }
    }
}
