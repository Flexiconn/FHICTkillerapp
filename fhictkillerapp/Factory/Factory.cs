﻿using Data;
using System;
using Contract;

namespace Factory
{
    public static class Factory
    {
        public static Contract.IChat GetClassChat()
        {
            Contract.IChat chat = new ChatDbConnection();
            return chat;
        }
        public static Contract.IBackPanel GetClassBackpanel()
        {
            Contract.IBackPanel BackPanel = new BackPanelDbConnection();
            return BackPanel;
        }
        public static Contract.IPost GetClassPost()
        {
            Contract.IPost Post = new PostDbConnection();
            return Post;
        }
        public static Contract.IAccount GetClassAccount()
        {
            Contract.IAccount Account = new AccountDbConnection();
            return Account;
        }
    }
}
