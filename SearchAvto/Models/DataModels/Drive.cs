using System.Collections.Generic;

namespace SearchAvto.Models.DataModels
{
    public class Drive
    {
        public byte Id { get; private set; }
        public string Name { get; private set; }

        public Drive(byte id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Drives
    {
        readonly static Drive[] DrivesCollection =
        {
            new Drive(0,"Неизвестно"),
            new Drive(1,"Передний"),
            new Drive(2,"Задний"), 
            new Drive(3,"Полный")
        };

        public static Drive Unknown
        {
            get { return DrivesCollection[0]; }
        }

        public static Drive Front
        {
            get { return DrivesCollection[1]; }
        }

        public static Drive Back
        {
            get { return DrivesCollection[2]; }
        }

        public static Drive Full
        {
            get { return DrivesCollection[3]; }
        }

        public static Drive GetById(byte? id)
        {
            if (!id.HasValue ||id.Value > DrivesCollection.Length) return null;
            return DrivesCollection[id.Value];
        }

        public static IEnumerable<Drive> GetAll()
        {
            return DrivesCollection;
        }
    }
}