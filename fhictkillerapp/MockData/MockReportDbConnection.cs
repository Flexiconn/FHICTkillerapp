using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MockData
{
    public class MockReportDbConnection : Contract.IReport
    {
        private MySqlConnection connection;



        private void open()
        {
            connection.Open();
        }

        private void close()
        {
            connection.Close();
        }


        public void createReport(string newId, string id, Contract.reportTypes reportType, Contract.reportReasons reportReason, string comment, string reportedId)
        {
            open();

            string query = $"INSERT INTO report (id, type, reason, comment, reportId, creator) VALUES(@id, @type, @reason, @comment, @reportId, @creator);";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", newId);
            cmd.Parameters.AddWithValue("@type", reportType);
            cmd.Parameters.AddWithValue("@reason", reportReason);
            cmd.Parameters.AddWithValue("@comment", comment);
            cmd.Parameters.AddWithValue("@reportId", reportedId);
            cmd.Parameters.AddWithValue("@creator", id);
            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();
            close();
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

    }
}
