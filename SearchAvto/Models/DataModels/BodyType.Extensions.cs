using SearchAvto.Models.LogicModels;

namespace SearchAvto.Models.DataModels
{
    public partial class BodyType
    {
        public BodyClass BodyClass
        {
            get { return DataManager.Cars.DefineBodyClass(BodyClassId); }
        }
    }
}