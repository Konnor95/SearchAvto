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

        public IEnumerable<News> All()
        {
            return Data.News.OrderByDescending(x => x.Date);
        }

        public IEnumerable<News> Search(string search)
        {
            search = search.ToUpper();
            string[] keys = search.Split(' ');
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (News news in All())
            {
                if (news.Contains(keys))
                    yield return news;
            }
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
            Data.CommentsToNews.RemoveRange(GetComments(id));
            Data.News.Remove(n);
            Data.SaveChanges();
            DeleteImage(img, server);
            return ProcessResults.NewsDeleted;
        }

        public IEnumerable<CommentsToNew> GetComments(int id)
        {
            return Data.CommentsToNews.Where(x => x.NewsId == id);
        }
    }
}