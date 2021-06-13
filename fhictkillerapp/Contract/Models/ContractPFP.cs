using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Models
{
    public class ContractPFP
    {
        public IFormFile pfp { get; set; }
    }
}
