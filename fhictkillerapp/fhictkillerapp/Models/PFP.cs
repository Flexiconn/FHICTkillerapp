using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class PFP
    {
        public IFormFile pfp { get; set; }

        public PFP() {

        }
        public PFP(Logic.Models.LogicPFP dto)
        {
            this.pfp = dto.pfp;
        }
    }
}
