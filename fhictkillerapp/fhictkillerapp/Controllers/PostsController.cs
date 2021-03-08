using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Common;

namespace fhictkillerapp.Controllers
{
    public class PostsController : Controller
    {
        Querries querries = new Querries();
        // GET: PostsController
        

        public ActionResult AddPost(Posts posts)
        {
            posts.PostId = Guid.NewGuid().ToString();
            querries.AddPost(posts);
            return Redirect("Index");
        }

        public ActionResult Index()
        {
            IList<Posts> postList = querries.GetPosts();
            Console.WriteLine(postList.Count());
            return View(postList);
        }

        [HttpGet]
        public ActionResult ViewPost(string Id)
        {
            ViewBag.Post = querries.GetPost(Id);
            Console.WriteLine(Id);
            return View();
        }

        public ActionResult CreatePost()
        {
            return View();
        }
    }
}
