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
        //[Route("Posts/ViewPost/{id:int}")]
        public ActionResult ViewPost(string id)
        {
            Console.WriteLine(id);
            return View(id);
        }

        public ActionResult CreatePost()
        {
            return View();
        }
    }
}
