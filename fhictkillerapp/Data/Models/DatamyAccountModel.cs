using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class DatamyAccountModel
    {
        public string PFP { get; set; }
        public List<DataOrder> ordersIncoming { get; set; }
        public List<DataOrder> ordersOutgoing { get; set; }
        public DataAccount account { get; set; }

        public DatamyAccountModel() {
            ordersIncoming = new List<DataOrder>();
            ordersOutgoing = new List<DataOrder>();
        }
    }
}
