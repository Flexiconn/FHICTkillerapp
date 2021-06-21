using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IOrder
    {
        void AddOrder(string orderId, string id, string postId, string chatId);
        List<Contract.Models.Contractorder> GetOrders(string Id);
        List<Contract.Models.Contractorder> GetOrdersIncoming(string Id);
        string GetOrderStatus(string OrderId);
        string ChangeOrderStatus(string OrderId, string Status);
        string GetOrderOwner(string OrderId);
        Contract.Models.ContractPosts GetPost(string id);
    }
}
