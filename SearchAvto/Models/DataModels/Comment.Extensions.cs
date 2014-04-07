using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchAvto.Models.DataModels
{
    public partial class Comment
    {
        public string ShortText
        {
            get
            {
                if (Text.Length < 50) return Text;
                return Text.Substring(0, 50) + "...";
            }
        }
    }
}