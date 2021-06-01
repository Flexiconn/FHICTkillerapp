using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fhictkillerapp.Models
{
    public class Order
    {
        public Guid orderId { get; set; }
        public Account buyer { get; set; }
        public Posts post { get; set; }
        public ClientChat chat { get; set; }
        public string orderMessage { get; set; }
        public string postId { get; set; }
        public string chatId { get; set; }
        public string buyerId { get; set; }

        public string status { get; set; }

        public Order() {
            buyer = new Account();
            post = new Posts();
        }

        public Order(Logic.Models.LogicOrder dto)
        {
            this.orderId = dto.orderId;
            this.orderMessage = dto.orderMessage;
            this.postId = dto.postId;
            this.post = new Posts(dto.post);
            this.status = dto.status;
            this.buyer = new Account(dto.buyer);
            this.buyerId = dto.buyerId;
            this.chat = new ClientChat(dto.chat);
            this.chatId = dto.chatId;
        }
    }
}
