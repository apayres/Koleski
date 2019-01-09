using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Koleksi.Domain;
using Koleksi.Web.Models.Home;
using Koleksi.Services.Components.Loaders;

namespace Koleksi.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            CollectionLoader component = new CollectionLoader();
            HomeModel model = new HomeModel();
            return View(model);
        }
    }
}