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
        public string PostAuthor { get; set; }
        public string PostId { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public string PostFileName { get; set; }
        public virtual PostTags Tags { get; set; }
        public virtual Account Account { get; set; }
    }
}
