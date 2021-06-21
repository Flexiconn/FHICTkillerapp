using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MockData
{
    public class MockPostDbConnection : Contract.IPost
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public MockPostDbConnection()
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
            if (SessionId == "test") {
                return "test";
            }
            return "empty";
        }

        public Contract.Models.ContractAccount GetAccount(string id)
        {
            return new Contract.Models.ContractAccount();
        }

        public Int64 PostAmount(string id)
        {
            if (id == "4") {
                return 4;
            }
            return 1;
        }

        public void AddPost(string postId, string postName, string postDescription, string Id)
        {
            
        }

        public void AddImageToDB(string postId, string path, string Id)
        {
            
        }


        public List<Contract.Models.ContractPosts> GetPosts()
        {
            
            List<Contract.Models.ContractPosts> postsList = new List<Contract.Models.ContractPosts>();
            postsList.Add(new Contract.Models.ContractPosts() { PostAuthor = new Contract.Models.ContractAccount()});
            return (postsList);
        }



        public string GetAccountName(string id)
        {
            
            return "test";
        }

        public Contract.Models.ContractPosts GetPost(string id)
        {
            if (id == "test") {
                return new Contract.Models.ContractPosts() { PostId = "test", PostAuthor = new Contract.Models.ContractAccount() };
            }
            return new Contract.Models.ContractPosts() { PostId = "empty", PostAuthor = new Contract.Models.ContractAccount() }; ;
        }

        public void AddOrder(string orderId, string id, string postId, string chatId)
        {
            
        }

        

        public bool CheckIfSignedIn(string Id)
        {
            if (Id == "test")
            {
                return true;
            }
        return false;
        }

       
        public Contract.Models.ContractAccount GetProfileInfo(string Id)
        {
            Contract.Models.ContractAccount acc = new Contract.Models.ContractAccount();
            return acc;
        }

       
        
 

        public void createReview(string id, string text, int score, string postId)
        {
           
        }

        public List<Contract.Models.ContractReview> GetReview(string postId)
        {
            return new List<Contract.Models.ContractReview>() { new Contract.Models.ContractReview() {Account = new Contract.Models.ContractAccount() } };
        }

        public void createReport(string id, Contract.reportTypes reportType, Contract.reportReasons reportReason, string comment, string reportedId)
        {
            
        }

        public void AddFavourite(string Id, string AccountId, string PostId)
        {
            
        }

        public List<Contract.Models.ContractFavourite> GetFavourites(string UserId)
        {
            List<Contract.Models.ContractFavourite> favourites = new List<Contract.Models.ContractFavourite>();
            if (UserId == "test") {
                favourites.Add(new Contract.Models.ContractFavourite() { Id = "Test", Account = new Contract.Models.ContractAccount(), Post = new Contract.Models.ContractPosts() });
            }
            return favourites;
        }

        public void RemoveFavourite(string Id)
        {

        }

    }
}
