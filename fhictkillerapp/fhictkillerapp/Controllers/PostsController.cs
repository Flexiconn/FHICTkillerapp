using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fhictkillerapp.Controllers
{
    public class PostsController : Controller
    {
        // GET: PostsController
        public ActionResult Index()
        {
            return View();
        }

        public EmptyResult Create(string name)
        {
            Console.WriteLine(name);
            return;
        }

    }
}
