using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SearchAvto.Models.DataModels;
using SearchAvto.Models.LogicModels;
using SearchAvto.Models.ViewModels;

namespace SearchAvto.Controllers
{
    public class DashboardController : Controller
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        private string ToJsonString(object data)
        {
            return Serializer.Serialize(data);
        }
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

        #region Settings

        private bool HasNoAdminAccess(User user)
        {
            return (user == null || !user.HasAdminAccess);
        }
        public ActionResult Settings(int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(new SettingsModel(DataManager.Cars.BatteryTypes,
                                          DataManager.Cars.BodyTypes,
                                          DataManager.Cars.FuelTypes,
                                          DataManager.Cars.TireCarcassTypes,
                                          DataManager.Cars.TransmissionTypes,
                                          DataManager.Cars.EngineLocations,
                                          DataManager.Cars.ValvesArrangements));
        }

        public ActionResult AddBatteryType(int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View();
        }

        [HttpPost]
        public ActionResult ManageBatteryTypeAdding(BatteryType batteryType)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.AddBatteryType(batteryType);
            return RedirectToAction(result.Succeeded ? "Settings" : "AddBatteryType", new { result = result.Id });
        }

        public ActionResult EditBatteryType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetBatteryType(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageBatteryTypeEditing(BatteryType batteryType)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.EditBatteryType(batteryType);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings");
            }
            return RedirectToAction("EditBatteryType", new { id = batteryType.Id, result = result.Id });
        }

        public ActionResult DeleteBatteryType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetBatteryType(id));
        }
        [HttpPost]
        public ActionResult ManageBatteryTypeDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.DeleteBatteryType(id);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings");
            }
            return RedirectToAction("DeleteBatteryType", new { id, result = result.Id });
        }








        public ActionResult AddBodyType(int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(Tuple.Create(DataManager.Cars.BodyClasses, DataManager.Cars.GetBaseBodyTypes()));
        }

        [HttpPost]
        public ActionResult ManageBodyTypeAdding(BodyType bodyType)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.AddBodyType(bodyType);
            return RedirectToAction(result.Succeeded ? "Settings" : "AddBodyType", new { result = result.Id });
        }

        public ActionResult EditBodyType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(Tuple.Create(DataManager.Settings.GetBodyType(id), DataManager.Cars.BodyClasses, DataManager.Cars.GetBaseBodyTypes()));
        }

        [HttpPost]
        public ActionResult ManageBodyTypeEditing(BodyType bodyType)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.EditBodyType(bodyType);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("EditBodyType", new { id = bodyType.Id, result = result.Id });
        }

        public ActionResult DeleteBodyType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetBodyType(id));
        }
        [HttpPost]
        public ActionResult ManageBodyTypeDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.DeleteBodyType(id);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("DeleteBodyType", new { id, result = result.Id });
        }





        public ActionResult AddFuelType(int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View();
        }

        [HttpPost]
        public ActionResult ManageFuelTypeAdding(FuelType fuelType)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.AddFuelType(fuelType);
            return RedirectToAction(result.Succeeded ? "Settings" : "AddFuelType", new { result = result.Id });
        }

        public ActionResult EditFuelType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetFuelType(id));
        }

        [HttpPost]
        public ActionResult ManageFuelTypeEditing(FuelType fuelType)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.EditFuelType(fuelType);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("EditFuelType", new { id = fuelType.Id, result = result.Id });
        }

        public ActionResult DeleteFuelType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetFuelType(id));
        }
        [HttpPost]
        public ActionResult ManageFuelTypeDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.DeleteFuelType(id);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("DeleteFuelType", new { id, result = result.Id });
        }






        public ActionResult AddTireCarcassType(int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View();
        }

        [HttpPost]
        public ActionResult ManageTireCarcassTypeAdding(TireCarcassType type)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.AddTireCarcassType(type);
            return RedirectToAction(result.Succeeded ? "Settings" : "AddTireCarcassType", new { result = result.Id });
        }

        public ActionResult EditTireCarcassType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetTireCarcassType(id));
        }

        [HttpPost]
        public ActionResult ManageTireCarcassTypeEditing(TireCarcassType type)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.EditTireCarcassType(type);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("EditTireCarcassType", new { id = type.Id, result = result.Id });
        }

        public ActionResult DeleteTireCarcassType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetTireCarcassType(id));
        }
        [HttpPost]
        public ActionResult ManageTireCarcassTypeDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.DeleteTireCarcassType(id);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("DeleteTireCarcassType", new { id, result = result.Id });
        }





        public ActionResult AddTransmissionType(int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View();
        }

        [HttpPost]
        public ActionResult ManageTransmissionTypeAdding(TransmissionType type)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.AddTransmissionType(type);
            return RedirectToAction(result.Succeeded ? "Settings" : "AddTransmissionType", new { result = result.Id });
        }

        public ActionResult EditTransmissionType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetTransmissionType(id));
        }

        [HttpPost]
        public ActionResult ManageTransmissionTypeEditing(TransmissionType type)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.EditTransmissionType(type);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("EditTransmissionType", new { id = type.Id, result = result.Id });
        }

        public ActionResult DeleteTransmissionType(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetTransmissionType(id));
        }
        [HttpPost]
        public ActionResult ManageTransmissionTypeDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.DeleteTransmissionType(id);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("DeleteTransmissionType", new { id, result = result.Id });
        }


        public ActionResult AddEngineLocation(int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View();
        }

        [HttpPost]
        public ActionResult ManageEngineLocationAdding(EngineLocation type)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.AddEngineLocation(type);
            return RedirectToAction(result.Succeeded ? "Settings" : "AddEngineLocation", new { result = result.Id });
        }

        public ActionResult EditEngineLocation(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetEngineLocation(id));
        }

        [HttpPost]
        public ActionResult ManageEngineLocationEditing(EngineLocation type)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.EditEngineLocation(type);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("EditEngineLocation", new { id = type.Id, result = result.Id });
        }

        public ActionResult DeleteEngineLocation(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetEngineLocation(id));
        }
        [HttpPost]
        public ActionResult ManageEngineLocationDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.DeleteEngineLocation(id);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("DeleteEngineLocation", new { id, result = result.Id });
        }




        public ActionResult AddValvesArrangement(int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View();
        }

        [HttpPost]
        public ActionResult ManageValvesArrangementAdding(ValvesArrangement type)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.AddValvesArrangement(type);
            return RedirectToAction(result.Succeeded ? "Settings" : "AddValvesArrangement", new { result = result.Id });
        }

        public ActionResult EditValvesArrangement(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetValvesArrangement(id));
        }

        [HttpPost]
        public ActionResult ManageValvesArrangementEditing(ValvesArrangement type)
        {
            var user = DefineUser();
            if (HasNoAdminAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.EditValvesArrangement(type);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("EditTransmissionType", new { id = type.Id, result = result.Id });
        }

        public ActionResult DeleteValvesArrangement(int id, int? result)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
            return View(DataManager.Settings.GetValvesArrangement(id));
        }
        [HttpPost]
        public ActionResult ManageValvesArrangementDeleting(int id)
        {
            var user = DefineUser();
            if (HasNoAccess(user)) return NoPermission();
            ProcessResult result = DataManager.Settings.DeleteValvesArrangement(id);
            if (result.Succeeded)
            {
                return RedirectToAction("Settings", new { result = result.Id });
            }
            return RedirectToAction("DeleteValvesArrangement", new { id, result = result.Id });
        }

        #endregion


        #region Statistics

        public ActionResult Statistics()
        {
            ViewBag.BodyTypes = ToJsonString(BodyTypesStatistics());
            ViewBag.Brands = ToJsonString(BrandsStatistics());
            ViewBag.News = ToJsonString(NewsStatistics());
            ViewBag.Engines = ToJsonString(EnginesStatistics());
            return View();
        }

        public IEnumerable BodyTypesStatistics()
        {
            var bodyTypes = new ArrayList();
            var bodys = DataManager.Cars.BodyTypes;
            foreach (BodyType bodyType in bodys)
            {
                int c = bodyType.CarModels.Count;
                if (c > 0)
                    bodyTypes.Add(new { label = bodyType.Name, value = c });
            }
            return bodyTypes;
        }

        private IEnumerable BrandsStatistics()
        {
            var res = new ArrayList();
            var brands = DataManager.Cars.Brands();
            foreach (Brand brand in brands)
            {
                int c = brand.ModelLines.Sum(m => m.CarModels.Count);
                if (c > 0)
                    res.Add(new { y = brand.Name, a = c });
            }
            return res;
        }

        // { y: '2006', a: 100 },

        public IEnumerable NewsStatistics()
        {
            var res = new ArrayList();
            var months = new int[12];
            string[] monthNames =
            {
                "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь",
                "Декабрь"
            };
            var news = DataManager.News.NewsOfTheLastYear();
            foreach (News n in news)
            {
                months[n.Date.Month]++;
            }
            for (int i = 0; i < months.Length; i++)
            {
                if (months[i] > 0)
                    res.Add(new { y = monthNames[i-1], a = months[i] });
            }
            return res;
        }
        public IEnumerable EnginesStatistics()
        {
            var res = new ArrayList();
            var engines = new int[3];
            var engineNames = new[]{"ДВС", "Электрокары", "Гибриды"};
            var models = DataManager.Cars.CarModels();
            foreach (CarModel m in models)
            {
                engines[m.TEngine]++;
            }
            for (int i = 0; i < engines.Length; i++)
            {
                int e = engines[i];
                if (e > 0) res.Add(new { label = engineNames[i], value = e });
            }
            return res;
        }
        #endregion


    }
}