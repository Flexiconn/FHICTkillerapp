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
        Data.Connection Querries = new Data.Connection();
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

        [Route("Myaccount")]
        public IActionResult MyAccount()
        {
            ViewBag.OrdersIncoming = Querries.GetOrdersIncoming(HttpContext.Session.GetString("SessionId"));
            ViewBag.Orders = Querries.GetOrders(HttpContext.Session.GetString("SessionId"));
            ViewBag.Profile = Querries.GetProfileInfo(HttpContext.Session.GetString("SessionId"));
            return View();
        }

        public IActionResult AddFunds()
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

        [HttpPost]
        public IActionResult AddfundsToAccount(int amount)
        {
            Console.WriteLine(amount);
            Querries.AddFunds(amount, HttpContext.Session.GetString("SessionId"));
            return RedirectToAction("MyAccount","Account");
        }
    }
}
