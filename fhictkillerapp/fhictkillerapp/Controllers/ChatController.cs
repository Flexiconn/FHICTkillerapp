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

            ViewBag.chat = Querries.GetMessages(id);
            return View();
        }

        
        public IActionResult Chat() 
        {

            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(Chat chat)
        {
            Console.WriteLine(chat.Message); 
            Querries.SendMessage(chat, HttpContext.Session.GetString("SessionId"));
            return Redirect("Index");
        }


        [HttpPost]
        public ActionResult GetMessages(Chat chat)
        {
            Console.WriteLine(chat.Message);
            Console.WriteLine(Querries.GetMessages(null).Count);
            return Redirect("Index");
        }
    }
}
