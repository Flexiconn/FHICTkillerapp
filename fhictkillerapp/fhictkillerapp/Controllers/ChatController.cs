using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fhictkillerapp.Models;

namespace fhictkillerapp.Controllers
{
    public class ChatController : Controller
    {
        Logic.ChatContainer Logic = new Logic.ChatContainer();
        [Route("chat/{id}")]
        public IActionResult Index(string id)
        {
            ViewBag.chat = Logic.GetChat(id, HttpContext.Session.GetString("SessionId"));
            ViewBag.chatid = id;
            return View();
        }

        
        public IActionResult Chat() 
        {

            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(ViewClientChat chat, string ChatId)
        {
            if (Logic.SendMessage(chat.Message, ChatId, HttpContext.Session.GetString("SessionId")))
            {
                return LocalRedirect("/chat/" + ChatId);
            }
            else {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        public ActionResult createReport(int reportReasonform, string comment, string chatId)
        {
            if (Logic.createReport(reportReasonform, comment, chatId, HttpContext.Session.GetString("SessionId")))
            {

                return RedirectToAction("Index", new { id = chatId });

            }
            return RedirectToAction("Login", "Account");
        }
    }
}
