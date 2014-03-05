using SearchAvto.Models.DataModels;

namespace SearchAvto.Models.LogicModels
{
    public class DataManager
    {
        private static readonly DatabaseEntities Data = new DatabaseEntities();
        public static CarManager Cars = new CarManager(Data);
        public static NewsManger News = new NewsManger(Data);
        public static UserManager Users = new UserManager(Data); 
    }
}