using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Data
{
    public class PostDbConnection : Contract.IPost
    {
        private MySqlConnection connection;

        public PostDbConnection()
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

        public string GetAccountId(string SessionId)
        {
            open();
            string query = $"SELECT Id FROM account WHERE SessionId=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("Id", SessionId);
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
            cmd.Parameters.AddWithValue("Id", id);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Contract.Models.ContractAccount thisAccount = new Contract.Models.ContractAccount();
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

        public void AddImageToDB(string path, string parent, string id) {
            open();
            string query = $"INSERT INTO Images (Id, Path, Parent) VALUES(@Id, @Path, @Parent); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Path", path);
            cmd.Parameters.AddWithValue("@Parent", parent);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public System.Int64 PostAmount(string id)
        {
            open();
            string query = $"SELECT COUNT(PostId) FROM `post`INNER JOIN `account` ON post.PostAuthor=account.Id WHERE Id=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("Id", id);
            //Create a data reader and Execute the command
            //Read the data and store them in the list
            System.Int64 test = (System.Int64)cmd.ExecuteScalar();
            close();

            return test;
        }

        public void AddPost( string postId, string postName, string postDescription, string Id)
        {        
            open();
            string query = $"INSERT INTO post (PostId, PostName, PostDescription, PostAuthor) VALUES(@PostId, @PostName, @PostDescription, @PostAuthor); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PostId", postId);
            cmd.Parameters.AddWithValue("@PostName", postName);
            cmd.Parameters.AddWithValue("@PostDescription", postDescription);
            cmd.Parameters.AddWithValue("@PostAuthor", Id);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }



        public List<Contract.Models.ContractPosts> GetPosts()
        {
            open();
            string query = $"SELECT * FROM post";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Contract.Models.ContractPosts> postsList = new List<Contract.Models.ContractPosts>();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                postsList.Add(new Contract.Models.ContractPosts() { PostAuthor = new Contract.Models.ContractAccount() { Id = dataReader["PostAuthor"].ToString() }, PostId = dataReader["PostId"].ToString(), PostName = dataReader["PostName"].ToString(), PostDescription = dataReader["PostDescription"].ToString() });
            }
            dataReader.Close();

            query = $"SELECT * FROM images";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                foreach (var item in postsList)
                {
                    if (item.PostId == dataReader["Parent"].ToString())
                    {
                        item.images.Add(dataReader["Path"].ToString());
                    }
                }
            }


            dataReader.Close();
            close();
            return (postsList);
        }



        public string GetAccountName(string id)
        {
            open();
            string query = $"SELECT * FROM account WHERE Id=@PostId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PostId", id);
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

       
        public Contract.Models.ContractAccount GetProfileInfo(string Id)
        {
            open();

            string query = $"SELECT * FROM account WHERE Id=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            Contract.Models.ContractAccount acc = new Contract.Models.ContractAccount();
            while (dataReader.Read())
            {
                acc.Name = dataReader["Name"].ToString();
                acc.Balance = dataReader["Balance"].ToString();
            }
            dataReader.Close();
            close();
            return acc;
        }

       
        
 

        public void createReview(string id, string text, int score, string postId)
        {
            open();


            string query = $"INSERT INTO review (id, score, text, account, post) VALUES(@id, @score, @text, @account, @post);";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@text", text);
            cmd.Parameters.AddWithValue("@account", id);
            cmd.Parameters.AddWithValue("@post", postId);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public List<Contract.Models.ContractReview> GetReview(string postId)
        {
            open();
            List<Contract.Models.ContractReview> reviews = new List<Contract.Models.ContractReview>();
            string query = $"SELECT * FROM review INNER JOIN account ON review.account = account.Id WHERE post=@id ;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", postId);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                reviews.Add(new Contract.Models.ContractReview()
                {
                    Account = new Contract.Models.ContractAccount() { Name = dataReader["Name"].ToString() },
                    post = new Contract.Models.ContractPosts() { PostId = dataReader["post"].ToString() },
                    //score = Int32.Parse(dataReader["Id"].ToString()),
                    text = dataReader["text"].ToString(),
                    reviewId = dataReader["id"].ToString()
                });
            }
            dataReader.Close();

            return reviews;
        }

        public void createReport(string id, Contract.reportTypes reportType, Contract.reportReasons reportReason, string comment, string reportedId)
        {
            open();


            string query = $"INSERT INTO report (id, type, reason, comment, reportId, creator) VALUES(@id, @type, @reason, @comment, @reportId, @creator);";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("@type", reportType);
            cmd.Parameters.AddWithValue("@reason", reportReason);
            cmd.Parameters.AddWithValue("@comment", comment);
            cmd.Parameters.AddWithValue("@reportId", reportedId);
            cmd.Parameters.AddWithValue("@creator", id);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        
        
    }
}
