using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fhictkillerapp.Models;

namespace fhictkillerapp.Controllers
{
    public class AccountController : Controller
    {
        Logic.AccountContainer Logic = new Logic.AccountContainer();
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
            Models.ViewmyAccountModel myAccountModel = new Models.ViewmyAccountModel(Logic.GetMyAccountInfo(HttpContext.Session.GetString("SessionId")));

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
        public IActionResult RegisterAccount(ViewAccount account)
        {
            Logic.RegisterAccount(account.Name, account.Password);
            return View("Index");
        }

        [HttpPost]
        public IActionResult LoginAccount(ViewAccount account)
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
        public IActionResult SetPFP(ViewPFP pfpModel)
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
