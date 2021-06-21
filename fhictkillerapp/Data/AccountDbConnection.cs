using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Data
{
    public class AccountDbConnection : Contract.IAccount
    {
        private MySqlConnection connection;

        public AccountDbConnection()
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
                thisAccount.SetAccount(dataReader["Id"].ToString(), dataReader["SessionId"].ToString(), dataReader["Name"].ToString(), dataReader["Password"].ToString(), dataReader["Balance"].ToString());
            }
            dataReader.Close();
            close();
            return thisAccount;
        }

        public string GetAccountName(string id)
        {
            open();
            string query = $"SELECT * FROM account WHERE Id=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            var name = dataReader["Name"].ToString();
            dataReader.Close();
            close();
            if (name == null)
            {
                return "Not Found";
            }
            return (name);
        }

 
        public void CreateAccount(string Password, string Name, string Id)
        {
            open();
            string query = $"INSERT INTO account (Id, Name, Password) VALUES(@Id, @Name, @Password);";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Password", Password);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

       
        public Contract.Models.ContractAccount LoginAccountCheck(string Password, string Name)
        {
            open();
            string query = $"SELECT * FROM account WHERE Password=@Password AND Name=@Name";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Password", Password);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Contract.Models.ContractAccount acc = new Contract.Models.ContractAccount();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                acc.Id = dataReader["Id"].ToString();
            }
            dataReader.Close();
            
            close();
            return acc;
        }

        public string SetSessionId(string Id, string newSessionId) {

            open();
            string query = $"UPDATE account SET SessionId=@SessionId WHERE Id=@Id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SessionId", newSessionId);
            cmd.Parameters.AddWithValue("@Id", Id);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
            return (newSessionId);
        }

        public bool CheckIfSignedIn(string Id)
        {
            open();
            string query = $"SELECT SessionId FROM account WHERE SessionId=@SessionId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SessionId", Id);
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

        public Contract.Models.ContractAccount GetProfileInfo(string Id)
        {
            open();
            string query = $"SELECT * FROM account WHERE Id=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            Contract.Models.ContractAccount acc = new Contract.Models.ContractAccount();
            while (dataReader.Read())
            {
                acc.Name = dataReader["Name"].ToString();
                acc.Balance = dataReader["Balance"].ToString();
            }
            dataReader.Close();
            close();
            return acc;
        }

        public void AddFunds(float amount, string id)
        {
            open();
            float funds;
            string query = $"SELECT * FROM account WHERE Id=@Id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Create a data reader and Execute the command
            dataReader.Read();


            funds = (float)dataReader["Balance"];

            dataReader.Close();
            funds = funds + amount;
            Console.WriteLine("funds: " + funds);
            query = $"UPDATE account SET Balance=@Funds WHERE Id=@Id;";
            cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Funds", funds);

            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }

        public void AddPFP(string Path, string ParentId, string Id)
        {
            open();
            string query = $"INSERT INTO pfp (Id, Path, Parent) VALUES(Id, @Path, @Parent); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Path", Path);
            cmd.Parameters.AddWithValue("@Parent", Id);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
        }



        public string GetPFP(string Id)
        {
            open();
            string path = "";
            string query = $"SELECT * FROM pfp WHERE Parent=@Parent";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Parent", Id);

            MySqlDataReader dataReader = cmd.ExecuteReader();
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                path = dataReader["Path"].ToString();
            }

            dataReader.Close();
            close();
            if (path == null)
                return "Not Found";
            return path;
        }

        
    }
}
