using Data;
using Data.Interfaces;
using System;

namespace Factory
{
    public static class Factory
    {
        public static IChat GetClassChat()
        {
            IChat chat = new Connection();
            return chat;
        }
        public static IBackPanel GetClassBackpanel()
        {
            IBackPanel BackPanel = new Connection();
            return BackPanel;
        }
        public static IPost GetClassPost()
        {
            IPost Post = new Connection();
            return Post;
        }
        public static IAccount GetClassAccount()
        {
            IAccount Account = new Connection();
            return Account;
        }
    }
}
