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
        Logic.Account Logic = new Logic.Account();
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
            Models.myAccountModel myAccountModel = new Models.myAccountModel(Logic.MyAccount(HttpContext.Session.GetString("SessionId")));

            ViewBag.pfp = myAccountModel.PFP;
            ViewBag.OrdersIncoming = myAccountModel.ordersIncoming;
            ViewBag.Orders = myAccountModel.ordersOutgoing;
            ViewBag.Profile = myAccountModel.account;
            return View();
        }

        public IActionResult AddFunds()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterAccount(Account account)
        {
            Logic.RegisterAccount(account.Name, account.Password);
            return View("Index");
        }

        [HttpPost]
        public IActionResult LoginAccount(Account account)
        {
            Console.WriteLine(Logic.LoginAccount(account.Name, account.Password));
            HttpContext.Session.SetString("SessionId", Logic.LoginAccount(account.Name, account.Password));
            return View("Index");
        }

        [HttpPost]
        public IActionResult AddfundsToAccount(int amount)
        {
            if (Logic.AddfundsToAccount(amount, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("MyAccount", "Account");

            }
            else {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult SetPFP(Common.Models.PFP pfpModel)
        {
            if (Logic.SetPFP(pfpModel.pfp, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("MyAccount", "Account");

            }
            else {
                return RedirectToAction("Login", "Account");

            }
        }
        [HttpPost]
        public ActionResult CancelOrder(string orderId)
        {
            if (Logic.cancelOrder(orderId, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("MyAccount", "Account");
            }
            else
            {
                return RedirectToAction("MyAccount", "Account");
            }
        }

        [HttpPost]
        public ActionResult AcceptOrder(string orderId)
        {
            if (Logic.AcceptOrder(orderId, HttpContext.Session.GetString("SessionId")))
            {
                return RedirectToAction("MyAccount", "Account");
            }
            else
            {
                return RedirectToAction("MyAccount", "Account");
            }
        }
    }
}
