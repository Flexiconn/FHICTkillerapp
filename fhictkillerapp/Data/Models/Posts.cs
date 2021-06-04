using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Posts
    {
        public string PostId { get; set; }
        public string PostAuthor { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public List<string> images { get; set; }
        public virtual Account Account { get; set; }
        public List<Review> reviews { get; set; }


        public Posts() {
            images = new List<string>();
            reviews = new List<Review>();
        }
    }
}
