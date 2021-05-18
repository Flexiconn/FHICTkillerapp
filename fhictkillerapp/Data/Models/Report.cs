using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
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
    }
}
