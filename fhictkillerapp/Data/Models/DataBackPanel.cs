using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class DataBackPanel
    {
        public List<DataOrder> orders { get; set; }
        public int earnings  { get; set; }
        public string userName { get; set; }

        public DataBackPanel() {
            orders = new List<DataOrder>();
        }
    }
}
