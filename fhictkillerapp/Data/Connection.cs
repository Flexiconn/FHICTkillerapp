using Common;
using Common.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Connection : IConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Connection() {
            Initialize();


        }
        public void start() 
        {
            Initialize();
            //open();
            //close();
        }
        private void Initialize()
        {

            server = "localhost";
            database = "killer";
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

        public Account GetAccount(string id)
        {
            open();
            string query = $"SELECT * FROM account WHERE SessionId='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Account thisAccount = new Account();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                thisAccount.Id = dataReader["Id"].ToString();
                thisAccount.SessionId = dataReader["SessionId"].ToString();
                thisAccount.Name = dataReader["Name"].ToString();
                thisAccount.Password = dataReader["Password"].ToString();
            }
            dataReader.Close();
            close();
            return thisAccount;
        }

        public void AddPost(PostUpload insertPost, string sesId)
        {
            open();
            //string pathString = System.IO.Path.Combine("wwwroot/Data/IMG/", insertPost.PostId);
            //System.IO.Directory.CreateDirectory(pathString);
            //insertPost.MyImage.CopyTo(new FileStream(System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString()), FileMode.Create));


            //posts.PostFileName = System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString());

            string query = $"INSERT INTO post (PostId, PostName, PostDescription, PostAuthor) VALUES('{insertPost.PostId}', '{insertPost.PostName}', '{insertPost.PostDescription}','53EFCF09-36AF-4B04-A61C-27A1625C96C1'); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }



        public List<Posts> GetPosts()
        {
            open();
            string query = $"SELECT * FROM post";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Posts> postsList = new List<Posts>();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                postsList.Add(new Posts() { PostAuthor = dataReader["PostAuthor"].ToString(), PostId = dataReader["PostId"].ToString(), PostName = dataReader["PostName"].ToString(), PostDescription = dataReader["PostDescription"].ToString() });
            }
            dataReader.Close();
            close();
            return (postsList);
        }



        public string GetAccountName(string id)
        {
            open();
            string query = $"SELECT * FROM post WHERE PostId='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            var name = dataReader["Name"].ToString();
            dataReader.Close();
            close();
            return (name);
        }

        public Posts GetPost(string id)
        {

            open();
            string query = $"SELECT * FROM post WHERE PostId='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Posts> postsList = new List<Posts>();
            //Read the data and store them in the list
            dataReader.Read();

            Posts post = new Posts() { PostAuthor = dataReader["PostAuthor"].ToString(), PostId = dataReader["PostId"].ToString(), PostName = dataReader["PostName"].ToString(), PostDescription = dataReader["PostDescription"].ToString() };
            
            dataReader.Close();
            close();
            return (post);
        }

        public void CreateAccount(Account account)
        {
            open();
            account.Id = Guid.NewGuid().ToString().ToUpper();
            account.SessionId = null;
            string query = $"INSERT INTO account (Id, SessionId, Name, Password) VALUES('{account.Id}', '{account.SessionId}', '{account.Name}','{account.Password}'); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public void AddOrder(order order)
        {
            open();
            order.orderId = Guid.NewGuid();
            string query = $"INSERT INTO `order` (OrderId, BuyerId, PostId, ChatId, Status) VALUES ('{order.orderId.ToString()}', '{order.buyer.Id}', '{order.post.PostId}','{Guid.NewGuid().ToString()}','false');";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public string LoginAccount(Account account)
        {
            open();
            string query = $"SELECT * FROM account WHERE Password='{account.Password}' AND Name='{account.Name}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Account acc = new Account();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                acc.Name = dataReader["Name"].ToString();
                acc.Password = dataReader["Password"].ToString();
                acc.Id = dataReader["Id"].ToString();
            }
            dataReader.Close();
            if (acc != null && acc.Name == account.Name && acc.Password == account.Password)
            {
                acc.SessionId = Guid.NewGuid().ToString();
                query = $"UPDATE account SET SessionId='{acc.SessionId}' WHERE Id='{acc.Id}'";
                cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                close();
                return (acc.SessionId);
            }
            close();
            return ("nope");
        }

        public bool CheckIfSignedIn(string Id)
        {
            open();
            string query = $"SELECT SessionId FROM account WHERE SessionId='{Id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                if (Id != null && Id == dataReader["SessionId"].ToString())
                {
                    dataReader.Close();
                    close();
                    return true;
                }
            }
            dataReader.Close();
            close();
            return false;
        }

        public void SendMessage(Chat Message, string id, string chatid)
        {
            Console.WriteLine(chatid);
            open();
            Message.DateTime = DateTime.Now;
            string query = $"SELECT Id FROM account WHERE SessionId='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            dataReader.Read();
            var Idd = dataReader["Id"].ToString();
            dataReader.Close();
            
            query = $"INSERT INTO chat (MessageId, chatId, AccountId, Message, DateTime) VALUES('{Guid.NewGuid().ToString()}','{chatid}','{Idd}','{Message.Message}','{Message.DateTime}')";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public List<ClientChat> GetMessages(string chatId, string id)
        {
            open();
            List<ClientChat> msgs = new List<ClientChat>();
            string query = $"SELECT * FROM `chat` INNER JOIN account ON chat.AccountId = account.Id WHERE chatId ='{chatId}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                msgs.Add(new ClientChat() { Message = dataReader["Message"].ToString(), MessageId = dataReader["MessageId"].ToString(), AccountName = dataReader["Name"].ToString(), DateTime = DateTime.Parse(dataReader["DateTime"].ToString()) });
                if (dataReader["SessionId"].ToString() == id)
                {
                    msgs.Last().Sender = true;
                }
                else {
                    msgs.Last().Sender = false;
                }
            }
            dataReader.Close();
            close();
            var persons = from p in msgs
                          orderby p.DateTime
                          select p;
            return persons.ToList();
        }

        public Account GetProfileInfo(string Id) {
            open();
            
            string query = $"SELECT * FROM account WHERE SessionId='{Id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            Account acc = new Account();
            while (dataReader.Read())
            {
                acc.Name = dataReader["Name"].ToString();
                acc.Balance = dataReader["Balance"].ToString();
            }
            dataReader.Close();
            close();
            return acc;
        }

        public List<order> GetOrders(string Id)
        {
            open();
            string query = $"SELECT * FROM account WHERE SessionId='{Id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                Id = dataReader["Id"].ToString();
            }
            dataReader.Close();

            query = $"SELECT * FROM `order` WHERE BuyerId='{Id}';";
            cmd = new MySqlCommand(query, connection);
            dataReader = cmd.ExecuteReader();
            List<order> orders = new List<order>();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                orders.Add(new order() { postId = dataReader["PostId"].ToString(), status = dataReader["Status"].ToString(), chatId = dataReader["ChatId"].ToString() });
            }
            dataReader.Close();
            close();
            return orders;
        }

        public List<order> GetOrdersIncoming(string Id) {
            open();
            string query = $"SELECT * FROM account WHERE SessionId='{Id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                Id = dataReader["Id"].ToString();
            }
            dataReader.Close();

            query = $"SELECT * FROM `order` INNER JOIN post ON order.PostId = post.PostId WHERE post.PostAuthor = '{Id}';";
            cmd = new MySqlCommand(query, connection);
            dataReader = cmd.ExecuteReader();
            List<order> orders = new List<order>();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                orders.Add(new order() { postId = dataReader["PostId"].ToString(), status = dataReader["Status"].ToString(), chatId = dataReader["ChatId"].ToString() });
            }
            dataReader.Close();
            close();
            return orders;
        }

        public void AddFunds(float amount, string id)
        {
            open();
            float funds;
            string query = $"SELECT * FROM account WHERE SessionId='{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            dataReader.Read();

            id = dataReader["Id"].ToString();
            funds = (float)dataReader["Balance"];
            
            dataReader.Close();
            funds = funds + amount;
            Console.WriteLine("funds: " + funds);
            query = $"UPDATE account SET Balance='{funds}' WHERE Id='{id}';";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public BackPanel GetEarnings(string id)
        {
            open();
            BackPanel backPanel = new BackPanel();
            string query = $"SELECT * FROM account WHERE SessionId='{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            dataReader.Read();

            id = dataReader["Id"].ToString();
            dataReader.Close();

            query = $"SELECT * FROM `order` INNER JOIN post ON order.PostId = post.PostId WHERE post.PostAuthor = '{id}';";
            cmd = new MySqlCommand(query, connection);
            dataReader = cmd.ExecuteReader();
            List<string> orders = new List<string>();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                if (dataReader["Status"].ToString() == "Delivered") {
                    backPanel.orders.Add(new order() { postId = dataReader["PostId"].ToString(), buyerId = dataReader["BuyerId"].ToString() });
                    backPanel.earnings = backPanel.earnings + (int)dataReader["PostPrice"];
                }
            }
            dataReader.Close();

            close();
            return backPanel;
        }
    }
}
