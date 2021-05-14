﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;

namespace fhictkillerapp.Controllers
{
    public class backPanelController : Controller
    {
        Logic.BackPanel Logic = new Logic.BackPanel();

        public ActionResult Index()
        {
            ViewBag.earnings = Logic.Index(HttpContext.Session.GetString("SessionId"));
            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.reports = Logic.Admin(HttpContext.Session.GetString("SessionId"));
            return View();
        }

        [HttpPost]
        public ActionResult BanUser(string userId)
        {
            if (Logic.BanUser(HttpContext.Session.GetString("SessionId"), userId))
            {
                return RedirectToAction("Admin");

            }
            else {
                return RedirectToAction("Login", "Account");

            }
        }

        [HttpPost]
        public ActionResult BanUserByPost(string postId)
        {
            if (Logic.BanUserByPost(postId, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("Admin");

            }
            else {
                return RedirectToAction("Login", "Account");

            }
        }

        public ActionResult ViewReportPost(string reportId)
        {
            
            ViewBag.Post = Logic.ViewReportPost(reportId,  HttpContext.Session.GetString("SessionId"));
            ViewBag.Review = Logic.ViewReportPost(reportId, HttpContext.Session.GetString("SessionId")).reviews;
            return View("ReviewReportView");
        }
    }
}
