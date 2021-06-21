using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IReport
    {
        void createReport(string newId, string id, Contract.reportTypes reportType, Contract.reportReasons reportReason, string comment, string reportedId);
        List<Contract.Models.ContractReport> getReports(string id);

    }
}
