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
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AddPost(Posts posts)
        {
            posts.PostId = Guid.NewGuid().ToString();
            querries.AddPost(posts);
            return Redirect("List");
        }

        public ActionResult List()
        {
            IList<Posts> postList = querries.GetPosts();
            Console.WriteLine(postList.Count());
            return View(postList);
        }

        //[HttpGet]
        //[Route("Home/Details/{id:int}")]
        public ActionResult ViewPost(int id)
        {
            Console.WriteLine(id);
            return View(id);
        }
    }
}
