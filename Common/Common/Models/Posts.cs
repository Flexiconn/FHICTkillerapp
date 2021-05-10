using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Models;

namespace Common
{
    public class Posts
    {
        [Key]
        public string PostId { get; set; }
        public string PostAuthor { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public List<string> images { get; set; }
        public virtual PostTags Tags { get; set; }
        public virtual Account Account { get; set; }

        public Posts() {
            images = new List<string>();
        }
    }
}
