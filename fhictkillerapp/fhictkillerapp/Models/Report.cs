﻿using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class Report
    {
        public string id { get; set; }
        public int ReportType { get; set; }
        public int reportReason { get; set; }
        public string reportComment { get; set; }
        public string reportId { get; set; }
        public Account creatorId { get; set; }
        public string status { get; set; }

        public Report() { 
        
        }
        public Report(Logic.Models.LogicReport dto)
        {
            this.creatorId = new Account() { Balance = dto.creatorId.Balance, Id = dto.creatorId.Balance, Name = dto.creatorId.Name, Password = dto.creatorId.Password, SessionId = dto.creatorId.SessionId };
            this.id = dto.id;
            this.reportComment = dto.reportComment;
            this.reportId = dto.reportId;
            this.reportReason = dto.reportReason;
            this.ReportType = dto.ReportType;
            this.status = dto.status;
        }
    }
}
