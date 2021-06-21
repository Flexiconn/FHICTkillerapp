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
                    this.ordersIncoming.Add(new ViewOrder() { buyer = new ViewAccount(t.buyer), chat = new ViewClientChat() { ChatId = t.chatId}, status = t.status, post = new ViewPosts() { PostId = t.postId} , orderMessage = t.orderMessage});
                }
            }

            if (dto.ordersOutgoing.Count > 0)
            {
                foreach (var t in dto.ordersOutgoing)
                {
                    this.ordersOutgoing.Add(new ViewOrder() { buyer = new ViewAccount(t.buyer), chat = new ViewClientChat() { ChatId = t.chatId }, status = t.status, post = new ViewPosts() { PostId = t.postId }, orderMessage = t.orderMessage });
                }
            }
            this.PFP = dto.PFP;
            if (dto.account != null) {
                this.account = new ViewAccount(dto.account);
            }

        }
    }
}
