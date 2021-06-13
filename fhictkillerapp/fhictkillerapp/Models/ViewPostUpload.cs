using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace fhictkillerapp.Models
{
    public class ViewPostUpload
    {
        public string PostId { get; set; }
        public IFormFile MyImage { set; get; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }

        public ViewPostUpload() {
        
        }

        public ViewPostUpload(Logic.Models.LogicPostUpload dto)
        {
            this.PostId = dto.PostId;
            this.MyImage = dto.MyImage;
            this.PostDescription = dto.PostDescription;
            this.PostName = dto.PostName;
        }

    }
}
