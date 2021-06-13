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
        Logic.BackPanelLogic Logic = new Logic.BackPanelLogic();

        public ActionResult Index()
        {
            ViewBag.earnings = Logic.GetBackPanelInfo(HttpContext.Session.GetString("SessionId"));
            return View();
        }

        public ActionResult Admin()
        {
            List<fhictkillerapp.Models.ViewReport> reports = new List<fhictkillerapp.Models.ViewReport>();
            foreach (var t in Logic.GetAdminPanelInfo(HttpContext.Session.GetString("SessionId"))) {
                reports.Add(new fhictkillerapp.Models.ViewReport(t));
            }
            ViewBag.reports = reports;
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
            return View("ReportView");
        }

        
    }
}
