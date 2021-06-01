using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Models;

namespace fhictkillerapp.Models
{
    public class Posts
    {
        public string PostId { get; set; }
        public string PostAuthor { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public List<string> images { get; set; }
        public virtual Account Account { get; set; }
        public List<Review> reviews { get; set; }


        public Posts() {
            images = new List<string>();
            reviews = new List<Review>();
        }

        public Posts(Logic.Models.LogicPosts dto)
        {
            this.PostId = dto.PostId;
            this.PostName = dto.PostName;
            this.PostDescription = dto.PostDescription;
            this.PostAuthor = dto.PostAuthor;
            this.images = new List<string>();
            this.reviews = new List<Review>();
            //this.Account = new Account(dto.Account);
            foreach (var t in dto.reviews) {
                this.reviews.Add(new Review(t));
            }

            foreach (var t in dto.images)
            {
                Console.WriteLine("image " + t);

                this.images.Add(t);
            }
        }
    }
}
