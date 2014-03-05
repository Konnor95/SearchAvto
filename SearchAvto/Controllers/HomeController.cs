using System.Configuration;
using System.Web.Mvc;

namespace SearchAvto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Temp = ConfigurationManager.AppSettings["Temp"];
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}