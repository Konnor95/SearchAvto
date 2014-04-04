using System.Collections.Generic;
using SearchAvto.Models.DataModels;

namespace SearchAvto.Models.ViewModels
{
    public class SettingsModel
    {
        public IEnumerable<BatteryType> BatteryTypes { get; private set; }
        public IEnumerable<BodyType> BodyTypes { get; private set; }
        public IEnumerable<FuelType> FuelTypes { get; private set; }
        public IEnumerable<TireCarcassType> TireCarcassTypes { get; private set; }
        public IEnumerable<TransmissionType> TransmissionTypes { get; private set; }
        public IEnumerable<EngineLocation> EngineLocations { get; private set; } 
        public IEnumerable<ValvesArrangement> ValvesArrangements { get; private set; }

        public SettingsModel(
            IEnumerable<BatteryType> batteryTypes,
            IEnumerable<BodyType> bodyTypes,
            IEnumerable<FuelType> fuelTypes,
            IEnumerable<TireCarcassType> tireCarcassTypes,
            IEnumerable<TransmissionType> transmissionTypes,
            IEnumerable<EngineLocation> engineLocations,
            IEnumerable<ValvesArrangement> valvesArrangements)
        {
            BatteryTypes = batteryTypes;
            BodyTypes = bodyTypes;
            FuelTypes = fuelTypes;
            TireCarcassTypes = tireCarcassTypes;
            TransmissionTypes = transmissionTypes;
            EngineLocations = engineLocations;
            ValvesArrangements = valvesArrangements;
        }
    }
}