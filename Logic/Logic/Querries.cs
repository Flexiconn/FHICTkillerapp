using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Logic
{
    public class Querries
    {
        CliverrContext db = new CliverrContext();
        public void DbTest() 
        {
            db.Database.EnsureCreated();
        }
        public void AddPost(Posts insertPost) 
        {

            db.Posts.Add(insertPost);
            db.SaveChanges();
        }

        public List<Posts> GetPosts()
        {
            List<Posts> postsList = new List<Posts>();
            var postList = db.Posts.Select(x => x).ToList();
            postsList.AddRange(db.Posts.Select(x => x).ToList());
            return (postsList);
        }

        public Posts GetPost(string id)
        {
            Posts post = db.Posts.Where(b => b.PostId == id).FirstOrDefault();
            return (post);
        }
    }
}
