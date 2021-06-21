using System;
using System.Collections.Generic;
using System.Text;
using Logic.Models;

namespace Logic
{
    public class OrderContainer
    {
        readonly Contract.IOrder IOrder;
        readonly Contract.IAccount IAccount;

        public bool cancelOrder(string OrderId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (IOrder.GetOrderOwner(OrderId) == IAccount.GetAccountId(SessionId))
                {
                    if (IOrder.GetOrderStatus(OrderId) == "ordered")
                    {
                        IOrder.ChangeOrderStatus(OrderId, "cancelled");
                        return true;
                    }

                    if (IOrder.GetOrderStatus(OrderId) == "accepted")
                    {
                        IOrder.ChangeOrderStatus(OrderId, "cancelled");
                        return true;
                    }
                }
                else
                {
                    if (IOrder.GetOrderStatus(OrderId) == "ordered")
                    {
                        IOrder.ChangeOrderStatus(OrderId, "cancelled");
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public bool OrderPost(string orderMessage, string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (IOrder.GetPost(postId).PostId == postId)
                {
                    if (Int32.Parse(IAccount.GetAccount(IAccount.GetAccountId(SessionId)).Balance) > 10)
                    {
                        IOrder.AddOrder(Guid.NewGuid().ToString(), IAccount.GetAccountId(SessionId), postId, Guid.NewGuid().ToString());
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (IAccount.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }

        public LogicmyAccountModel GetOrders(string Id) {
            
            if (CheckIfSignedIn(Id)) {
                return new LogicmyAccountModel(new Contract.Models.ContractmyAccountModel(IOrder.GetOrdersIncoming(IAccount.GetAccountId(Id)), IOrder.GetOrders(IAccount.GetAccountId(Id))));
            }
            return new LogicmyAccountModel();
        }

        public bool AcceptOrder(string OrderId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                if (IOrder.GetOrderOwner(OrderId) == new LogicAccount(IAccount.GetAccount(IAccount.GetAccountId(SessionId))).GetSessionId())
                {
                    if (IOrder.GetOrderStatus(OrderId) == "ordered")
                    {
                        IOrder.ChangeOrderStatus(OrderId, "accepted");
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        public OrderContainer() {
            IOrder = Factory.Factory.GetOrderDAL();
            IAccount = Factory.Factory.GetAccountDAL();
        }

        public OrderContainer(string mode)
        {
            if(mode == "mock")
            IOrder = Factory.MockFactory.GetOrderDAL();
            IAccount = Factory.MockFactory.GetAccountDAL();
        }
    }
}
