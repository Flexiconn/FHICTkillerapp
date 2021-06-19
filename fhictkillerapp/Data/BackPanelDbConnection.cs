﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class BackPanelDbConnection : Contract.IBackPanel
    {

        private MySqlConnection connection;


        public BackPanelDbConnection()
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

       
        public bool CheckIfSignedIn(string Id)
        {
            open();
            string query = $"SELECT SessionId FROM account WHERE SessionId=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", Id);

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

        
        public Contract.Models.ContractBackPanel GetEarnings(string id)
        {
            open();
            Contract.Models.ContractBackPanel backPanel = new Contract.Models.ContractBackPanel();
            string query = $"SELECT * FROM `order` INNER JOIN post ON order.PostId = post.PostId WHERE post.PostAuthor=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<string> orders = new List<string>();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                if (dataReader["Status"].ToString() == "Delivered")
                {
                    backPanel.orders.Add(new Contract.Models.Contractorder() { postId = dataReader["PostId"].ToString(), buyerId = dataReader["BuyerId"].ToString() });
                    backPanel.earnings = backPanel.earnings + (int)dataReader["PostPrice"];
                }
            }
            dataReader.Close();

            close();
            return backPanel;
        }

        public bool CheckIfAdmin(string id) {
            bool admin = false;
            open();
            string query = $"SELECT * FROM account WHERE Id=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                id = dataReader["Id"].ToString();
                admin = (bool)dataReader["admin"];

            }
            dataReader.Close();
            close();
            return admin;
        }
        
        public List<Contract.Models.ContractReport> getReports(string id)
        {
            open();
            List<Contract.Models.ContractReport> reports = new List<Contract.Models.ContractReport>();
            string query = $"SELECT * FROM report INNER JOIN account ON report.creator = account.Id WHERE status='open'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                reports.Add(new Contract.Models.ContractReport()
                {
                    creatorId = new Contract.Models.ContractAccount() { Name = dataReader["Name"].ToString() },
                    reportComment = dataReader["comment"].ToString(),
                    reportId = dataReader["reportId"].ToString(),
                    ReportType = Int32.Parse(dataReader["type"].ToString()),
                    reportReason = Int32.Parse(dataReader["reason"].ToString())
                });
            }
            dataReader.Close();
            close();
            return reports;
        }

        public void banUser(string adminId, string userId)
        {
            open();
            string query = $"UPDATE account SET ban=1 WHERE Id=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", userId);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        

        public string GetPostByReviewId(string Id)
        {
            open();
            string query = $"SELECT * FROM review WHERE Id=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                Id = dataReader["Parent"].ToString();
            }
            dataReader.Close();

            close();
            return Id;
        }

        public Contract.Models.ContractReview GetReportReview(string reportId)
        {
            open();
            Contract.Models.ContractReview review = new Contract.Models.ContractReview();
            string query = $"SELECT * FROM review  WHERE id=@Id ;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", reportId);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                review = new Contract.Models.ContractReview()
                {
                    Account = new Contract.Models.ContractAccount() { Id = dataReader["account"].ToString() },
                    post = new Contract.Models.ContractPosts() { PostId = dataReader["post"].ToString() },
                    //score = Int32.Parse(dataReader["Id"].ToString()),
                    text = dataReader["text"].ToString(),
                    reviewId = dataReader["id"].ToString()
                };
            }
            dataReader.Close();

            return review;
        }
    }
}
