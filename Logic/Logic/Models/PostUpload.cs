using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    public class PostUpload
    {
        public string PostId { get; set; }
        public IFormFile MyImage { set; get; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }


    }
}
