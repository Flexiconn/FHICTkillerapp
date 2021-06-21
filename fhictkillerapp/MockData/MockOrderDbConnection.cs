using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MockData
{
    public class MockOrderDbConnection : Contract.IOrder
    {
        public List<Contract.Models.Contractorder> GetOrders(string Id)
        {
            List<Contract.Models.Contractorder> orders = new List<Contract.Models.Contractorder>();

            orders.Add(new Contract.Models.Contractorder() { post = new Contract.Models.ContractPosts(), chat = new Contract.Models.ContractClientChat(), buyer = new Contract.Models.ContractAccount(), orderId = "test" });

            return orders;
        }

        public List<Contract.Models.Contractorder> GetOrdersIncoming(string Id)
        {

            List<Contract.Models.Contractorder> orders = new List<Contract.Models.Contractorder>();

                orders.Add(new Contract.Models.Contractorder() { post = new Contract.Models.ContractPosts(), chat = new Contract.Models.ContractClientChat(), buyer = new Contract.Models.ContractAccount(), orderId = "test" });

            return orders;
        }

        public void AddOrder(string OrderId, string id, string postId, string ChatId)
        {

        }

        public string GetOrderStatus(string OrderId)
        {
            string status = "";
            return status;
        }

        public string ChangeOrderStatus(string OrderId, string Status)
        {
            return Status;
        }

        public string GetOrderOwner(string OrderId)
        {
            string owner = "";
            return owner;
        }

        public Contract.Models.ContractPosts GetPost(string id)
        {

            
            return new Contract.Models.ContractPosts() { PostAuthor = new Contract.Models.ContractAccount(), PostId = "test"};
        }

    }
}
