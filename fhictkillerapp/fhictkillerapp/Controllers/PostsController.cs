﻿using Microsoft.AspNetCore.Http;
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
        Logic.PostContainer PostContainer = new Logic.PostContainer();
        Logic.OrderContainer OrderContainer = new Logic.OrderContainer();
        Logic.ReportContainer ReportContainer = new Logic.ReportContainer();

        [HttpPost]
        public ActionResult AddPost(ViewPostUpload postUpload)
        {

            if (PostContainer.AddPost(postUpload.MyImage, postUpload.PostName, postUpload.PostDescription, HttpContext.Session.GetString("SessionId")))
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
            foreach (var t in PostContainer.GetPostsList()) {
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
            Models.ViewPosts post = new Models.ViewPosts(PostContainer.ViewPost(Id));
            ViewBag.Post = post;
            ViewBag.Review = post.reviews;
            return View();
        }

        public ActionResult CreatePost()
        {
            if (PostContainer.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
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
            if (OrderContainer.OrderPost(orderMessage, postId, HttpContext.Session.GetString("SessionId")))
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
            if (PostContainer.createReview(review.text, review.score,review.postId , HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("ViewPost", new { id = review.postId });

            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult createReport(int reportReasonform, string comment, string PostId)
        {
            if (ReportContainer.createPostReport(reportReasonform, comment, PostId, HttpContext.Session.GetString("SessionId")))
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
            if (PostContainer.FavouriteToggle(PostId, HttpContext.Session.GetString("SessionId")) == true)
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
            if (ReportContainer.createReviewReport(reviewId, postId, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("ViewPost", new { id = postId });
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }


    }
}
