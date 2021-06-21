using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class LogicmyAccountModel
    {
        public string PFP { get; set; }
        public List<LogicOrder> ordersIncoming { get; set; }
        public List<LogicOrder> ordersOutgoing { get; set; }
        public LogicAccount account { get; set; }

        public LogicmyAccountModel() {
            ordersIncoming = new List<LogicOrder>();
            ordersOutgoing = new List<LogicOrder>();
        }

        public LogicmyAccountModel(string pfp, LogicAccount newAccount)
        {
            PFP = pfp;
            account = newAccount;
        }

        public LogicmyAccountModel(List<LogicOrder> incoming, List<LogicOrder> outgoing)
        {
            ordersIncoming = incoming;
            ordersOutgoing = outgoing;
        }

        public LogicmyAccountModel(Contract.Models.ContractmyAccountModel dto) {
            this.ordersIncoming = new List<LogicOrder>();
            this.ordersOutgoing = new List<LogicOrder>();
            if (dto.ordersIncoming.Count > 0)
            {
                foreach (var t in dto.ordersIncoming)
                {
                    this.ordersIncoming.Add(new LogicOrder() { buyer = new LogicAccount(t.buyer), buyerId = t.buyerId, chat = new LogicClientChat() { }, chatId = t.chatId, orderId = t.orderId, orderMessage = t.orderMessage, post = new LogicPosts() { } });
                }
            }

            if (dto.ordersOutgoing.Count > 0)
            {
                foreach (var t in dto.ordersOutgoing)
                {
                    this.ordersOutgoing.Add(new LogicOrder() { buyer = new LogicAccount(t.buyer), buyerId = t.buyerId, chat = new LogicClientChat() { }, chatId = t.chatId, orderId = t.orderId, orderMessage = t.orderMessage, post = new LogicPosts() { } });
                }
            }
            this.PFP = dto.PFP;
            if (dto.account != null) {
                this.account = new LogicAccount(dto.account);
            }
        }

        public string GetPFP() {
            return PFP;
        }

        public List<LogicOrder> GetIncomingOrders() {
            return ordersIncoming;
        }

        public List<LogicOrder> GetOutgoingOrders()
        {
            return ordersOutgoing;
        }

        public LogicAccount GetAccount() {
            return account;
        }
    }
}
