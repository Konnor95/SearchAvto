using System;
using System.Web;
using System.Web.Mvc;
using SearchAvto.Models.DataModels;
using SearchAvto.Models.LogicModels;
using SearchAvto.Models.ViewModels;

namespace SearchAvto.Controllers
{
	public class UserController : Controller
	{
		public ActionResult Index()
		{
			var user = DataManager.Users.Define(HttpContext);
			if (user == null)
				return RedirectToAction("LogIn");
			return View(user);
		}

		public ActionResult LogIn(int? result)
		{
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
			return View(new LogInModel());
		}

	    public ActionResult Logout()
	    {
            var userCookie = new HttpCookie("UserId")
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            var keyCookie = new HttpCookie("Key")
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            HttpContext.Response.Cookies.Set(userCookie);
            HttpContext.Response.Cookies.Set(keyCookie);

            return RedirectToAction("Index", "Home");
	    }

		public ActionResult ManageLogIn(LogInModel model)
		{
		    User user;
			ProcessResult result = DataManager.Users.LogInUser(model,out user);
		    if (result.Succeeded && user != null)
		    {
                SetUser(user.Id, user.Password);
                return RedirectToAction("Index", "Home");
		    }

			return RedirectToAction("LogIn","User",new{result = result.Id});
		}

		public ActionResult Confirm(string hash)
		{
			ViewBag.Message = DataManager.Users.ConfirmRegistration(hash) ? "Регистрация успешно завершена" : "Ошибка";
			return View();
		}
		private void SetUser(int userId, string hashedKey)
		{
			User user = DataManager.Users.Find(userId);
			if (user == null) return;
			var cookieUser = new HttpCookie("UserId")
			{
				Value = Convert.ToString(userId),
				Expires = DateTime.MaxValue
			};

			var cookieKey = new HttpCookie("Key")
			{
				Value = hashedKey,
				Expires = DateTime.MaxValue
			};

			HttpContext.Response.Cookies.Remove("UserId");
			HttpContext.Response.Cookies.Remove("Key");
			HttpContext.Response.SetCookie(cookieUser);
			HttpContext.Response.SetCookie(cookieKey);
		}

		[HttpPost]
        public ActionResult RegistrateUser(RegistrationModel registrationModel, HttpPostedFileBase imageUpload)
		{
		    ProcessResult result = DataManager.Users.RegistrateUser(HttpContext, registrationModel,Server,imageUpload);
			if (!result.Succeeded)
				return RedirectToAction("Registrate", "User", new { result = result.Id });
			return View();
		}

		public ActionResult Registrate(int? result )
		{
            if (result.HasValue)
                ViewBag.Result = ProcessResults.GetById(result.Value);
			return View();
		}
	}
}