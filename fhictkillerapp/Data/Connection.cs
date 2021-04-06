using Common.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Connection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public void start() 
        {
            Initialize();
            open();
            querry();
            GetAccount("test");
            close();
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

        private void querry()
        {
            string query = "SELECT * FROM account";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                Console.WriteLine("Id: " + dataReader["Id"]);
            }
            dataReader.Close();

        }

        public Account GetAccount(string id)
        {
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

            return thisAccount;
        }

    }
}
