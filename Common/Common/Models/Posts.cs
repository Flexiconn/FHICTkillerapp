using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Posts
    {
        [Key]
        public string PostId { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public string PostFileName { get; set; }

    }
}
