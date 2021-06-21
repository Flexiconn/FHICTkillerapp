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
           
        }

        public List<Contract.Models.ContractReport> getReports(string id)
        {
           
            List<Contract.Models.ContractReport> reports = new List<Contract.Models.ContractReport>();
            
                reports.Add(new Contract.Models.ContractReport()
                {
                    creatorId = new Contract.Models.ContractAccount() { Id = "Test" },
                    reportComment = "Test",
                    reportId = "Test",
                    ReportType = 1,
                    reportReason = 1
                });
            return reports;
        }

    }
}
