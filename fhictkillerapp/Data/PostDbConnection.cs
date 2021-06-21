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

        public void AddFavourite(string Id, string AccountId, string PostId) {
            open();
            string query = $"INSERT INTO favourites (Id, UserId, PostId) VALUES(@Id, @UserId, @PostId);";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@UserId", AccountId);
            cmd.Parameters.AddWithValue("@PostId", PostId);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public List<Contract.Models.ContractFavourite> GetFavourites(string UserId)
        {
            open();
            List<Contract.Models.ContractFavourite> favourites = new List<Contract.Models.ContractFavourite>();
            string query = $"SELECT * FROM favourites WHERE UserId=@UserId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                favourites.Add(new Contract.Models.ContractFavourite()
                {
                    Id = dataReader["Id"].ToString(),
                    Account = new Contract.Models.ContractAccount() { Id = dataReader["UserId"].ToString() },
                    Post = new Contract.Models.ContractPosts() { PostId = dataReader["PostId"].ToString() }
                });
            }
            dataReader.Close();
            close();
            return favourites;
        }

        public void RemoveFavourite(string Id)
        {
            open();
            string query = $"DELETE FROM favourites WHERE Id=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", Id);

            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }
    }
}
