using System.Web.Mvc;
using SearchAvto.Models.DataModels;
using SearchAvto.Models.LogicModels;

namespace SearchAvto.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View(DataManager.Cars.Brands());
        }

        public ActionResult Brand(int id)
        {
            return View(DataManager.Cars.GetBrand(id));
        }

        public ActionResult ModelLine(int id)
        {
            return View(DataManager.Cars.GetModelLine(id));
        }

        public ActionResult Model(int id)
        {
            return View(DataManager.Cars.GetCarModel(id));
        }

        public ActionResult Modification(int id)
        {
            return View(DataManager.Cars.GetModification(id));
        }

        [Route("Test")]
        public ActionResult TestPage()
        {
            ViewBag.Length = Test.Length;
            return View();
        }

        public PartialViewResult Question(byte id)
        {
            ViewBag.Id = id + 1;
            ViewBag.IsLast = id + 1 == Test.Length;
            return Request.IsAjaxRequest() ? PartialView(Test.GetQuestion(id)) : PartialView(null);
        }

        [HttpPost]
        public ActionResult GetResult(int[] results)
        {
            return View(DataManager.Cars.FindCarModel(results));
        }
    }
}