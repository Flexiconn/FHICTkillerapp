using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fhictkillerapp.Models
{
    public class ViewOrder
    {
        public string orderId { get; set; }
        public ViewAccount buyer { get; set; }
        public ViewPosts post { get; set; }
        public ViewClientChat chat { get; set; }
        public string orderMessage { get; set; }
        public string postId { get; set; }
        public string chatId { get; set; }
        public string buyerId { get; set; }

        public string status { get; set; }

        public ViewOrder() {
            buyer = new ViewAccount();
            post = new ViewPosts();
        }

        public ViewOrder(Logic.Models.LogicOrder dto)
        {
            this.orderId = dto.orderId;
            this.orderMessage = dto.orderMessage;
            this.postId = dto.postId;
            this.post = new ViewPosts(dto.post);
            this.status = dto.status;
            this.buyer = new ViewAccount(dto.buyer);
            this.buyerId = dto.buyerId;
            this.chat = new ViewClientChat(dto.chat);
            this.chatId = dto.chatId;
        }
    }
}
