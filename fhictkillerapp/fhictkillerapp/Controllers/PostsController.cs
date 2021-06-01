using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using System.Net.Http.Headers;
using Common.Models;
using System.IO;
using System.Web;
using Logic;

namespace fhictkillerapp.Controllers
{
    public class PostsController : Controller
    {
        Logic.Post Logic = new Logic.Post();
        Data.Connection Querries = new Data.Connection();
        // GET: PostsController

        [HttpPost]
        public ActionResult AddPost(PostUpload postUpload)
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
            List<fhictkillerapp.Models.Posts> post = new List<fhictkillerapp.Models.Posts>();
            foreach (var t in Logic.Index()) {
                post.Add(new Models.Posts(t));
            }
            ViewBag.Posts = post;
            return View();
        }

        [HttpGet]
        [Route("post/{id}")]
        public ActionResult ViewPost(string Id)
        {
            Models.Posts post = new Models.Posts(Logic.ViewPost(Id));
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

        public ActionResult createReview(Review review) 
        {
            if (Logic.createReview(review.text, review.score,review.postId, HttpContext.Session.GetString("SessionId")))
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
