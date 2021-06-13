using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract.Models
{
    public class ContractPosts
    {
        public string PostId { get; set; }
        public string PostAuthor { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public List<string> images { get; set; }
        public virtual ContractAccount Account { get; set; }
        public List<ContractReview> reviews { get; set; }


        public ContractPosts() {
            images = new List<string>();
            reviews = new List<ContractReview>();
        }
    }
}
