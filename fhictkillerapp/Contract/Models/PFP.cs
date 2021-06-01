using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Models
{
    public class PFP
    {
        public IFormFile pfp { get; set; }
    }
}
