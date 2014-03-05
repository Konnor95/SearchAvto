using System.Collections.Generic;
using System.Linq;
using SearchAvto.Models.DataModels;

namespace SearchAvto.Models.ViewModels
{
    public class ModificationModel
    {
        public List<EngineLocation> EngineLocations;
        public List<ValvesArrangement> ValvesArrangements;
        public List<FuelType> FuelTypes;
        public IEnumerable<Modification> Modifications;
        public List<BatteryType> BatteryTypes;
        public List<TransmissionType> TransmissionTypes;
        public List<TireCarcassType> TireCarcassTypes;

        protected ModificationModel(IEnumerable<EngineLocation> engineLocations, IEnumerable<ValvesArrangement> valvesArrangements, IEnumerable<FuelType> fuelTypes, IEnumerable<BatteryType> batteryTypes, IEnumerable<TransmissionType> transmissionTypes, IEnumerable<TireCarcassType> tireCarcassTypes)
        {
            ValvesArrangements = new List<ValvesArrangement> { new ValvesArrangement { Id = 0, Name = "Неизвестно" } };
            ValvesArrangements.AddRange(valvesArrangements);
            EngineLocations = new List<EngineLocation> { new EngineLocation { Id = 0, Name = "Неизвестно" } };
            EngineLocations.AddRange(engineLocations);
            FuelTypes = new List<FuelType> { new FuelType { Id = 0, Name = "Неизвестно" } };
            FuelTypes.AddRange(fuelTypes);
            BatteryTypes = new List<BatteryType> { new BatteryType { Id = 0, Name = "Неизвестно" } };
            BatteryTypes.AddRange(batteryTypes);
            TransmissionTypes = new List<TransmissionType> { new TransmissionType { Id = 0, Name = "Неизвестно" } };
            TransmissionTypes.AddRange(transmissionTypes);
            TireCarcassTypes = new List<TireCarcassType> { new TireCarcassType { Id = 0, FullName = "Неизвестно" } };
            TireCarcassTypes.AddRange(tireCarcassTypes);
        }
    }

    public class AddModificationModel : ModificationModel
    {
        public CarModel CarModel;
        public AddModificationModel(CarModel carModel, IEnumerable<EngineLocation> engineLocations, IEnumerable<ValvesArrangement> valvesArrangements, IEnumerable<FuelType> fuelTypes, IEnumerable<BatteryType> batteryTypes, IEnumerable<TransmissionType> transmissionTypes, IEnumerable<TireCarcassType> tireCarcassTypes)
            : base(engineLocations, valvesArrangements, fuelTypes, batteryTypes, transmissionTypes, tireCarcassTypes)
        {
            CarModel = carModel;
            Modifications = CarModel.Modifications;
        }
    }

    public class EditModificationModel : ModificationModel
    {
        public Modification Modification;
        public EditModificationModel(Modification modification, IEnumerable<EngineLocation> engineLocations, IEnumerable<ValvesArrangement> valvesArrangements, IEnumerable<FuelType> fuelTypes, IEnumerable<BatteryType> batteryTypes, IEnumerable<TransmissionType> transmissionTypes, IEnumerable<TireCarcassType> tireCarcassTypes)
            : base(engineLocations, valvesArrangements, fuelTypes, batteryTypes, transmissionTypes, tireCarcassTypes)
        {
            Modification = modification;
            Modifications = modification.CarModel.Modifications.Where(x => x.Id != Modification.Id &&
                x.InternalCombustionEngineId != Modification.InternalCombustionEngineId &&
                x.ElectricEngineId != Modification.ElectricEngineId);
        }
    }
}