using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Models
{
    public class ContractmyAccountModel
    {
        public string PFP { get; set; }
        public List<Contractorder> ordersIncoming { get; set; }
        public List<Contractorder> ordersOutgoing { get; set; }
        public ContractAccount account { get; set; }

        public ContractmyAccountModel() {
            ordersIncoming = new List<Contractorder>();
            ordersOutgoing = new List<Contractorder>();
        }
    }
}
