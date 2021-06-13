using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace fhictkillerapp.Models
{
    public class ViewPFP
    {
        public IFormFile pfp { get; set; }

        public ViewPFP() {

        }
        public ViewPFP(Logic.Models.LogicPFP dto)
        {
            this.pfp = dto.pfp;
        }
    }
}
