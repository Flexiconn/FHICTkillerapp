using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Models
{
    public class ContractBackPanel
    {
        public List<Contractorder> orders { get; set; }
        public int earnings  { get; set; }
        public string userName { get; set; }

        public ContractBackPanel() {
            orders = new List<Contractorder>();
        }
    }
}
