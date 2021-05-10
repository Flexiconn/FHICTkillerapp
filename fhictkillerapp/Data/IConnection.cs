using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    interface IConnection
    {
        void start();
        Account GetAccount(string id);
        void AddPost(PostUpload insertPost, string sesId);
        List<Posts> GetPosts();
        string GetAccountName(string id);
        Posts GetPost(string id);
        void CreateAccount(Account account);
        void AddOrder(order order);
        string LoginAccount(Account account);
        bool CheckIfSignedIn(string Id);
        void SendMessage(Chat Message, string id, string chatId);
        List<ClientChat> GetMessages(string chatId, string id);
        Account GetProfileInfo(string Id);
        List<order> GetOrders(string Id);
        List<order> GetOrdersIncoming(string Id);
        void AddFunds(float amount, string id);
        BackPanel GetEarnings(string id);
        void createReview(string id, Review review);
        void createReport(string id, Report report);
        List<Report> getReports(string id);
        void banUser(string adminId, string userId);
    }
}
