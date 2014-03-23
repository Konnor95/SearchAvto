using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using SearchAvto.Models.DataModels;
using SearchAvto.Models.LogicModels;
using SearchAvto.Models.ViewModels;

namespace SearchAvto.Controllers
{
    public class DashboardController : Controller
    {
        private User DefineUser()
        {
            return DataManager.Users.Define(HttpContext);
        }

        private bool HasNoAccess(User user)
        {
            return user == null || !user.HasModeratorAccess;
        }

        public ActionResult Index()
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ViewBag.User = user;
            return View();
        }

        public ActionResult Users()
        {
            var user = DefineUser();
            if (!user.HasAdminAccess) return NoPermission();
            ViewBag.User = user;
            return View(DataManager.Users.GetAll());
        }

        public ActionResult NoPermission()
        {
            return View();
        }

        #region Brand

        public ActionResult Brands(int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.Brands());
        }

        public ActionResult Brand(int? result, int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetBrand(id));
        }
        public ActionResult AddBrand(int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View();
        }
        public ActionResult EditBrand(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetBrand(id));
        }

        public ActionResult DeleteBrand(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetBrand(id));
        }
        [HttpPost]
        public ActionResult ManageBrandAdding(string name, HttpPostedFileBase imageUpload)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.AddBrand(name, imageUpload, Server);
            if (result.Succeeded && result.AffectedObjectId.HasValue)
            {
                return RedirectToAction("Brand", new { id = result.AffectedObjectId.Value, result = result.Id });
            }
            return RedirectToAction("AddBrand", new { result = result.Id });
        }
        [HttpPost]
        public ActionResult ManageBrandEditing(int id, string name, HttpPostedFileBase imageUpload, bool deleteImage = false)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.EditBrand(id, name, imageUpload, deleteImage, Server);
            if (result.Succeeded)
            {
                return RedirectToAction("Brand", new { id, result = result.Id });
            }
            return RedirectToAction("EditBrand", new { id, result = result.Id });
        }

        [HttpPost]
        public ActionResult ManageBrandDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.DeleteBrand(id, Server);
            if (result.Succeeded)
            {
                return RedirectToAction("Brands", new { result = result.Id });
            }
            return RedirectToAction("DeleteBrand", new { id, result = result.Id });
        }
        #endregion

        #region ModelLine

        public ActionResult ModelLines()
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            return View(DataManager.Cars.ModelLines());
        }

        public ActionResult ModelLine(int? result, int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetModelLine(id));
        }

        public ActionResult AddModelLine(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetBrand(id));
        }

        [HttpPost]
        public ActionResult ManageModelLineAdding(int id, string name)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.AddModelLine(id, name);
            if (result.Succeeded && result.AffectedObjectId.HasValue)
            {
                return RedirectToAction("ModelLine", new { id = result.AffectedObjectId.Value, result = result.Id });
            }
            return RedirectToAction("AddModelLine", new { id, result = result.Id });
        }
        public ActionResult EditModelLine(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetModelLine(id));
        }
        [HttpPost]
        public ActionResult ManageModelLineEditing(int id, string name)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.EditModelLine(id, name);
            if (result.Succeeded)
            {
                return RedirectToAction("ModelLine", new { id, result = result.Id });
            }
            return RedirectToAction("EditModelLine", new { id, result = result.Id });
        }
        public ActionResult DeleteModelLine(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetModelLine(id));
        }
        [HttpPost]
        public ActionResult ManageModelLineDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.DeleteModelLine(id);
            if (result.Succeeded && result.AffectedObjectId.HasValue)
            {
                return RedirectToAction("Brand", new { id = result.AffectedObjectId.Value, result = result.Id });
            }
            return RedirectToAction("DeleteModelLine", new { id, result = result.Id });
        }
        #endregion

        #region Model

        public ActionResult Models()
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            return View(DataManager.Cars.CarModels());
        }
        public ActionResult Model(int? result, int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            //if (id <= 0) return View(DataManager.Cars.Modifications());
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetCarModel(id));
        }

        public ActionResult AddModel(int? result, int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(Tuple.Create(DataManager.Cars.GetModelLine(id), DataManager.Cars.BodyClasses));
        }

        [HttpPost]
        public ActionResult ManageModelAdding(int id, int[] results, string name, int bodyType, int? start, int? end, HttpPostedFileBase imageUpload)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.AddCarModel(id, results, name, bodyType, start, end, imageUpload, Server);
            if (result.Succeeded && result.AffectedObjectId.HasValue)
            {
                return RedirectToAction("Model", new { id = result.AffectedObjectId.Value, result = result.Id });
            }
            return RedirectToAction("AddModel", new { id, result = result.Id });
        }

        public ActionResult EditModel(int? result, int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(Tuple.Create(DataManager.Cars.GetCarModel(id), DataManager.Cars.BodyClasses));
        }

        [HttpPost]
        public ActionResult ManageModelEditing(int id, int[] results, string name, int bodyType, int? start, int? end, HttpPostedFileBase imageUpload, bool deleteImage = false)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.EditCarModel(id, results, name, bodyType, start, end, imageUpload, deleteImage, Server);
            if (result.Succeeded)
            {
                return RedirectToAction("Model", new { id, result = result.Id });
            }
            return RedirectToAction("EditModel", new { id, result = result.Id });
        }

        public ActionResult DeleteModel(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetCarModel(id));
        }

        [HttpPost]
        public ActionResult ManageModelDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.DeleteCarModel(id, Server);
            if (result.Succeeded && result.AffectedObjectId.HasValue)
            {
                return RedirectToAction("ModelLine", new { id = result.AffectedObjectId.Value, result = result.Id });
            }
            return RedirectToAction("DeleteModel", new { id, result = result.Id });
        }
        #endregion

        #region Modification

        public ActionResult Modification(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetModification(id));
        }

        public ActionResult AddModification(int? result, int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(new AddModificationModel(
                DataManager.Cars.GetCarModel(id),
                DataManager.Cars.EngineLocations,
                DataManager.Cars.ValvesArrangements,
                DataManager.Cars.FuelTypes,
                DataManager.Cars.BatteryTypes,
                DataManager.Cars.TransmissionTypes,
                DataManager.Cars.TireCarcassTypes));
        }

        [HttpPost]
        public ActionResult ManageModificationAdding(Modification modification, int ice, int ee)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.AddModification(modification, ice, ee);
            if (result.Succeeded && result.AffectedObjectId.HasValue)
            {
                return RedirectToAction("Modification", new { id = result.AffectedObjectId.Value, result = result.Id });
            }
            return RedirectToAction("AddModification", new { id = modification.ModelId, result = result.Id });
        }

        public ActionResult EditModification(int? result, int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(new EditModificationModel(
                DataManager.Cars.GetModification(id),
                DataManager.Cars.EngineLocations,
                DataManager.Cars.ValvesArrangements,
                DataManager.Cars.FuelTypes,
                DataManager.Cars.BatteryTypes,
                DataManager.Cars.TransmissionTypes,
                DataManager.Cars.TireCarcassTypes));
        }

        [HttpPost]
        public ActionResult ManageModificationEditing(Modification modification, int ice, int ee)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.EditModification(modification, ice, ee);
            if (result.Succeeded)
            {
                return RedirectToAction("Modification", new { id = modification.Id, result = result.Id });
            }
            return RedirectToAction("EditModification", new { id = modification.Id, result = result.Id });
        }

        public ActionResult DeleteModification(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Cars.GetModification(id));
        }

        [HttpPost]
        public ActionResult ManageModificationDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Cars.DeleteModification(id);
            if (result.Succeeded && result.AffectedObjectId.HasValue)
            {
                return RedirectToAction("Model", new { id = result.AffectedObjectId.Value, result = result.Id });
            }
            return RedirectToAction("DeleteModification", new { id, result = result.Id });
        }
        #endregion

        #region News

        public ActionResult AllNews()
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            return View(DataManager.News.All());
        }
        public ActionResult News(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            return View(DataManager.News.GetNews(id));
        }

        public ActionResult AddNews(int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View();
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageNewsAdding(News news, HttpPostedFileBase imageUpload)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.News.AddNews(news, imageUpload, Server);
            if (result.Succeeded && result.AffectedObjectId.HasValue)
            {
                return RedirectToAction("News", new { id = result.AffectedObjectId.Value, result = result.Id });
            }
            return RedirectToAction("AddNews", new { id = news.Id, result = result.Id });
        }

        public ActionResult EditNews(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.News.GetNews(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageNewsEditing(News news, HttpPostedFileBase imageUpload)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.News.EditNews(news, imageUpload, Server);
            if (result.Succeeded)
            {
                return RedirectToAction("News", new { id = news.Id, result = result.Id });
            }
            return RedirectToAction("EditNews", new { id = news.Id, result = result.Id });
        }
        public ActionResult DeleteNews(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.News.GetNews(id));
        }
        [HttpPost]
        public ActionResult ManageNewsDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.News.DeleteNews(id, Server);
            if (result.Succeeded)
            {
                return RedirectToAction("AllNews");
            }
            return RedirectToAction("DeleteNews", new { id, result = result.Id });
        }
        #endregion

        #region Setting

        public ActionResult Settings()
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            return View();
        }

        #endregion





    }
}