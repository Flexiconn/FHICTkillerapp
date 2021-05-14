using Common;
using Common.Models;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;
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
    public class Connection :  IAccount, IPost, IChat, IBackPanel
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
            database = "KillerApp";
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
            string id = GetAccount(sesId).Id;
            open();
            string pathString = System.IO.Path.Combine("wwwroot/data/IMG/post/", insertPost.PostId );
            System.IO.Directory.CreateDirectory(pathString);
            insertPost.MyImage.CopyTo(new FileStream(System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString()), FileMode.Create));





            string query = $"INSERT INTO post (PostId, PostName, PostDescription, PostAuthor) VALUES(@PostId, @PostName, @PostDescription, @PostAuthor); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PostId", insertPost.PostId);
            cmd.Parameters.AddWithValue("@PostName", insertPost.PostName);
            cmd.Parameters.AddWithValue("@PostDescription", insertPost.PostDescription);
            cmd.Parameters.AddWithValue("@PostAuthor", id);

            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();

            //posts.PostFileName = System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString());
            query = $"INSERT INTO images (Id, Path, Parent) VALUES('{Guid.NewGuid().ToString()}', @Path, '{insertPost.PostId}'); ";
            cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Path", System.IO.Path.Combine(System.IO.Path.Combine("/data/IMG/post/", insertPost.PostId + "/") , insertPost.MyImage.FileName.ToString()));

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

            query = $"SELECT * FROM images";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                foreach (var item in postsList) {
                    if (item.PostId == dataReader["Parent"].ToString()) {
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
            Posts post = new Posts();
            string query = $"SELECT * FROM post WHERE PostId='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Posts> postsList = new List<Posts>();
            //Read the data and store them in the list
            while (dataReader.Read())
            {

                post = new Posts() { PostAuthor = dataReader["PostAuthor"].ToString(), PostId = dataReader["PostId"].ToString(), PostName = dataReader["PostName"].ToString(), PostDescription = dataReader["PostDescription"].ToString() };
            }
            dataReader.Close();

            query = $"SELECT * FROM images WHERE Parent='{id}'";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read()) {
                post.images.Add(dataReader["Path"].ToString());
            }


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

        public void createReview(string id, Review review) {
            open();
            string query = $"SELECT * FROM account WHERE SessionId='{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                id = dataReader["Id"].ToString();
            }
            dataReader.Close();

            query = $"INSERT INTO review (id, score, text, account, post) VALUES('{Guid.NewGuid().ToString()}','{review.score}','{review.text}','{review.Account.Id}','{review.postId}');";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public List<Review> GetReview(string postId) {
            open();
            List<Review> reviews = new List<Review>();
            string query = $"SELECT * FROM review INNER JOIN account ON review.account = account.Id WHERE post='{postId}' ;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                reviews.Add(new Review()
                {
                    Account = new Account() { Name = dataReader["Name"].ToString() },
                    postId = dataReader["post"].ToString(),
                    //score = Int32.Parse(dataReader["Id"].ToString()),
                    text = dataReader["text"].ToString(),
                    reviewId = dataReader["id"].ToString()
                });
            }
            dataReader.Close();

            return reviews;
        }

        public void createReport(string id, Report report)
        {
            open();
            string query = $"SELECT * FROM account WHERE SessionId='{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                id = dataReader["Id"].ToString();
            }
            dataReader.Close();

            query = $"INSERT INTO report (id, type, reason, comment, reportId, creator) VALUES('{Guid.NewGuid().ToString()}','{(int)report.ReportType}','{(int)report.reportReason}','{report.reportComment}','{report.reportId}','{report.creatorId.Id}');";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public List<Report> getReports(string id)
        {
            List<Report> reports = new List<Report>();

            bool admin = false;
            open();
            string query = $"SELECT * FROM account WHERE SessionId='{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                id = dataReader["Id"].ToString();
                admin = (bool)dataReader["admin"];

            }

            dataReader.Close();
            if (admin == false)
            {
                return null;
            }

            query = $"SELECT * FROM report INNER JOIN account ON report.creator = account.Id WHERE status='open'";
            cmd = new MySqlCommand(query, connection);
            dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                reports.Add(new Report()
                {
                    creatorId = new Account() { Name = dataReader["Name"].ToString() },
                    reportComment = dataReader["comment"].ToString(),
                    reportId = dataReader["reportId"].ToString(),
                    ReportType = Int32.Parse( dataReader["type"].ToString()),
                    reportReason = Int32.Parse(dataReader["reason"].ToString())
                }) ;
            }
            dataReader.Close();
            return reports;
        }

        public void banUser(string adminId, string userId)
        {
            open();
            bool admin = false;
            string query = $"SELECT * FROM account WHERE SessionId='{adminId}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            dataReader.Read();

            admin = (bool)dataReader["admin"];

            dataReader.Close();
            if (admin == true)
            {
                query = $"UPDATE account SET ban=1 WHERE Id='{userId}';";
                cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
            }
            close();
        }

        public void AddPFP(PFP pfp, string Id)
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
            string pathString = System.IO.Path.Combine("wwwroot/data/IMG/pfp/", Id);
            System.IO.Directory.CreateDirectory(pathString);
            pfp.pfp.CopyTo(new FileStream(System.IO.Path.Combine(pathString, pfp.pfp.FileName.ToString()), FileMode.Create));




            query = $"INSERT INTO pfp (Id, Path, Parent) VALUES('{Guid.NewGuid().ToString()}', '{System.IO.Path.Combine(System.IO.Path.Combine("/data/IMG/pfp/", Id + "/"), pfp.pfp.FileName.ToString())}', '{Id}'); ";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public string GetPFP(string Id)
        {
            open();
            string path = "";
            string query = $"SELECT * FROM account WHERE SessionId='{Id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                Id = dataReader["Id"].ToString();
            }
            dataReader.Close();


            query = $"SELECT * FROM pfp WHERE Parent='{Id}'";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                path = dataReader["Path"].ToString();
            }

            dataReader.Close();
            close();
            return path;
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

        public Review GetReportReview(string reportId)
        {
            open();
            Review review = new Review();
            string query = $"SELECT * FROM review  WHERE id='{reportId}' ;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                review =new Review()
                {
                    Account = new Account() { Id = dataReader["account"].ToString() },
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
