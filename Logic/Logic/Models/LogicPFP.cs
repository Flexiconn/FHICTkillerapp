using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class LogicPFP
    {
        public IFormFile pfp { get; set; }

        public LogicPFP() {

        }
        public LogicPFP(Contract.Models.PFP dto)
        {
            this.pfp = dto.pfp;
        }
    }
}
