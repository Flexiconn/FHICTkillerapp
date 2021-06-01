using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Models;

namespace Logic.Models
{
    public class LogicPosts
    {
        public string PostId { get; set; }
        public string PostAuthor { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public List<string> images { get; set; }
        public virtual LogicAccount Account { get; set; }
        public List<LogicReview> reviews { get; set; }


        public LogicPosts() {
            images = new List<string>();
            reviews = new List<LogicReview>();
        }

        public LogicPosts(Contract.Models.Posts dto)
        {
            this.PostId = dto.PostId;
            this.PostName = dto.PostName;
            this.PostDescription = dto.PostDescription;
            this.PostAuthor = dto.PostAuthor;
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
    }
}
