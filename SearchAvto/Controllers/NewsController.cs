using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SearchAvto.Models.DataModels;
using SearchAvto.Models.LogicModels;

namespace SearchAvto.Controllers
{
    public class NewsController : Controller
    {
        private const int NewsPerPages = 3;

        [Route("News")]
        public ActionResult Index(int page = 1, string search = null)
        {
            int? max;
            ViewBag.Current = page;
            var news = String.IsNullOrWhiteSpace(search)
                ? DataManager.News.All(out max, page, NewsPerPages)
                : DataManager.News.Search(search, page, NewsPerPages, out max);
            if (max.HasValue)
                ViewBag.Max = max.Value;
            ViewBag.Search = search;
            return View(news);
        }

        [Route("News/{id}")]
        public ActionResult News(int id)
        {
            return View(DataManager.News.GetNews(id));
        }

        public PartialViewResult Comments(IEnumerable<Comment> comments)
        {
            return PartialView(comments);
        }

        private User DefineUser()
        {
            return DataManager.Users.Define(HttpContext);
        }

        private bool HasNoAccess(User user)
        {
            return user == null || user.HasNoAccess;
        }


        public PartialViewResult AddComment(int id, string text)
        {
            var user = DefineUser();
            if (!HasNoAccess(user))
            {
                ProcessResult result = DataManager.News.AddComment(id, user.Id, text);
                ViewBag.Result = result.Message;
            }
            else
                ViewBag.Result = "Войдите в систему";
            return PartialView("Comments", DataManager.News.GetComments(id));
        }
    }
}