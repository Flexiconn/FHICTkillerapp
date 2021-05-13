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

namespace fhictkillerapp.Controllers
{
    public class PostsController : Controller
    {

        Data.Connection Querries = new Data.Connection();
        // GET: PostsController

        [HttpPost]
        public ActionResult AddPost(PostUpload postUpload)
        {

            postUpload.PostId = Guid.NewGuid().ToString();
            Querries.AddPost(postUpload, HttpContext.Session.GetString("SessionId"));
            return RedirectToAction("Viewpost" , new { id = postUpload.PostId });
        }



        public ActionResult Index()
        {
            ViewBag.Posts = Querries.GetPosts();
            return View();
        }

        [HttpGet]
        [Route("post/{id}")]
        public ActionResult ViewPost(string Id)
        {
            ViewBag.Post = Querries.GetPost(Id);
            ViewBag.Review = Querries.GetReview(Id);
            Console.WriteLine(Id);
            return View();
        }

        public ActionResult CreatePost()
        {
            if (Querries.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
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
            if (Querries.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
            {
                order order = new order();
                order.post.PostId = postId;
                order.buyer.Id = Querries.GetAccount(HttpContext.Session.GetString("SessionId")).Id;
                Querries.AddOrder(order);
            }

            return RedirectToAction("Login", "Account");

        }

        [HttpPost]

        public ActionResult createReview(Review review) 
        {
            if (Querries.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
            {
                review.Account = Querries.GetAccount(HttpContext.Session.GetString("SessionId"));
                Querries.createReview(HttpContext.Session.GetString("SessionId"), review);
            }
            return RedirectToAction("ViewPost", new { id = review.postId });
        }

        [HttpPost]
        public ActionResult createReport(int reportReasonform, string comment, string PostId)
        {
            if (Querries.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
            {
                
                Report report = new Report() { reportReason = reportReasonform, ReportType = (int)reportTypes.post, reportComment = comment, reportId = PostId };
                report.creatorId = Querries.GetAccount(HttpContext.Session.GetString("SessionId"));
                Querries.createReport(HttpContext.Session.GetString("SessionId"), report);
            }
            return RedirectToAction("ViewPost", new { id = PostId });
        }
        [HttpPost]
        public ActionResult createReviewReport(string reviewId, string postId)
        {
            if (Querries.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
            {

                Report report = new Report() { ReportType = (int)reportTypes.review,  reportId = reviewId };
                report.creatorId = Querries.GetAccount(HttpContext.Session.GetString("SessionId"));
                Querries.createReport(HttpContext.Session.GetString("SessionId"), report);
            }
            return RedirectToAction("ViewPost", new { id = postId });
        }

    }
}
