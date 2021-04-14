using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Common;
using System.Net.Http.Headers;
using Common.Models;
using System.IO;

namespace fhictkillerapp.Controllers
{
    public class PostsController : Controller
    {


        Querries querries = new Querries();
        // GET: PostsController

        [HttpPost]
        public ActionResult AddPost(PostUpload postUpload)
        {

            postUpload.PostId = Guid.NewGuid().ToString();
            querries.AddPost(postUpload, HttpContext.Session.GetString("SessionId"));
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
            if (querries.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
            {
                return View();
            }

            return RedirectToAction("Login", "Account");

        }

        public ActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrderPost( string orderMessage, string postId)
        {
            if (querries.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
            {
                order order = new order();
                order.orderMessage = orderMessage;
                Console.WriteLine(orderMessage);
                order.post = querries.GetPost(postId);
                order.buyer = querries.GetAccount(HttpContext.Session.GetString("SessionId"));
                querries.AddOrder(order);
            }

            return RedirectToAction("Login", "Account");

        }

        
        public ActionResult OrderPost()
        {
            if (querries.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("Index", "Chat");
            }

            return RedirectToAction("Login", "Account");

        }

    }
}
