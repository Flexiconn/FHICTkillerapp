using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common;
using Common.Models;

namespace Logic
{
    public class Querries
    {
        CliverrContext db = new CliverrContext();
        public void DbTest() 
        {
            db.Database.EnsureCreated();
        }
        public void AddPost(PostUpload insertPost) 
        {
            string pathString = System.IO.Path.Combine("../Data/IMG/", insertPost.PostId);
            System.IO.Directory.CreateDirectory(pathString);
            insertPost.MyImage.CopyTo(new FileStream(System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString()), FileMode.Create));

            Posts posts = new Posts();
            posts.PostId = insertPost.PostId;
            posts.PostName = insertPost.PostName;
            posts.PostDescription = insertPost.PostDescription;
            posts.PostFileName = System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString());
            db.Posts.Add(posts);
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
