using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class ViewBackPanel
    {
        public List<ViewOrder> orders { get; set; }
        public int earnings  { get; set; }
        public ViewAccount Account { get; set; }

        public ViewBackPanel() {
            orders = new List<ViewOrder>();
        }

        public ViewBackPanel(Logic.Models.LogicBackPanel dto)
        {
            this.orders = new List<ViewOrder>();
            foreach (var t in dto.orders) {
                this.orders.Add(new ViewOrder() {buyer = new ViewAccount(t.buyer), buyerId = t.buyerId, chat = new ViewClientChat() {Account = new ViewAccount() { Name = t.chat.Account.Name }, ChatId = t.chatId, DateTime = t.chat.DateTime, Message = t.chat.Message, MessageId = t.chat.MessageId, Sender = t.chat.Sender }, chatId = t.chatId, orderId = t.orderId, orderMessage = t.orderMessage, post = new ViewPosts() {  } });
            }
            earnings = dto.earnings;
        }
    }
}
