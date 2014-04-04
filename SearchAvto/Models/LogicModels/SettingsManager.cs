using System;
using System.Linq;
using SearchAvto.Models.DataModels;

namespace SearchAvto.Models.LogicModels
{
    public class SettingsManager : Manager
    {
        public SettingsManager(DatabaseEntities data)
            : base(data)
        {
        }

        public BatteryType GetBatteryType(int id)
        {
            return Data.BatteryTypes.FirstOrDefault(x => x.Id == id);
        }

        public BatteryType GetBatteryType(string name)
        {
            return Data.BatteryTypes.FirstOrDefault(x => x.Name == name);
        }

        public ProcessResult AddBatteryType(BatteryType batteryType)
        {
            if (String.IsNullOrEmpty(batteryType.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            BatteryType b = GetBatteryType(batteryType.Name);
            if (b != null)
            {
                return ProcessResults.SuchBatteryTypeExists;
            }
            Data.BatteryTypes.Add(batteryType);
            Data.SaveChanges();
            return ProcessResults.BatteryTypeAdded;
        }

        public ProcessResult EditBatteryType(BatteryType batteryType)
        {
            if (String.IsNullOrEmpty(batteryType.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            BatteryType b = GetBatteryType(batteryType.Id);
            if (b == null)
            {
                return ProcessResults.NoSuchBatteryType;
            }
            b.Name = batteryType.Name;
            Data.SaveChanges();
            return ProcessResults.BatteryTypeChanged;
        }

        public ProcessResult DeleteBatteryType(int id)
        {
            BatteryType b = GetBatteryType(id);
            if (b == null)
            {
                return ProcessResults.NoSuchBatteryType;
            }
            Data.BatteryTypes.Remove(b);
            Data.SaveChanges();
            return ProcessResults.BatteryTypeDeleted;
        }



        public BodyType GetBodyType(int id)
        {
            return Data.BodyTypes.FirstOrDefault(x => x.Id == id);
        }

        public BodyType GetBodyType(string name)
        {
            return Data.BodyTypes.FirstOrDefault(x => x.Name == name);
        }

        public ProcessResult AddBodyType(BodyType bodyType)
        {
            if (String.IsNullOrEmpty(bodyType.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            BodyType b = GetBodyType(bodyType.Name);
            if (b != null)
            {
                return ProcessResults.SuchBodyTypeExists;
            }
            Data.BodyTypes.Add(bodyType);
            Data.SaveChanges();
            return ProcessResults.BodyTypeAdded;
        }

        public ProcessResult EditBodyType(BodyType bodyType)
        {
            if (String.IsNullOrEmpty(bodyType.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            BodyType b = GetBodyType(bodyType.Id);
            if (b == null || b.IsBase)
            {
                return ProcessResults.NoSuchBodyType;
            }
            b.Name = bodyType.Name;
            b.BodyClassId = bodyType.BodyClassId;
            b.Description = bodyType.Description;
            b.BaseBodyTypeId = bodyType.BaseBodyTypeId;
            Data.SaveChanges();
            return ProcessResults.BodyTypeChanged;
        }

        public ProcessResult DeleteBodyType(int id)
        {
            BodyType b = GetBodyType(id);
            if (b == null)
            {
                return ProcessResults.NoSuchBodyType;
            }
            Data.BodyTypes.Remove(b);
            Data.SaveChanges();
            return ProcessResults.BodyTypeDeleted;
        }




        public FuelType GetFuelType(int id)
        {
            return Data.FuelTypes.FirstOrDefault(x => x.Id == id);
        }

        public FuelType GetFuelType(string name)
        {
            return Data.FuelTypes.FirstOrDefault(x => x.Name == name);
        }

        public ProcessResult AddFuelType(FuelType fuelType)
        {
            if (String.IsNullOrEmpty(fuelType.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            FuelType b = GetFuelType(fuelType.Name);
            if (b != null)
            {
                return ProcessResults.SuchFuelTypeExists;
            }
            Data.FuelTypes.Add(fuelType);
            Data.SaveChanges();
            return ProcessResults.FuelTypeAdded;
        }

        public ProcessResult EditFuelType(FuelType batteryType)
        {
            if (String.IsNullOrEmpty(batteryType.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            FuelType b = GetFuelType(batteryType.Id);
            if (b == null)
            {
                return ProcessResults.NoSuchFuelType;
            }
            b.Name = batteryType.Name;
            Data.SaveChanges();
            return ProcessResults.FuelTypeChanged;
        }

        public ProcessResult DeleteFuelType(int id)
        {
            FuelType b = GetFuelType(id);
            if (b == null)
            {
                return ProcessResults.NoSuchFuelType;
            }
            Data.FuelTypes.Remove(b);
            Data.SaveChanges();
            return ProcessResults.FuelTypeDeleted;
        }





        public TireCarcassType GetTireCarcassType(int id)
        {
            return Data.TireCarcassTypes.FirstOrDefault(x => x.Id == id);
        }

        public TireCarcassType GetTireCarcassType(string fullName,string shortName)
        {
            return Data.TireCarcassTypes.FirstOrDefault(x => x.FullName == fullName || x.ShortName == shortName);
        }

        public ProcessResult AddTireCarcassType(TireCarcassType type)
        {
            if (String.IsNullOrEmpty(type.FullName)||String.IsNullOrWhiteSpace(type.ShortName))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            FuelType b = GetFuelType(type.FullName);
            if (b != null)
            {
                return ProcessResults.SuchTireCarcassTypeExists;
            }
            Data.TireCarcassTypes.Add(type);
            Data.SaveChanges();
            return ProcessResults.TireCarcassTypeAdded;
        }

        public ProcessResult EditTireCarcassType(TireCarcassType type)
        {
            if (String.IsNullOrEmpty(type.FullName) || String.IsNullOrWhiteSpace(type.ShortName))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            TireCarcassType b = GetTireCarcassType(type.Id);
            if (b == null)
            {
                return ProcessResults.NoSuchTireCarcassType;
            }
            b.FullName = type.FullName;
            b.ShortName = type.ShortName;
            Data.SaveChanges();
            return ProcessResults.TireCarcassTypeChanged;
        }

        public ProcessResult DeleteTireCarcassType(int id)
        {
            TireCarcassType b = GetTireCarcassType(id);
            if (b == null)
            {
                return ProcessResults.NoSuchTireCarcassType;
            }
            Data.TireCarcassTypes.Remove(b);
            Data.SaveChanges();
            return ProcessResults.TireCarcassTypeDeleted;
        }



        public TransmissionType GetTransmissionType(int id)
        {
            return Data.TransmissionTypes.FirstOrDefault(x => x.Id == id);
        }

        public TransmissionType GetTransmissionType(string name)
        {
            return Data.TransmissionTypes.FirstOrDefault(x => x.Name == name);
        }

        public ProcessResult AddTransmissionType(TransmissionType type)
        {
            if (String.IsNullOrEmpty(type.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            TransmissionType b = GetTransmissionType(type.Name);
            if (b != null)
            {
                return ProcessResults.SuchTransmissionTypeExists;
            }
            Data.TransmissionTypes.Add(type);
            Data.SaveChanges();
            return ProcessResults.TransmissionTypeAdded;
        }

        public ProcessResult EditTransmissionType(TransmissionType type)
        {
            if (String.IsNullOrEmpty(type.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            TransmissionType b = GetTransmissionType(type.Id);
            if (b == null)
            {
                return ProcessResults.NoSuchTransmissionType;
            }
            b.Name = type.Name;
            Data.SaveChanges();
            return ProcessResults.TransmissionTypeChanged;
        }

        public ProcessResult DeleteTransmissionType(int id)
        {
            TransmissionType b = GetTransmissionType(id);
            if (b == null)
            {
                return ProcessResults.NoSuchTransmissionType;
            }
            Data.TransmissionTypes.Remove(b);
            Data.SaveChanges();
            return ProcessResults.TransmissionTypeDeleted;
        }



        public EngineLocation GetEngineLocation(int id)
        {
            return Data.EngineLocations.FirstOrDefault(x => x.Id == id);
        }

        public EngineLocation GetEngineLocation(string name)
        {
            return Data.EngineLocations.FirstOrDefault(x => x.Name == name);
        }

        public ProcessResult AddEngineLocation(EngineLocation type)
        {
            if (String.IsNullOrEmpty(type.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            EngineLocation b = GetEngineLocation(type.Name);
            if (b != null)
            {
                return ProcessResults.SuchEngineLocationExists;
            }
            Data.EngineLocations.Add(type);
            Data.SaveChanges();
            return ProcessResults.EngineLocationAdded;
        }

        public ProcessResult EditEngineLocation(EngineLocation type)
        {
            if (String.IsNullOrEmpty(type.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            EngineLocation b = GetEngineLocation(type.Id);
            if (b == null)
            {
                return ProcessResults.NoSuchEngineLocation;
            }
            b.Name = type.Name;
            Data.SaveChanges();
            return ProcessResults.EngineLocationChanged;
        }

        public ProcessResult DeleteEngineLocation(int id)
        {
            EngineLocation b = GetEngineLocation(id);
            if (b == null)
            {
                return ProcessResults.NoSuchEngineLocation;
            }
            Data.EngineLocations.Remove(b);
            Data.SaveChanges();
            return ProcessResults.EngineLocationDeleted;
        }



        public ValvesArrangement GetValvesArrangement(int id)
        {
            return Data.ValvesArrangements.FirstOrDefault(x => x.Id == id);
        }

        public ValvesArrangement GetValvesArrangement(string name,string codeName)
        {
            return Data.ValvesArrangements.FirstOrDefault(x => x.Name == name || x.CodeName == codeName);
        }

        public ProcessResult AddValvesArrangement(ValvesArrangement type)
        {
            if (String.IsNullOrEmpty(type.Name)||String.IsNullOrWhiteSpace(type.CodeName))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            ValvesArrangement b = GetValvesArrangement(type.Name,type.CodeName);
            if (b != null)
            {
                return ProcessResults.SuchValvesArrangementExists;
            }
            Data.ValvesArrangements.Add(type);
            Data.SaveChanges();
            return ProcessResults.ValvesArrangementAdded;
        }

        public ProcessResult EditValvesArrangement(ValvesArrangement type)
        {
            if (String.IsNullOrEmpty(type.Name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            ValvesArrangement b = GetValvesArrangement(type.Id);
            if (b == null)
            {
                return ProcessResults.NoSuchValvesArrangement;
            }
            b.Name = type.Name;
            b.CodeName = type.CodeName;
            Data.SaveChanges();
            return ProcessResults.ValvesArrangementChanged;
        }

        public ProcessResult DeleteValvesArrangement(int id)
        {
            ValvesArrangement b = GetValvesArrangement(id);
            if (b == null)
            {
                return ProcessResults.NoSuchValvesArrangement;
            }
            Data.ValvesArrangements.Remove(b);
            Data.SaveChanges();
            return ProcessResults.ValvesArrangementDeleted;
        }
    }
}