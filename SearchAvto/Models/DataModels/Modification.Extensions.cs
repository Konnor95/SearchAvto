using System;
using System.Linq;

namespace SearchAvto.Models.DataModels
{
    public partial class Modification
    {
        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(Name))
                {
                    if (HasInternalCombustionEngine)
                    {
                        if (InternalCombustionEngine.Volume != null)
                            return CarModel.FullName + " " +
                                   String.Format("{0:#.##} л", Math.Round((double)InternalCombustionEngine.Volume / 1000, 1));
                        return CarModel.FullName;
                    }
                    if (HasElectricEngine)
                    {
                        if (ElectricEngine.ElectricMotor != null && ElectricEngine.ElectricMotor.Power != null)
                            return CarModel.FullName + " " +
                                   String.Format("{0:#.##} л.с.", ElectricEngine.ElectricMotor.Power);
                        return CarModel.FullName;
                    }
                }
                return CarModel.FullName + " " + Name;
            }
        }

        public bool HasInternalCombustionEngine
        {
            get { return InternalCombustionEngineId != null; }
        }

        public int InternalCombustionEngineLink
        {
            get
            {
                if (InternalCombustionEngineId == null) return 0;
                return 1;
            }
        }
        public bool HasElectricEngine
        {
            get { return ElectricEngine != null && ElectricEngine.HasAnything; }
        }
        public int ElectricEngineLink
        {
            get
            {
                if (ElectricEngineId == null) return 0;
                return 1;
            }
        }
        public bool HasBothEngines
        {
            get { return HasInternalCombustionEngine && HasElectricEngine; }
        }

        public string Type
        {
            get
            {
                if (HasBothEngines) return "Гибрид";
                if (HasElectricEngine) return "Электрокар";
                if (HasInternalCombustionEngine) return "Автомобиль с ДВС";
                return null;
            }
        }

        public bool HasAnyDimension
        {
            get { return Length != null || Width != null || Height != null; }
        }

        public bool HasAnyTransmissionOrWheelInfo
        {
            get { return TransmissionType != null || TransmissionStages != null || Drive != null || WheelFormula!=null; }
        }
        public bool HasAnyMassInfo
        {
            get { return CurbWeight != null || TotalWeight != null; }
        }

        public bool HasAnySpeedInfo
        {
            get { return MaxSpeed != null || Velocity != null; }
        }

        public string DriveName
        {
            get
            {
                if (!Drive.HasValue) return null;
                return Drives.GetById(Drive.Value).Name;
            }
        }

        public bool HasAnyTireInfo()
        {
            return TireCarcassTypeId != null && TireMountingDiameter != null && TireProfileHeight != null && TireProfileWidth != null;
        }
        public bool HasAnyOtherInfo
        {
            get { return CargoVolume != null || MaxCargoVolume != null; }
        }

        public string WheelFormula
        {
            get
            {
                if (NumberOfWheels != null && NumberOfDrivenWheels != null)
                {
                    return NumberOfWheels + "x" + NumberOfDrivenWheels;
                }
                return null;
            }
        }
    }
}