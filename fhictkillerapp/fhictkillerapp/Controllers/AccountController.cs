using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fhictkillerapp.Controllers
{
    public class AccountController : Controller
    {
        Logic.Querries Querries = new Logic.Querries();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterAccount(Account account)
        {
            Querries.CreateAccount(account);
            Console.WriteLine(account.Name);
            return View("Index");
        }

        [HttpPost]
        public IActionResult LoginAccount(Account account)
        {
            HttpContext.Session.SetString("SessionId", Querries.LoginAccount(account));
            Console.WriteLine(HttpContext.Session.GetString("SessionId"));
            return View("Index");
        }
    }
}
