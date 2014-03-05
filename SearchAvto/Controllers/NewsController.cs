using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchAvto.Models.LogicModels;

namespace SearchAvto.Controllers
{
	public class NewsController : Controller
	{
		[Route("News")]
		public ActionResult Index(string search = null)
		{
			return View(String.IsNullOrWhiteSpace(search) ? DataManager.News.All() : DataManager.News.Search(search));
		}

		[Route("News/{id}")]
		public ActionResult News(int id)
		{
			return View(DataManager.News.GetNews(id));
		}

	}
}