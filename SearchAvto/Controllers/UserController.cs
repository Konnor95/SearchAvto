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

		public ActionResult LogIn()
		{
			return View(new LogInModel());
		}

		public ActionResult ManageLogIn(LogInModel model)
		{
			var curUser = DataManager.Users.LogInUser(model);
			if (curUser != null)
			{
				SetUser(curUser.Id, curUser.Password);
				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("LogIn");
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
		public ActionResult RegistrateUser(RegistrationModel registrationModel)
		{
			if (!DataManager.Users.RegistrateUser(registrationModel))
				return RedirectToAction("Registrate", "User", new { message = "Пользователь с таким адресом электронной почты уже существует."});
			return View();
		}

		public ActionResult Registrate(string message = "")
		{
			if (String.IsNullOrEmpty(message))
				message = "Регистрация";

			ViewBag.Message = message;

			return View();
		}
	}
}