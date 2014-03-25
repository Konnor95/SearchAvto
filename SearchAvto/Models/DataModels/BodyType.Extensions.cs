using SearchAvto.Models.LogicModels;

namespace SearchAvto.Models.DataModels
{
    public partial class BodyType
    {
        public BodyClass BodyClass
        {
            get { return DataManager.Cars.DefineBodyClass(BodyClassId); }
        }

        public string BaseBodyType
        {
            get
            {
                return IsBase ? "-" : DataManager.Cars.GetBodyType(BaseBodyTypeId).Name;
            }
        }

        public bool IsBase
        {
            get { return BaseBodyTypeId == 0; }
        }
    }
}