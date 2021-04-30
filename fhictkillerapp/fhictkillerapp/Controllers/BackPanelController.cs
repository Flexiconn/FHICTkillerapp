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
            Console.WriteLine(connection.GetEarnings(HttpContext.Session.GetString("SessionId")).earnings);
            ViewBag.earnings = connection.GetEarnings(HttpContext.Session.GetString("SessionId"));
            return View();
        }
    }
}
