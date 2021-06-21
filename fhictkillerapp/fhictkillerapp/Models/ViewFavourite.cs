using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fhictkillerapp.Models
{
    public class ViewFavourite
    {
        public string Id { get; set; }
        public ViewAccount Account { get; set; }
        public ViewPosts Post { get; set; }

        public ViewFavourite(Logic.Models.LogicFavourite dto)
        {
            Id = dto.Id;
            Account = new ViewAccount(dto.Account);
            Post = new ViewPosts(dto.Post);
        }

        static public List<ViewFavourite> Favourites(List<Logic.Models.LogicFavourite> order)
        {
            List<ViewFavourite> orders = new List<ViewFavourite>();
            foreach (var t in order)
            {
                orders.Add(new ViewFavourite(t));
            }
            return orders;
        }
    }
}
