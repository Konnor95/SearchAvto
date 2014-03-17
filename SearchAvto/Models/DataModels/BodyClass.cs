using System.Collections.Generic;

namespace SearchAvto.Models.DataModels
{
    public class BodyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BodyType> BodyTypes { get; private set; } 

        public BodyClass(int id, string name,IEnumerable<BodyType> bodyTypes)
        {
            Id = id;
            Name = name;
            BodyTypes = bodyTypes;
        }

    }
}