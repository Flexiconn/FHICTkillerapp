using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Models
{
    public class ContractFavourite
    {
        public string Id { get; set; }
        public ContractAccount Account { get; set; }
        public ContractPosts Post { get; set; }
    }
}
