﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Logic.Models
{
    public class LogicOrder
    {
        public string orderId { get; set; }
        public LogicAccount buyer { get; set; }
        public LogicPosts post { get; set; }
        public LogicClientChat chat { get; set; }
        public string orderMessage { get; set; }
        public string postId { get; set; }
        public string chatId { get; set; }
        public string buyerId { get; set; }

        public string status { get; set; }

        public LogicOrder() {
            buyer = new LogicAccount();
            post = new LogicPosts();
        }

        public LogicOrder(Contract.Models.Contractorder dto)
        {
            this.orderId = dto.orderId;
            this.orderMessage = dto.orderMessage;
            this.postId = dto.postId;
            if (dto.post != null) {
                this.post = new LogicPosts(dto.post);
            }
            this.status = dto.status;
            if (dto.buyer != null)
            {
                this.buyer = new LogicAccount(dto.buyer);
            }
            this.buyerId = dto.buyerId;
            if (dto.chat != null)
            {
                this.chat = new LogicClientChat(dto.chat);
            }
            this.chatId = dto.chatId;
        }
    }
}
