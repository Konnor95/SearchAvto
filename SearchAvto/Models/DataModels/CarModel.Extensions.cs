using System;

namespace SearchAvto.Models.DataModels
{
    public partial class CarModel
    {
        public string FullName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Name)) return ModelLine.FullName + " " + Name;
                if (StartYearOfProduction != null)
                {
                    return ModelLine.FullName +" " +  StartYearOfProduction;
                }
                return ModelLine.FullName;
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

        public byte GetTestVaue(string id)
        {
            switch (id)
            {
                case "TMainFeature":
                    return TMainFeature;
                case "TColor":
                    return TColor;
                case "TAge":
                    return TAge;
                case "TSpeed":
                    return TSpeed;
                case "TSize":
                    return TSize;
                case "TEngine":
                    return TEngine;
                case "TPassengers":
                    return TPassengers;
                case "TDistance":
                    return TDistance;
                case "TOutLand":
                    return TOutLand;
                case "TPrice":
                    return TPrice;
            }
            return 0;
        }

        public byte[] TestValues
        {
            get
            {
                return new[]
                {
                    TMainFeature,
                    TColor,
                    TAge,
                    TSpeed,
                    TSize,
                    TEngine,
                    TPassengers,
                    TDistance,
                    TOutLand,
                    TPrice
                };
            }
            set
            {
                TMainFeature = value[0];
                TColor = value[1];
                TAge = value[2];
                TSpeed = value[3];
                TSize = value[4];
                TEngine = value[5];
                TPassengers = value[6];
                TDistance = value[7];
                TOutLand = value[8];
                TPrice = value[9];
            }
        }
    }
}