using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MockData
{
    public class AccountDbConnection : Contract.IAccount
    {
        public bool testMode;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public AccountDbConnection()
        {
            Initialize();
        }

        private void Initialize()
        {

            server = "localhost";
            database = "killerapp";
            uid = "root";
            password = "root";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private void open()
        {
            connection.Open();
        }

        private void close()
        {
            connection.Close();
        }

        public string GetAccountId(string SessionId)
        {
            return "testId";
        }

        public Contract.Models.Account GetAccount(string id)
        {

            return new Contract.Models.Account() { Id = "TestId", Balance = "69", Name= "TestName", Password = "TestPassword", SessionId = "TestSessionId" };
        }

        public string GetAccountName(string id)
        {
            
            return "TestName";
        }

 
        public void CreateAccount(string Password, string Name)
        {

        }

       
        public string LoginAccount(string Password, string Name)
        {
            
            return "TestSessionId";
        }

        public bool CheckIfSignedIn(string Id)
        {
            
            return true;
        }

        public Contract.Models.Account GetProfileInfo(string Id)
        {
            return new Contract.Models.Account() {Name = "TestName", Balance = "69" };
        }

        public List<Contract.Models.order> GetOrders(string Id)
        {
            List<Contract.Models.order> orders = new List<Contract.Models.order>();
            //Create a data reader and Execute the command
            orders.Add(new Contract.Models.order() { orderId = "TestOrderId", postId = "TestPostId", status = "Ordered", chatId = "TestChatId" });
            return orders;
        }

        public List<Contract.Models.order> GetOrdersIncoming(string Id)
        {
            List<Contract.Models.order> orders = new List<Contract.Models.order>();
            //Create a data reader and Execute the command
            orders.Add(new Contract.Models.order() { orderId = "TestOrderId", postId = "TestPostId", status = "Ordered", chatId = "TestChatId" });
            return orders;
        }

        public void AddFunds(float amount, string id)
        {
           
        }

        public void AddPFP(IFormFile pfp, string Id)
        {
            
        }

        public string GetPFP(string Id)
        {
            return "TestPath";
        }

        public string GetOrderStatus(string OrderId)
        {
            return "Ordered";
        }

        public string ChangeOrderStatus(string OrderId, string Status)
        {
            return "Ordered";
        }

        public string GetOwner(string OrderId)
        {
            return "TestId";
        }
    }
}
