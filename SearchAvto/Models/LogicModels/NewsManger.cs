using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchAvto.Models.DataModels;

namespace SearchAvto.Models.LogicModels
{
    public class NewsManger : Manager
    {
        public NewsManger(DatabaseEntities data)
            : base(data)
        {
        }

        public int MaxPage(int newsPerPages)
        {
            return (int)Math.Ceiling(((decimal)Data.News.Count() / newsPerPages));
        }
        public IEnumerable<News> All(out int? max, int page = 1, int? newsPerPages = null)
        {
            if (newsPerPages == null)
            {
                var n2 = Data.News.OrderByDescending(x => x.Date);
                max = n2.Count();
                return n2.Take(4);
            }
            var n3 = Data.News.OrderByDescending(x => x.Date);
            max = (int)Math.Ceiling((decimal)n3.Count()/newsPerPages.Value);
            return n3.Skip((page - 1) * newsPerPages.Value).Take(newsPerPages.Value);
        }

        public IEnumerable<News> All(int? count = null)
        {
            if (count.HasValue)
                return Data.News.OrderByDescending(x => x.Date).Take(count.Value);
            return Data.News.OrderByDescending(x => x.Date);
        }

        public IEnumerable<News> NewsOfTheLastYear()
        {
            DateTime lastYear = DateTime.Now.AddMonths(-12);
            return Data.News.Where(x => x.Date >= lastYear);
        }

        public IEnumerable<News> Search(string search, int page, int newsPerPages, out int? max)
        {
            search = search.ToUpper();
            string[] keys = search.Split(' ');
            var n = new List<News>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (News news in Data.News)
            {
                if (news.Contains(keys))
                    n.Add(news);
            }
            max = (int)Math.Ceiling((decimal)n.Count()/newsPerPages);
            return n.Skip((page - 1) * newsPerPages).Take(newsPerPages);
        }

        public News GetNews(int id)
        {
            return Data.News.FirstOrDefault(x => x.Id == id);
        }

        [ValidateInput(false)]
        public ProcessResult AddNews(News news, HttpPostedFileBase imageUpload, HttpServerUtilityBase server)
        {
            if (String.IsNullOrEmpty(news.Title))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            if (String.IsNullOrEmpty(news.Text))
            {
                return ProcessResults.TextCannotBeEmpty;
            }
            News n = Data.News.Add(news);
            Data.SaveChanges();
            if (imageUpload != null)
            {
                if (imageUpload.ContentLength <= 0 || !SecurityManager.IsImage(imageUpload))
                {
                    return ProcessResults.InvalidImageFormat;
                }
                n.Image = SaveImage(n.Id, StaticSettings.NewsUploadFolderPath, imageUpload, server);
                Data.SaveChanges();
            }
            ProcessResult result = ProcessResults.NewsAdded;
            result.AffectedObjectId = n.Id;
            return result;
        }

        [ValidateInput(false)]
        public ProcessResult EditNews(News news, HttpPostedFileBase imageUpload, HttpServerUtilityBase server)
        {

            if (String.IsNullOrEmpty(news.Title))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            if (String.IsNullOrEmpty(news.Text))
            {
                return ProcessResults.TextCannotBeEmpty;
            }
            News n = GetNews(news.Id);
            n.Title = news.Title;
            n.Date = news.Date;
            n.Text = news.Text;
            Data.SaveChanges();
            if (imageUpload != null)
            {
                if (imageUpload.ContentLength <= 0 || !SecurityManager.IsImage(imageUpload))
                {
                    return ProcessResults.InvalidImageFormat;
                }
                n.Image = SaveImage(n.Id, StaticSettings.NewsUploadFolderPath, imageUpload, server);
                Data.SaveChanges();
            }
            return ProcessResults.NewsChanged;
        }

        public ProcessResult DeleteNews(int id, HttpServerUtilityBase server)
        {
            News n = GetNews(id);
            if (n == null)
            {
                return ProcessResults.NoSuchNews;
            }
            string img = n.Image;
            Data.Comments.RemoveRange(GetComments(id));
            Data.News.Remove(n);
            Data.SaveChanges();
            DeleteImage(img, server);
            return ProcessResults.NewsDeleted;
        }

        public IEnumerable<Comment> GetComments(int id)
        {
            return Data.Comments.Where(x => x.NewsId == id);
        }

        public Comment GetComment(int id)
        {
            return Data.Comments.FirstOrDefault(x => x.Id == id);
        }

        public ProcessResult AddComment(int newsId, int userId, string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return ProcessResults.CommentCannotBeEmpty;
            News news = Data.News.FirstOrDefault(x => x.Id == newsId);
            if (news == null)
                return ProcessResults.NoSuchNews;
            Data.Comments.Add(new Comment { NewsId = newsId, UserId = userId, Text = text });
            Data.SaveChanges();
            return ProcessResults.CommentAddedSuccessfully;
        }

        public ProcessResult DeleteComment(int id)
        {
            Comment comment = Data.Comments.FirstOrDefault(x => x.Id == id);
            if (comment == null)
                return ProcessResults.NoSuchComment;
            Data.Comments.Remove(comment);
            Data.SaveChanges();
            return ProcessResults.CommentDeleted;
        }
    }
}