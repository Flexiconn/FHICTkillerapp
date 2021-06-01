using Data;
using System;
using Contract;

namespace Factory
{
    public static class Factory
    {
        public static Contract.IChat GetClassChat()
        {
            Contract.IChat chat = new Connection();
            return chat;
        }
        public static Contract.IBackPanel GetClassBackpanel()
        {
            Contract.IBackPanel BackPanel = new Connection();
            return BackPanel;
        }
        public static Contract.IPost GetClassPost()
        {
            Contract.IPost Post = new Connection();
            return Post;
        }
        public static Contract.IAccount GetClassAccount()
        {
            Contract.IAccount Account = new Connection();
            return Account;
        }
    }
}
