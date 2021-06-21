using System;
using System.Collections.Generic;
using System.Text;
using MockData;

namespace Factory
{
    public class MockFactory
    {
        public static Contract.IChat GetChatDAL()
        {
            Contract.IChat chat = new MockChatDbConnection();
            return chat;
        }
        public static Contract.IBackPanel GetBackpanelDAL()
        {
            Contract.IBackPanel BackPanel = new MockBackPanelDbConnection();
            return BackPanel;
        }
        public static Contract.IPost GetPostDAL()
        {
            Contract.IPost Post = new MockPostDbConnection();
            return Post;
        }
        public static Contract.IAccount GetAccountDAL()
        {
            Contract.IAccount Account = new MockAccountDbConnection();
            return Account;
        }

        public static Contract.IOrder GetOrderDAL()
        {
            Contract.IOrder Order = new MockOrderDbConnection();
            return Order;
        }
        public static Contract.IReport GetReportDAL()
        {
            Contract.IReport Report = new MockReportDbConnection();
            return Report;
        }
    }
}
