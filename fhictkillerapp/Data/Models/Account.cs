using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string SessionId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Balance { get; set; }

    }
}
