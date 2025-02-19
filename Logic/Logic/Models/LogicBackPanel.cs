﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class LogicBackPanel
    {
        public List<LogicOrder> orders { get; set; }
        public int earnings  { get; set; }

        public LogicBackPanel() {
            orders = new List<LogicOrder>();
        }

        public LogicBackPanel(Contract.Models.ContractBackPanel dto)
        {
            this.orders = new List<LogicOrder>();
            foreach (var t in dto.orders) {
                this.orders.Add(new LogicOrder() {buyer = new LogicAccount(t.buyer), buyerId = t.buyerId, chat = new LogicClientChat() { Account = new LogicAccount() { Name = t.chat.account.Name}, ChatId = t.chatId, DateTime = t.chat.DateTime, Message = t.chat.Message, MessageId = t.chat.MessageId, Sender = t.chat.Sender }, chatId = t.chatId, orderId = t.orderId, orderMessage = t.orderMessage, post = new LogicPosts() {  } });
            }
            earnings = dto.earnings;
        }

        public LogicBackPanel(List<LogicOrder> Orders, int Earnings)
        {
            orders = Orders;
            earnings = Earnings;
        }

        public List<LogicOrder> GetOrders()
        {
            return orders;
        }
        public int GetEarnings()
        {
            return earnings;
        }
    }
}
