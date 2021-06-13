using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Logic.Models
{
    public class LogicAccount
    {
        public string Id { get; set; }
        public string SessionId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Balance { get; set; }

        public LogicAccount()
        {

        }

        public LogicAccount(Contract.Models.ContractAccount dto)
        {
            this.Balance = dto.Balance;
            this.Id = dto.Id;
            this.Name = dto.Name;
            this.Password = dto.Password;
            this.SessionId = dto.SessionId;
        }

    }
}
