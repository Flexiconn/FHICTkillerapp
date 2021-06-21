using Data;
using System;
using Contract;

namespace Factory
{
    public static class Factory
    {
        public static Contract.IChat GetChatDAL()
        {
            Contract.IChat chat = new ChatDbConnection();
            return chat;
        }
        public static Contract.IBackPanel GetBackpanelDAL()
        {
            Contract.IBackPanel BackPanel = new BackPanelDbConnection();
            return BackPanel;
        }
        public static Contract.IPost GetPostDAL()
        {
            Contract.IPost Post = new PostDbConnection();
            return Post;
        }
        public static Contract.IAccount GetAccountDAL()
        {
            Contract.IAccount Account = new AccountDbConnection();
            return Account;
        }

        public static Contract.IOrder GetOrderDAL()
        {
            Contract.IOrder Order = new OrderDbConnection();
            return Order;
        }

        public static Contract.IReport GetReportDAL()
        {
            Contract.IReport Report = new ReportDBConnection();
            return Report;
        }
    }
}
