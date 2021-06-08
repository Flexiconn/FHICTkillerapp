using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockData
{
    public class BackPanelDbConnection : Contract.IBackPanel
    {
        public bool testMode;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public BackPanelDbConnection()
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

        public Contract.Models.Posts GetPost(string id)
        {

            open();
            Contract.Models.Posts post = new Contract.Models.Posts();
            string query = $"SELECT * FROM post WHERE PostId='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Contract.Models.Posts> postsList = new List<Contract.Models.Posts>();
            //Read the data and store them in the list
            while (dataReader.Read())
            {

                post = new Contract.Models.Posts() { PostAuthor = dataReader["PostAuthor"].ToString(), PostId = dataReader["PostId"].ToString(), PostName = dataReader["PostName"].ToString(), PostDescription = dataReader["PostDescription"].ToString() };
            }
            dataReader.Close();

            query = $"SELECT * FROM images WHERE Parent='{id}'";
            cmd = new MySqlCommand(query, connection);
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

        
        public Contract.Models.BackPanel GetEarnings(string id)
        {
            open();
            Contract.Models.BackPanel backPanel = new Contract.Models.BackPanel();
            string query = $"SELECT * FROM `order` INNER JOIN post ON order.PostId = post.PostId WHERE post.PostAuthor = '{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<string> orders = new List<string>();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                if (dataReader["Status"].ToString() == "Delivered")
                {
                    backPanel.orders.Add(new Contract.Models.order() { postId = dataReader["PostId"].ToString(), buyerId = dataReader["BuyerId"].ToString() });
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
            string query = $"SELECT * FROM account WHERE Id='{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                id = dataReader["Id"].ToString();
                admin = (bool)dataReader["admin"];

            }
            dataReader.Close();
            return admin;
        }
        
        public List<Contract.Models.Report> getReports(string id)
        {
            List<Contract.Models.Report> reports = new List<Contract.Models.Report>();
            string query = $"SELECT * FROM report INNER JOIN account ON report.creator = account.Id WHERE status='open'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                reports.Add(new Contract.Models.Report()
                {
                    creatorId = new Contract.Models.Account() { Name = dataReader["Name"].ToString() },
                    reportComment = dataReader["comment"].ToString(),
                    reportId = dataReader["reportId"].ToString(),
                    ReportType = Int32.Parse(dataReader["type"].ToString()),
                    reportReason = Int32.Parse(dataReader["reason"].ToString())
                });
            }
            dataReader.Close();
            return reports;
        }

        public void banUser(string adminId, string userId)
        {
            open();
            string query = $"UPDATE account SET ban=1 WHERE Id='{userId}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        

        public string GetPostByReviewId(string Id)
        {
            open();
            string query = $"SELECT * FROM review WHERE Id='{Id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
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

        public Contract.Models.Review GetReportReview(string reportId)
        {
            open();
            Contract.Models.Review review = new Contract.Models.Review();
            string query = $"SELECT * FROM review  WHERE id='{reportId}' ;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                review = new Contract.Models.Review()
                {
                    Account = new Contract.Models.Account() { Id = dataReader["account"].ToString() },
                    postId = dataReader["post"].ToString(),
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
