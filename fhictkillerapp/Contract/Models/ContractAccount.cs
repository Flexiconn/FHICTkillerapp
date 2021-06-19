using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contract.Models
{
    public class ContractAccount
    {
        public string Id { get; set; }
        public string SessionId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Balance { get; set; }

        public void SetAccount(string newId, string newSessionId, string newName, string newPassword, string newBalance) {
            Id = newId;
            SessionId = newSessionId;
            Name = newName;
            Password = newPassword;
            Balance = newBalance;
        }


    }
}
