using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Koleksi.Domain;
using Koleksi.Web.Models.Home;
using Koleksi.Services.Components;

namespace Koleksi.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            CollectionComponent component = new CollectionComponent();
            HomeModel model = new HomeModel();
            model.MainCollections = component.LoadCollections();
            return View(model);
        }
    }
}