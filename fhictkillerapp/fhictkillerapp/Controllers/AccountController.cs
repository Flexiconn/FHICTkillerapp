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
        Logic.AccountContainer AccountContainer = new Logic.AccountContainer();
        Logic.OrderContainer OrderContainer = new Logic.OrderContainer();

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SessionId") != null) {
                return RedirectToAction("MyAccount");
            }
            return RedirectToAction("Login");
        }

        public IActionResult Login(bool test)
        {
            if (false) {
                ViewBag.Error = "Wrong Password or Username";
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [Route("Myaccount")]
        public IActionResult MyAccount()
        {
            if (HttpContext.Session.GetString("SessionId") != null)
            {
                Models.ViewmyAccountModel AccountInfo = new Models.ViewmyAccountModel(AccountContainer.GetMyAccountInfo(HttpContext.Session.GetString("SessionId")));
                Models.ViewmyAccountModel Orders = new Models.ViewmyAccountModel();
                ViewBag.pfp = AccountInfo.PFP;
                ViewBag.OrdersIncoming = Orders.ordersIncoming;
                ViewBag.Orders = Orders.ordersOutgoing;
                ViewBag.Profile = AccountInfo.account;
                return View();
            }
            return RedirectToAction("Login");
        }

        public IActionResult AddFunds()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterAccount(ViewAccount account)
        {
            AccountContainer.RegisterAccount(account.Name, account.Password);
            return RedirectToAction("Index", "Posts");
        }

        [HttpPost]
        public IActionResult LoginAccount(ViewAccount account)
        {
            Console.WriteLine(AccountContainer.LoginAccount(account.Name, account.Password));
            HttpContext.Session.SetString("SessionId", AccountContainer.LoginAccount(account.Name, account.Password));
            return RedirectToAction("Index","Posts", false);
        }

        [HttpPost]
        public IActionResult AddfundsToAccount(int amount)
        {
            if (AccountContainer.AddfundsToAccount(amount, HttpContext.Session.GetString("SessionId")))
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
            if (AccountContainer.SetPFP(pfpModel.pfp, HttpContext.Session.GetString("SessionId")))
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
            if (OrderContainer.cancelOrder(orderId, HttpContext.Session.GetString("SessionId")))
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
            if (OrderContainer.AcceptOrder(orderId, HttpContext.Session.GetString("SessionId")))
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
