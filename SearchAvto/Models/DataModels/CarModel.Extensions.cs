using System;

namespace SearchAvto.Models.DataModels
{
    public partial class CarModel
    {
        public string FullName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Name)) return ModelLine.FullName;
                return ModelLine.FullName + " " + Name;
            }
        }

        public string StartYearOfProduction
        {
            get
            {
                if (ProductionStarted == null) return "Неизвестно";
                return ProductionStarted.Value.Year.ToString();
            }
        }

        public string EndYearOfProduction
        {
            get
            {
                if (ProductionEnded == null) return "Неизвестно";
                return ProductionEnded.Value.Year.ToString();
            }
        }
        public int? StartYearOfProductionAsNumber
        {
            get
            {
                if (ProductionStarted == null) return null;
                return ProductionStarted.Value.Year;
            }
        }

        public int? EndYearOfProductionAsNumber
        {
            get
            {
                if (ProductionEnded == null) return null;
                return ProductionEnded.Value.Year;
            }
        }
        public string YearsOfProduction
        {
            get
            {
                if (ProductionStarted == null && ProductionEnded == null) return "";
                string start = "...";
                if (ProductionStarted != null) start = ProductionStarted.Value.Year.ToString();
                string end = "...";
                if (ProductionEnded != null) end = ProductionEnded.Value.Year.ToString();
                return String.Format("{0} - {1}", start, end);
            }
        }

        
    }
}