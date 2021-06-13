using System;
using System.Collections.Generic;
using System.Text;
using MockData;

namespace Factory
{
    public class MockFactory
    {
        public static Contract.IChat GetClassChat()
        {
            Contract.IChat chat = new MockChatDbConnection();
            return chat;
        }
        public static Contract.IBackPanel GetClassBackpanel()
        {
            Contract.IBackPanel BackPanel = new MockBackPanelDbConnection();
            return BackPanel;
        }
        public static Contract.IPost GetClassPost()
        {
            Contract.IPost Post = new MockPostDbConnection();
            return Post;
        }
        public static Contract.IAccount GetClassAccount()
        {
            Contract.IAccount Account = new MockAccountDbConnection();
            return Account;
        }
    }
}
