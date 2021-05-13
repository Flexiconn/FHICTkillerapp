using Microsoft.AspNetCore.Mvc;
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
        Connection connection = new Connection();
        public ActionResult Index()
        {
            ViewBag.earnings = connection.GetEarnings(HttpContext.Session.GetString("SessionId"));
            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.reports = connection.getReports(HttpContext.Session.GetString("SessionId"));
            return View();
        }

        [HttpPost]
        public ActionResult BanUser(string userId)
        {
            connection.banUser(HttpContext.Session.GetString("SessionId"), userId);
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public ActionResult BanUserByPost(string postId)
        {
            
            connection.banUser(HttpContext.Session.GetString("SessionId"), connection.GetPost(postId).PostAuthor);
            return RedirectToAction("Admin");
        }

        public ActionResult ViewReportPost(string reportId)
        {

            ViewBag.Post = connection.GetPost(connection.GetPostByReviewId(reportId));
            ViewBag.Review = connection.GetReportReview(reportId);
            return View("ReviewReportView");
        }
    }
}
