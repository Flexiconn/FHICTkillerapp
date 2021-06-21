using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MockData
{
    public class MockOrderDbConnection : Contract.IOrder
    {
        private MySqlConnection connection;



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
            string query = $"SELECT Id FROM account WHERE SessionId=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", SessionId);

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

        public Contract.Models.ContractAccount GetAccount(string id)
        {
            open();
            string query = $"SELECT * FROM account WHERE Id=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Contract.Models.ContractAccount thisAccount = new Contract.Models.ContractAccount();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                thisAccount.SetAccount(dataReader["Id"].ToString(), dataReader["SessionId"].ToString(), dataReader["Name"].ToString(), dataReader["Password"].ToString(), dataReader["Balance"].ToString());
            }
            dataReader.Close();
            close();
            return thisAccount;
        }

        public string GetAccountName(string id)
        {
            open();
            string query = $"SELECT * FROM account WHERE Id=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
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

        public bool CheckIfSignedIn(string Id)
        {
            open();
            string query = $"SELECT SessionId FROM account WHERE SessionId=@SessionId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SessionId", Id);
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

        public List<Contract.Models.Contractorder> GetOrders(string Id)
        {
            open();
            string query = $"SELECT * FROM `order` WHERE BuyerId=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Contract.Models.Contractorder> orders = new List<Contract.Models.Contractorder>();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                orders.Add(new Contract.Models.Contractorder() { orderId = dataReader["OrderId"].ToString(), postId = dataReader["PostId"].ToString(), status = dataReader["Status"].ToString(), chatId = dataReader["ChatId"].ToString() });
            }
            dataReader.Close();
            close();
            return orders;
        }

        public List<Contract.Models.Contractorder> GetOrdersIncoming(string Id)
        {
            open();
            string query = $"SELECT * FROM `order` INNER JOIN post ON order.PostId = post.PostId WHERE post.PostAuthor=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            List<Contract.Models.Contractorder> orders = new List<Contract.Models.Contractorder>();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                orders.Add(new Contract.Models.Contractorder() { postId = dataReader["PostId"].ToString(), status = dataReader["Status"].ToString(), chatId = dataReader["ChatId"].ToString() });
            }
            dataReader.Close();
            close();
            return orders;
        }

        public void AddOrder(string OrderId, string id, string postId, string ChatId)
        {
            open();
            string query = $"INSERT INTO `order` (OrderId, BuyerId, PostId, ChatId, Status) VALUES (@OrderId, @BuyerUd, @PostId, @ChatId,'ordered');";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BuyerUd", id);
            cmd.Parameters.AddWithValue("@OrderId", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("@PostId", postId);
            cmd.Parameters.AddWithValue("@ChatId", Guid.NewGuid().ToString());

            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public string GetOrderStatus(string OrderId)
        {
            open();
            string status = "";
            string query = $"SELECT * FROM `order` WHERE OrderId=@OrderId ;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@OrderId", OrderId);
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
            string query = $"UPDATE `order` SET Status=@Status WHERE OrderId=@OrderId;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@OrderId", OrderId);
            cmd.Parameters.AddWithValue("@Status", Status);

            cmd.ExecuteNonQuery();
            close();
            return Status;
        }

        public string GetOrderOwner(string OrderId)
        {
            open();
            //Create a data reader and Execute the command
            string owner = "";
            string query = $"SELECT * FROM `order` INNER JOIN `post` ON order.PostId=post.PostId WHERE OrderId=@OrderId;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@OrderId", OrderId);
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

        public Contract.Models.ContractPosts GetPost(string id)
        {

            open();
            Contract.Models.ContractPosts post = new Contract.Models.ContractPosts();
            string query = $"SELECT * FROM post WHERE PostId=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Contract.Models.ContractPosts> postsList = new List<Contract.Models.ContractPosts>();
            //Read the data and store them in the list
            while (dataReader.Read())
            {

                post = new Contract.Models.ContractPosts() { PostAuthor = new Contract.Models.ContractAccount() { Id = dataReader["PostAuthor"].ToString() }, PostId = dataReader["PostId"].ToString(), PostName = dataReader["PostName"].ToString(), PostDescription = dataReader["PostDescription"].ToString() };
            }
            dataReader.Close();

            query = $"SELECT * FROM images WHERE Parent=@Id";
            cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            //Create a data reader and Execute the command
            dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                post.images.Add(dataReader["Path"].ToString());
            }


            dataReader.Close();

            close();
            return (post);
        }

    }
}
