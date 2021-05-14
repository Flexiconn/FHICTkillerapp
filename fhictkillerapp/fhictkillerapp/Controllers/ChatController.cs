﻿using Common;
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
        Logic.Chat Logic = new Logic.Chat();
        [Route("chat/{id}")]
        public IActionResult Index(string id)
        {
            ViewBag.chat = Logic.Index(id, HttpContext.Session.GetString("SessionId"));
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
            if (Logic.SendMessage(chat, HttpContext.Session.GetString("SessionId"), ChatId))
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
