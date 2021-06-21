using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockData
{
    public class MockChatDbConnection : Contract.IChat
    {
        public bool testMode;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public MockChatDbConnection()
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
            return "test";
        }

        public Contract.Models.ContractAccount GetAccount(string id)
        {
            
            Contract.Models.ContractAccount thisAccount = new Contract.Models.ContractAccount();

            return thisAccount;
        }



        public bool CheckIfSignedIn(string Id)
        {
            if (Id == "test") {
                return true;
            }
            return false;
        }

        public void SendMessage(DateTime dateTime, string messageId, string Message, string Id, string chatid)
        {

        }

        public List<Contract.Models.ContractClientChat> GetMessages(string chatId, string id)
        {

            List<Contract.Models.ContractClientChat> msgs = new List<Contract.Models.ContractClientChat>();
            msgs.Add(new Contract.Models.ContractClientChat());

            return msgs.ToList();
        }


        public void createReport(string reportId, string id, Contract.reportTypes reportType, Contract.reportReasons reportReason, string comment, string reportedId)
        {
            open();


            string query = $"INSERT INTO report (id, type, reason, comment, reportId, creator) VALUES('{reportId}','{(int)reportType}','{(int)reportReason}','{comment}','{reportedId}','{id}');";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

    }
}
