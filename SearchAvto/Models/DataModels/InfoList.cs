using System.Collections.Generic;

namespace SearchAvto.Models.DataModels
{
    public class InfoListItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public InfoListItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class InfoList
    {
        public static  IEnumerable<InfoListItem> Items
        {
            get
            {
                return new[]
                {
                    new InfoListItem(0, "Отсутствует"),
                    new InfoListItem(1,"Присутствует"), 
                    new InfoListItem(2, "Выбрать из существующих") 
                };
            }
        }
    }
}