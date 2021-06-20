using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class LogicFavourite
    {
        public string Id { get; set; }
        public LogicAccount Account { get; set; }
        public LogicPosts Post { get; set; }

        public LogicFavourite(Contract.Models.ContractFavourite dto) {
            Id = dto.Id;
            Account = new LogicAccount(dto.Account);
            Post = new LogicPosts(dto.Post);
        }
    }
}
