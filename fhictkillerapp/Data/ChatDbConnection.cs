using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class ChatDbConnection : Contract.IChat
    {
        public bool testMode;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public ChatDbConnection()
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

        public Contract.Models.ContractAccount GetAccount(string id)
        {
            open();
            string query = $"SELECT * FROM account WHERE Id=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
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

        public void SendMessage(string Message, string Id, string chatid)
        {
            Console.WriteLine(chatid);
            open();
            DateTime dateTime = DateTime.Now;


            string query = $"INSERT INTO chat (MessageId, chatId, AccountId, Message, DateTime) VALUES(@MessageId, @chatId, @AccountId, @Message, @DateTime)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@MessageId", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("@chatId", chatid);
            cmd.Parameters.AddWithValue("@AccountId", Id);
            cmd.Parameters.AddWithValue("@Message", Message);
            cmd.Parameters.AddWithValue("@DateTime", dateTime);

            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public List<Contract.Models.ContractClientChat> GetMessages(string chatId, string id)
        {
            open();
            List<Contract.Models.ContractClientChat> msgs = new List<Contract.Models.ContractClientChat>();
            string query = $"SELECT * FROM `chat` INNER JOIN account ON chat.AccountId = account.Id WHERE chatId=@chatId;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@chatId", chatId);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Create a data reader and Execute the command
            while (dataReader.Read())
            {
                msgs.Add(new Contract.Models.ContractClientChat() { Message = dataReader["Message"].ToString(), MessageId = dataReader["MessageId"].ToString(), AccountName = dataReader["Name"].ToString(), DateTime = DateTime.Parse(dataReader["DateTime"].ToString()),});
            }
            dataReader.Close();
            close();

            return msgs.ToList();
        }


        public void createReport(string id, Contract.reportTypes reportType, Contract.reportReasons reportReason, string comment, string reportedId)
        {
            open();


            string query = $"INSERT INTO report (id, type, reason, comment, reportId, creator) VALUES(@id, @type, @reason, @comment, @reportId, @creator);";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("@type", (int)reportType);
            cmd.Parameters.AddWithValue("@reason", (int)reportReason);
            cmd.Parameters.AddWithValue("@comment", comment);
            cmd.Parameters.AddWithValue("@reportId", reportedId);
            cmd.Parameters.AddWithValue("@creator", id);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

    }
}
