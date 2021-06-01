using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Models
{
    public class myAccountModel
    {
        public string PFP { get; set; }
        public List<order> ordersIncoming { get; set; }
        public List<order> ordersOutgoing { get; set; }
        public Account account { get; set; }

        public myAccountModel() {
            ordersIncoming = new List<order>();
            ordersOutgoing = new List<order>();
        }
    }
}
