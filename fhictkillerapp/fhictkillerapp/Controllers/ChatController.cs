using Common;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fhictkillerapp.Controllers
{
    public class ChatController : Controller
    {
        Data.Connection Querries = new Data.Connection();
        [Route("chat/{id}")]
        public IActionResult Index(string id)
        {
            Console.WriteLine(id);

            ViewBag.chat = Querries.GetMessages(id, HttpContext.Session.GetString("SessionId"));
            ViewBag.chatid = id;
            return View();
        }

        
        public IActionResult Chat() 
        {

            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(Chat chat, string ChatId)
        {
            Console.WriteLine(chat.Message); 
            Querries.SendMessage(chat, HttpContext.Session.GetString("SessionId"), ChatId);
            return LocalRedirect("/chat/" + ChatId);
        }


        [HttpPost]
        public ActionResult GetMessages(Chat chat)
        {
            Console.WriteLine(chat.Message);
            Console.WriteLine(Querries.GetMessages(null, null).Count);
            return new EmptyResult();
        }


        [HttpPost]
        public ActionResult createReport(int reportReasonform, string comment, string chatId)
        {
            if (Querries.CheckIfSignedIn(HttpContext.Session.GetString("SessionId")))
            {

                Report report = new Report() { reportReason = reportReasonform, ReportType = (int)reportTypes.chatHelp, reportComment = comment, reportId = chatId };
                report.creatorId = Querries.GetAccount(HttpContext.Session.GetString("SessionId"));
                Querries.createReport(HttpContext.Session.GetString("SessionId"), report);
            }
            return RedirectToAction("Index", new { id = chatId });
        }
    }
}
