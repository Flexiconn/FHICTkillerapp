using Common;
using Common.Models;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract;

namespace Data
{
    public class Connection : Contract.IAccount, Contract.IPost, Contract.IChat, Contract.IBackPanel
    {
        public bool testMode;
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

        private void CreateTestDB()
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

        public Contract.Models.Account GetAccount(string id)
        {
            open();
            string query = $"SELECT * FROM account WHERE SessionId='{id}'";
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

        public void AddPost(IFormFile myImage, string postName, string postDescription, string sesId)
        {
            string postId = Guid.NewGuid().ToString();
            string id = GetAccount(sesId).Id;
            open();
            string pathString = System.IO.Path.Combine("wwwroot/data/IMG/post/", postId);
            System.IO.Directory.CreateDirectory(pathString);
            myImage.CopyTo(new FileStream(System.IO.Path.Combine(pathString, myImage.FileName.ToString()), FileMode.Create));





            string query = $"INSERT INTO post (PostId, PostName, PostDescription, PostAuthor) VALUES(@PostId, @PostName, @PostDescription, @PostAuthor); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PostId", postId);
            cmd.Parameters.AddWithValue("@PostName", postName);
            cmd.Parameters.AddWithValue("@PostDescription", postDescription);
            cmd.Parameters.AddWithValue("@PostAuthor", id);

            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();

            //posts.PostFileName = System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString());
            query = $"INSERT INTO images (Id, Path, Parent) VALUES('{Guid.NewGuid().ToString()}', @Path, '{postId}'); ";
            cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Path", System.IO.Path.Combine(System.IO.Path.Combine("/data/IMG/post/", postId + "/") , myImage.FileName.ToString()));

            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }



        public List<Contract.Models.Posts> GetPosts()
        {
            open();
            string query = $"SELECT * FROM post";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Contract.Models.Posts> postsList = new List<Contract.Models.Posts>();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                postsList.Add(new Contract.Models.Posts() { PostAuthor = dataReader["PostAuthor"].ToString(), PostId = dataReader["PostId"].ToString(), PostName = dataReader["PostName"].ToString(), PostDescription = dataReader["PostDescription"].ToString() });
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

        public Contract.Models.Posts GetPost(string id)
        {

            open();
            Contract.Models.Posts post = new Contract.Models.Posts();
            string query = $"SELECT * FROM post WHERE PostId='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<Posts> postsList = new List<Posts>();
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
            while (dataReader.Read()) {
                post.images.Add(dataReader["Path"].ToString());
            }


            dataReader.Close();

            close();
            return (post);
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

        public void AddOrder(string id, string postId)
        {
            open();
            string query = $"INSERT INTO `order` (OrderId, BuyerId, PostId, ChatId, Status) VALUES ('{Guid.NewGuid().ToString()}', '{id}', '{postId}','{Guid.NewGuid().ToString()}','false');";
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

        public void SendMessage(string Message, string id, string chatid)
        {
            Console.WriteLine(chatid);
            open();
            DateTime  dateTime = DateTime.Now;
            string query = $"SELECT Id FROM account WHERE SessionId='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            dataReader.Read();
            var Idd = dataReader["Id"].ToString();
            dataReader.Close();
            
            query = $"INSERT INTO chat (MessageId, chatId, AccountId, Message, DateTime) VALUES('{Guid.NewGuid().ToString()}','{chatid}','{Idd}','{Message}','{dateTime}')";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public List<Contract.Models.ClientChat> GetMessages(string chatId, string id)
        {
            open();
            List<Contract.Models.ClientChat> msgs = new List<Contract.Models.ClientChat>();
            string query = $"SELECT * FROM `chat` INNER JOIN account ON chat.AccountId = account.Id WHERE chatId ='{chatId}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                msgs.Add(new Contract.Models.ClientChat() { Message = dataReader["Message"].ToString(), MessageId = dataReader["MessageId"].ToString(), AccountName = dataReader["Name"].ToString(), DateTime = DateTime.Parse(dataReader["DateTime"].ToString()) });
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

        public Contract.Models.Account GetProfileInfo(string Id) {
            open();
            
            string query = $"SELECT * FROM account WHERE SessionId='{Id}';";
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

        public List<Contract.Models.order> GetOrdersIncoming(string Id) {
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

        public Contract.Models.BackPanel GetEarnings(string id)
        {
            open();
            Contract.Models.BackPanel backPanel = new Contract.Models.BackPanel();
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
                    backPanel.orders.Add(new Contract.Models.order() { postId = dataReader["PostId"].ToString(), buyerId = dataReader["BuyerId"].ToString() });
                    backPanel.earnings = backPanel.earnings + (int)dataReader["PostPrice"];
                }
            }
            dataReader.Close();

            close();
            return backPanel;
        }

        public void createReview(string id, string text, int score, string postId) {
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

            query = $"INSERT INTO review (id, score, text, account, post) VALUES('{Guid.NewGuid().ToString()}','{score}','{text}','{id}','{postId}');";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public List<Contract.Models.Review> GetReview(string postId) {
            open();
            List<Contract.Models.Review> reviews = new List<Contract.Models.Review>();
            string query = $"SELECT * FROM review INNER JOIN account ON review.account = account.Id WHERE post='{postId}' ;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                reviews.Add(new Contract.Models.Review()
                {
                    Account = new Contract.Models.Account() { Name = dataReader["Name"].ToString() },
                    postId = dataReader["post"].ToString(),
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
            string query = $"SELECT * FROM account WHERE SessionId='{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                id = dataReader["Id"].ToString();
            }
            dataReader.Close();

            query = $"INSERT INTO report (id, type, reason, comment, reportId, creator) VALUES('{Guid.NewGuid().ToString()}','{(int)reportType}','{(int)reportReason}','{comment}','{reportedId}','{id}');";
            cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public List<Contract.Models.Report> getReports(string id)
        {
            List<Contract.Models.Report> reports = new List<Contract.Models.Report>();

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
                reports.Add(new Contract.Models.Report()
                {
                    creatorId = new Contract.Models.Account() { Name = dataReader["Name"].ToString() },
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

        public void AddPFP(IFormFile pfp, string Id)
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
            pfp.CopyTo(new FileStream(System.IO.Path.Combine(pathString, pfp.FileName.ToString()), FileMode.Create));




            query = $"INSERT INTO pfp (Id, Path, Parent) VALUES('{Guid.NewGuid().ToString()}', '{System.IO.Path.Combine(System.IO.Path.Combine("/data/IMG/pfp/", Id + "/"), pfp.FileName.ToString())}', '{Id}'); ";
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
