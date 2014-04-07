using System;
using System.Configuration;
using System.Web.Mvc;
using SearchAvto.Models.LogicModels;

namespace SearchAvto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Tuple.Create(DataManager.News.All(4),DataManager.Cars.RandomCarModels(4)));
        }

    }
}