using System.Linq;

namespace SearchAvto.Models.DataModels
{
    public partial class News
    {
        public string ShortTitle
        {
            get
            {
                if (Title.Length < 50) return Title;
                return Title.Substring(0, 50) + "...";
            }
        }
        
        public bool Contains(params string[] keys)
        {
            return keys.All(key => Title.ToUpper().Contains(key));
        }
    }
}