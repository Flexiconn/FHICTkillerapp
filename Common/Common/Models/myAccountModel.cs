using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class myAccountModel
    {
        public PFP PFP { get; set; }
        public order ordersIncoming { get; set; }
        public order ordersOutgoing { get; set; }
        public Account account { get; set; }
    }
}
