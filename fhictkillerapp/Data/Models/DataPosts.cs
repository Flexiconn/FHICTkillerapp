using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class DataPosts
    {
        public string PostId { get; set; }
        public string PostAuthor { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public List<string> images { get; set; }
        public virtual DataAccount Account { get; set; }
        public List<DataReview> reviews { get; set; }


        public DataPosts() {
            images = new List<string>();
            reviews = new List<DataReview>();
        }
    }
}
