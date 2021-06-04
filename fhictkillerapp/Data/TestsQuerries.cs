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
using ThrowawayDb;

namespace Data
{
    public class TestQuerries
    {
        public bool testMode;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public TestQuerries()
        {
            Initialize();
        }
        public void start()
        {
            Initialize();
            //open();
            //close();
        }
        private void open()
        {
            connection.Open();
        }

        private void close()
        {
            connection.Close();
        }
        public void Initialize()
        {
            server = "studmysql01.fhict.local";
            database = "dbi456098";
            uid = "dbi456098";
            password = "cliver";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        public void cleanAccount(string id) {
            open();
            string query = $"DELETE FROM `account` WHERE SessionId='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            close();
        }

        public void cleanPFP(string id)
        {
            open();
            string query = $"DELETE pfp FROM pfp INNER JOIN account ON pfp.Parent = account.Id WHERE account.SessionId = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            close();
        }

        public void cleanPost(string id)
        {
            open();
            string query = $"DELETE post FROM post INNER JOIN account ON post.PostAuthor = account.Id WHERE account.SessionId = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            close();
        }

        public void cleanOrder(string id)
        {
            open();
            string query = $"DELETE order FROM order INNER JOIN account ON order.BuyerId = account.Id WHERE account.SessionId = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            close();
        }

        public string getPostFromSesId(string id)
        {
            open();
            string query = $"SELECT * FROM post INNER JOIN account ON post.PostAuthor = account.Id WHERE account.SessionId = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                query = dataReader["PostId"].ToString();
            }
            dataReader.Close();
            cmd.ExecuteNonQuery();
            close();
            return query;
        }

        public string getOrderFromSesId(string id)
        {
            open();
            string query = $"SELECT * FROM order INNER JOIN account ON order.BuyerId = account.Id WHERE account.SessionId = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                query = dataReader["OrderId"].ToString();
            }
            dataReader.Close();
            cmd.ExecuteNonQuery();
            close();
            return query;
        }

        public string getChatFromSesId(string id)
        {
            open();
            string query = $"SELECT * FROM order INNER JOIN account ON order.BuyerId = account.Id WHERE account.SessionId = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                query = dataReader["ChatId"].ToString();
            }
            dataReader.Close();
            cmd.ExecuteNonQuery();
            close();
            return query;
        }

        public void cleanReview(string id)
        {
            open();
            string query = $"DELETE review FROM review INNER JOIN account ON review.account = account.Id WHERE account.SessionId = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            close();
        }
        public void cleanReport(string id)
        {
            open();
            string query = $"DELETE report FROM report INNER JOIN account ON report.creator = account.Id WHERE account.SessionId = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            close();
        }
    }
}
