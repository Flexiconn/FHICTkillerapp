﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Logic
{
    public class Querries
    {
        CliverrContext db = new CliverrContext();
        public void DbTest() 
        {
            db.Database.EnsureCreated();
        }

        public void AddPost(PostUpload insertPost,string sesId) 
        {
            string pathString = System.IO.Path.Combine("wwwroot/Data/IMG/", insertPost.PostId);
            System.IO.Directory.CreateDirectory(pathString);
            insertPost.MyImage.CopyTo(new FileStream(System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString()), FileMode.Create));

            Posts posts = new Posts();
            posts.PostId = insertPost.PostId;
            posts.PostName = insertPost.PostName;
            posts.PostDescription = insertPost.PostDescription;
            posts.PostFileName = System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString());
            db.Account.Where(b => b.SessionId == sesId).FirstOrDefault().Posts = posts;
            db.SaveChanges();
        }

        public List<Posts> GetPosts()
        {
            List<Posts> postsList = new List<Posts>();
            var postList = db.Posts.Select(x => x).ToList();
            postsList.AddRange(db.Posts.Select(x => x).ToList());
            return (postsList);
        }

        public Account GetAccount(string id)
        {
            return (db.Account.Where(b => b.SessionId == id).FirstOrDefault());
        }

        public string GetAccountName(string id)
        {
            return (db.Account.Where(b => b.Id.ToString() == id).FirstOrDefault().Name);
        }

        public Posts GetPost(string id)
        {
            Posts post = db.Posts.Where(b => b.PostId == id).FirstOrDefault();
            return (post);
        }

        public void CreateAccount(Account account)
        {
            account.Id = Guid.NewGuid().ToString().ToUpper();
            account.SessionId = null;
            db.Account.Add(account);
            db.SaveChanges();
            Console.WriteLine("succes: " + account.Id);
        }

        public void AddOrder(order order)
        {
            db.Add(order);
            db.SaveChanges();
        }

        public string LoginAccount(Account account)
        {
            var acc = db.Account.Where(b => b.Name == account.Name).FirstOrDefault();
            if(acc != null && acc.Name == account.Name && acc.Password == account.Password) 
            {
                acc.SessionId = Guid.NewGuid().ToString();
                db.SaveChanges();
                return (acc.SessionId);
            }

            return ("nope");
        }

        public bool CheckIfSignedIn(string Id) {
            if (Id != null && Id == db.Account.Where(b => b.SessionId == Id).FirstOrDefault().SessionId)
            {
                return true;
            }
            return false;
        }

        public void SendMessage(Chat Message, string id) 
        {
            Message.DateTime = DateTime.Now;
            Message.Account = db.Account.Where(b => b.SessionId == id).FirstOrDefault();
            db.Chat.Add(Message);
            db.SaveChanges();
        }

        public Dictionary<ClientChat,string> GetMessages(string chatId) 
        {
            Dictionary<ClientChat, string> msgs = new Dictionary<ClientChat, string>();
            var msg = db.Chat.Where(b => b.ChatId == chatId).ToList();
            foreach (var s in msg) 
            {
                ClientChat chat = new ClientChat();
                chat.ChatId = s.ChatId;
                chat.DateTime = s.DateTime;
                chat.Message = s.Message;
                chat.MessageId = s.MessageId;
                var tthis = db.Chat.FromSqlRaw("SELECT MessageId, AccountId, ChatId, DateTime, Message FROM Chat").ToList();
                Console.WriteLine("test " + tthis[0].Account);
                msgs.Add(chat, GetAccountName(s.Account.ToString()));
            }
            return msgs;
        }

        public List<ChatClient> GetMessagesClient(string chatId)
        {
            List<ChatClient> msgs = new List<ChatClient>();
            foreach (var item in db.Chat.Where(b => b.ChatId == chatId).ToList()) 
            {
                msgs.Add(new ChatClient(item.MessageId, "test name", item.Message, item.DateTime));
            }
            return msgs;
        }
    }
}
