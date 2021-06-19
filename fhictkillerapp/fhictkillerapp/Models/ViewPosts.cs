using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fhictkillerapp;
namespace fhictkillerapp.Models
{
    public class ViewPosts
    {
        public string PostId { get; set; }
        public ViewAccount PostAuthor { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public List<string> images { get; set; }
        public List<ViewReview> reviews { get; set; }


        public ViewPosts() {
            images = new List<string>();
            reviews = new List<ViewReview>();
        }

        public ViewPosts(Logic.Models.LogicPosts dto)
        {
            this.PostId = dto.PostId;
            this.PostName = dto.PostName;
            this.PostDescription = dto.PostDescription;
            this.PostAuthor = new ViewAccount(dto.PostAuthor);
            this.images = new List<string>();
            this.reviews = new List<ViewReview>();
            //this.Account = new Account(dto.Account);
            foreach (var t in dto.reviews) {
                this.reviews.Add(new ViewReview(t));
            }

            foreach (var t in dto.images)
            {
                Console.WriteLine("image " + t);

                this.images.Add(t);
            }
        }
    }
}
