using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
    public class PostTags
    {
         [Key]
         public string Tag { get; set; }
    }
}
