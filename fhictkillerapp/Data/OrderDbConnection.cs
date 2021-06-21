using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Data
{
    public class OrderDbConnection : Contract.IOrder
    {
        private MySqlConnection connection;

        public OrderDbConnection()
        {
            connection = new MySqlConnection(ConnenctionString.GetConnectionString());
        }

        private void open()
        {
            connection.Open();
        }

        private void close()
        {
            connection.Close();
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
