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

        public LogicAccount(string balance, string id, string sessionId, string name, string password)
        {
            this.Balance = balance;
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.SessionId = sessionId;
        }

        public LogicAccount(Contract.Models.ContractAccount dto)
        {
            this.Balance = dto.Balance;
            this.Id = dto.Id;
            this.Name = dto.Name;
            this.Password = dto.Password;
            this.SessionId = dto.SessionId;
        }



        public string GetId() {
            return this.Id;
        }

        public string GetSessionId()
        {
            return this.SessionId;
        }

        public string GetBalance()
        {
            return this.Balance;
        }
        public string GetName()
        {
            return this.Name;
        }
    }
}
