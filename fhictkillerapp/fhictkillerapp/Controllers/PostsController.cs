using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using System.Web;
using Logic;
using fhictkillerapp.Models;

namespace fhictkillerapp.Controllers
{
    public class PostsController : Controller
    {
        Logic.PostContainer Logic = new Logic.PostContainer();
        // GET: PostsController

        [HttpPost]
        public ActionResult AddPost(ViewPostUpload postUpload)
        {

            if (Logic.AddPost(postUpload.MyImage, postUpload.PostName, postUpload.PostDescription, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("Viewpost", new { id = postUpload.PostId });

            }
            else {
                return RedirectToAction("Login", "Account");
            }
        }



        public ActionResult Index()
        {
            Console.WriteLine("sessionId" + HttpContext.Session.GetString("SessionId"));
            List<fhictkillerapp.Models.ViewPosts> post = new List<fhictkillerapp.Models.ViewPosts>();
            foreach (var t in Logic.GetPostsList()) {
                post.Add(new Models.ViewPosts(t));
            }
            ViewBag.Posts = post;
            ViewBag.postheight = 300 * Math.Ceiling((decimal)post.Count/4);
            return View();
        }

        [HttpGet]
        [Route("post/{id}")]
        public ActionResult ViewPost(string Id)
        {
            Models.ViewPosts post = new Models.ViewPosts(Logic.ViewPost(Id));
            ViewBag.Post = post;
            ViewBag.Review = post.reviews;
            return View();
        }

        public ActionResult CreatePost()
        {
            if (Logic.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
            {
                return View();
            }

            return RedirectToAction("Login", "Account");

        }

        public ActionResult Order(string id)
        {
            ViewBag.order = id;
            return View();
        }

        [HttpPost]
        public ActionResult OrderPost( string orderMessage, string postId)
        {
            if (Logic.OrderPost(orderMessage, postId, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("Myaccount", "Account");

            }
            else {
                return RedirectToAction("Login", "Account");

            }
        }

        [HttpPost]

        public ActionResult createReview(ViewReview review) 
        {
            if (Logic.createReview(review.text, review.score,review.postId , HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("ViewPost", new { id = review.postId });

            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult createReport(int reportReasonform, string comment, string PostId)
        {
            if (Logic.createReport(reportReasonform, comment, PostId, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("ViewPost", new { id = PostId });

            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult ToggleFavourite(string PostId)
        {
            Console.WriteLine("favourite");
            if (Logic.FavouriteToggle(PostId, HttpContext.Session.GetString("SessionId")) == true)
            {
                return RedirectToAction("ViewPost", new { id = PostId });
            }
            else {
                return RedirectToAction("Login", "Account");

            }
        }

        [HttpPost]
        public ActionResult createReviewReport(string reviewId, string postId)
        {
            if (Logic.createReviewReport(reviewId, postId, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("ViewPost", new { id = postId });
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }


    }
}
