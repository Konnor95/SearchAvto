namespace SearchAvto.Models.DataModels
{
    public partial class ModelLine
    {
        public string FullName
        {
            get { return Brand.Name + " " + Name; }
        }
    }
}