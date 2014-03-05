namespace SearchAvto.Models.LogicModels
{
    public class StaticSettings
    {
        public static string UploadFolderPath
        {
            get { return "Images/Upload"; }
        }

        public static string BrandsUploadFolderPath
        {
            get { return UploadFolderPath + "/Brands/"; }
        }

        public static string AvatarsUploadFolderPath
        {
            get { return UploadFolderPath + "/Avatars/"; }
        }

        public static string CarsUploadFolderPath
        {
            get { return UploadFolderPath + "/Cars/"; }
        }

        public static string NewsUploadFolderPath
        {
            get { return UploadFolderPath + "/News/"; }
        }
    }
}