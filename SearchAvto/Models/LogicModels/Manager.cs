using System.IO;
using System.Web;
using SearchAvto.Models.DataModels;

namespace SearchAvto.Models.LogicModels
{
    public abstract class Manager
    {
        protected DatabaseEntities Data;

        protected Manager(DatabaseEntities data)
        {
            Data = data;
        }
        protected string SaveImage(int id, string folder, HttpPostedFileBase imageUpload, HttpServerUtilityBase server)
        {
            string relativePath = folder + id + Path.GetExtension(imageUpload.FileName);
            imageUpload.SaveAs(server.MapPath("~") + relativePath);
            return relativePath;
        }

        protected void DeleteImage(string virtualPath, HttpServerUtilityBase server)
        {
            if (virtualPath != null)
            {
                string path = server.MapPath("~") + virtualPath;
                var file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }
    }
}