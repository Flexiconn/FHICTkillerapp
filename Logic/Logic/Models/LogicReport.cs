using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class LogicReport
    {
        public string id { get; set; }
        public int ReportType { get; set; }
        public int reportReason { get; set; }
        public string reportComment { get; set; }
        public string reportId { get; set; }
        public LogicAccount creatorId { get; set; }
        public string status { get; set; }

        public LogicReport() { 
        
        }
        public LogicReport(Contract.Models.ContractReport dto)
        {
            this.creatorId = new LogicAccount(dto.creatorId.Balance, dto.creatorId.Balance, dto.creatorId.SessionId , dto.creatorId.Name, dto.creatorId.Password);
            this.id = dto.id;
            this.reportComment = dto.reportComment;
            this.reportId = dto.reportId;
            this.reportReason = dto.reportReason;
            this.ReportType = dto.ReportType;
            this.status = dto.status;
        }

        public LogicAccount GetAccount() {
            return creatorId;
        }

        public string GetId() {
            return id;
        }

        public int GetType()
        {
            return ReportType;
        }

        public int GetReason()
        {
            return reportReason;
        }

        public string GetComment()
        {
            return reportComment;
        }

        public string GetReportedId()
        {
            return reportId;
        }


        public string GetStatus()
        {
            return status;
        }
    }
}
