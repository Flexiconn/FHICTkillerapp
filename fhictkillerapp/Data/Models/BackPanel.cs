using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class BackPanel
    {
        public List<order> orders { get; set; }
        public int earnings  { get; set; }
        public string userName { get; set; }

        public BackPanel() {
            orders = new List<order>();
        }
    }
}
