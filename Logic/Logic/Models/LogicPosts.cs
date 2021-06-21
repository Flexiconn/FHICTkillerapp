using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logic.Models
{
    public class LogicPosts
    {
        public string PostId { get; set; }
        public LogicAccount PostAuthor { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public List<string> images { get; set; }
        public List<LogicReview> reviews { get; set; }


        public LogicPosts() {
            images = new List<string>();
            reviews = new List<LogicReview>();
        }

        public LogicPosts(Contract.Models.ContractPosts dto)
        {
            this.PostId = dto.PostId;
            this.PostName = dto.PostName;
            this.PostDescription = dto.PostDescription;
            if (dto.PostAuthor != null)
            {
                this.PostAuthor = new LogicAccount(dto.PostAuthor);
            }
            this.images = new List<string>();
            this.reviews = new List<LogicReview>();
            //this.Account = new LogicAccount(dto.Account);
            foreach (var t in dto.reviews) {
                this.reviews.Add(new LogicReview(t));
            }

            foreach (var t in dto.images)
            {
                this.images.Add(t);
            }
        }

        public string GetPostId() {
            return PostId;
        }

        public LogicAccount GetAuthor() {
            return PostAuthor;
        }

        public string GetName()
        {
            return PostName;
        }

        public string GetDescription()
        {
            return PostDescription;
        }

        public List<string> GetImages() {
            return images;
        }

        public List<LogicReview> GetReviews() { 
        return reviews;
        }
    }
}
