using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchAvto.Models.DataModels;
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