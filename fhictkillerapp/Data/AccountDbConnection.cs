using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Data
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
            open();
            string query = $"SELECT Id FROM account WHERE SessionId='{SessionId}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                SessionId = dataReader["Id"].ToString();
            }
            dataReader.Close();
            close();
            return SessionId;
        }

        public Contract.Models.Account GetAccount(string id)
        {
            open();
            string query = $"SELECT * FROM account WHERE Id='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Contract.Models.Account thisAccount = new Contract.Models.Account();
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

        public string GetAccountName(string id)
        {
            open();
            string query = $"SELECT * FROM account WHERE Id='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            var name = dataReader["Name"].ToString();
            dataReader.Close();
            close();
            if (name == null)
            {
                return "Not Found";
            }
            return (name);
        }

 
        public void CreateAccount(string Password, string Name)
        {
            open();
            string query = $"INSERT INTO account (Id, SessionId, Name, Password) VALUES('{Guid.NewGuid().ToString().ToUpper()}', '{null}', '{Name}','{Password}'); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

       
        public string LoginAccount(string Password, string Name)
        {
            open();
            string query = $"SELECT * FROM account WHERE Password='{Password}' AND Name='{Name}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Contract.Models.Account acc = new Contract.Models.Account();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                acc.Name = dataReader["Name"].ToString();
                acc.Password = dataReader["Password"].ToString();
                acc.Id = dataReader["Id"].ToString();
            }
            dataReader.Close();
            if (acc.Name == Name && acc.Password == Password)
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

        public Contract.Models.Account GetProfileInfo(string Id)
        {
            open();

            string query = $"SELECT * FROM account WHERE Id='{Id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            Contract.Models.Account acc = new Contract.Models.Account();
            while (dataReader.Read())
            {
                acc.Name = dataReader["Name"].ToString();
                acc.Balance = dataReader["Balance"].ToString();
            }
            dataReader.Close();
            close();
            return acc;
        }

        public List<Contract.Models.order> GetOrders(string Id)
        {
            open();
            string query = $"SELECT * FROM `order` WHERE BuyerId='{Id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Contract.Models.order> orders = new List<Contract.Models.order>();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                orders.Add(new Contract.Models.order() { orderId = dataReader["OrderId"].ToString(), postId = dataReader["PostId"].ToString(), status = dataReader["Status"].ToString(), chatId = dataReader["ChatId"].ToString() });
            }
            dataReader.Close();
            close();
            return orders;
        }

        public List<Contract.Models.order> GetOrdersIncoming(string Id)
        {
            open();
            string query = $"SELECT * FROM `order` INNER JOIN post ON order.PostId = post.PostId WHERE post.PostAuthor = '{Id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            List<Contract.Models.order> orders = new List<Contract.Models.order>();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                orders.Add(new Contract.Models.order() { postId = dataReader["PostId"].ToString(), status = dataReader["Status"].ToString(), chatId = dataReader["ChatId"].ToString() });
            }
            dataReader.Close();
            close();
            return orders;
        }

        public void AddFunds(float amount, string id)
        {
            open();
            float funds;
            string query = $"SELECT * FROM account WHERE Id='{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            dataReader.Read();


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

        public void AddPFP(IFormFile pfp, string Id)
        {
            open();


            string pathString = System.IO.Path.Combine("wwwroot/data/IMG/pfp/", Id);
            System.IO.Directory.CreateDirectory(pathString);
            pfp.CopyTo(new FileStream(System.IO.Path.Combine(pathString, pfp.FileName.ToString()), FileMode.Create));



            string query = $"INSERT INTO pfp (Id, Path, Parent) VALUES('{Guid.NewGuid().ToString()}', '{System.IO.Path.Combine(System.IO.Path.Combine("/data/IMG/pfp/", Id + "/"), pfp.FileName.ToString())}', '{Id}'); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public string GetPFP(string Id)
        {
            open();
            string path = "";
            string query = $"SELECT * FROM pfp WHERE Parent='{Id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                path = dataReader["Path"].ToString();
            }

            dataReader.Close();
            close();
            if (path == null)
                return "Not Found";
            return path;
        }

        public string GetOrderStatus(string OrderId)
        {
            open();
            string status = "";
            string query = $"SELECT * FROM `order` WHERE OrderId='{OrderId}' ;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                status = dataReader["Status"].ToString();
            }
            dataReader.Close();
            close();
            return status;
        }

        public string ChangeOrderStatus(string OrderId, string Status)
        {
            open();
            string query = $"UPDATE `order` SET Status='{Status}' WHERE OrderId='{OrderId}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            close();
            return Status;
        }

        public string GetOwner(string OrderId)
        {
            open();
            //Create a data reader and Execute the command
            string owner = "";
            string query = $"SELECT * FROM `order` INNER JOIN `post` ON order.PostId=post.PostId WHERE OrderId='{OrderId}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                owner = dataReader["PostAuthor"].ToString();
            }
            dataReader.Close();
            close();
            return owner;
        }
    }
}
