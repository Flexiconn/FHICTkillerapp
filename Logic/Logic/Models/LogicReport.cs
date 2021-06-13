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
            this.creatorId = new LogicAccount() { Balance = dto.creatorId.Balance, Id = dto.creatorId.Balance, Name = dto.creatorId.Name, Password = dto.creatorId.Password, SessionId = dto.creatorId.SessionId };
            this.id = dto.id;
            this.reportComment = dto.reportComment;
            this.reportId = dto.reportId;
            this.reportReason = dto.reportReason;
            this.ReportType = dto.ReportType;
            this.status = dto.status;
        }
    }
}
