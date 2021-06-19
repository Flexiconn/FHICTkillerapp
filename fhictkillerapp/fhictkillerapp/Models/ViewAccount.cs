using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace fhictkillerapp.Models
{
    public class ViewAccount
    {
        public string Id { get; set; }
        public string SessionId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Balance { get; set; }

        public ViewAccount()
        {

        }

        public ViewAccount(Logic.Models.LogicAccount dto)
        {
            this.Balance = dto.GetBalance();
            this.Id = dto.GetId;
            this.Name = dto.Name;
            this.Password = dto.Password;
            this.SessionId = dto.SessionId;
        }

    }
}
