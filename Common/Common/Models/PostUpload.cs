using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
    public class PostUpload
    {
        [Key]
        public string PostId { get; set; }
        public IFormFile MyImage { set; get; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        
    }
}
