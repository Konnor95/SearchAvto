using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using SearchAvto.Models.DataModels;
using SearchAvto.Models.LogicModels;

namespace SearchAvto.Controllers
{
    public class DataController : Controller
    {
        class SElectricMotor
        {
            public int? Power { get; set; }
            public int? Torque { get; set; }

            public SElectricMotor(ElectricMotor electricMotor)
            {
                if (electricMotor == null)
                    return;
                Power = electricMotor.Power;
                Torque = electricMotor.Torque;
            }
        }
        class SElectricEngine
        {
            public string BatteryType { get; set; }
            public SElectricMotor ElectricMotor1 { get; set; }
            public SElectricMotor ElectricMotor2 { get; set; }

            public SElectricEngine(ElectricEngine engine)
            {
                if (engine == null)
                    return;
                if (engine.BatteryType != null)
                    BatteryType = engine.BatteryType.Name;
                ElectricMotor1 = new SElectricMotor(engine.ElectricMotor);
                ElectricMotor2 = new SElectricMotor(engine.ElectricMotor1);
            }
        }
        class SInternalCombustionEngine
        {
            public string Location { get; set; }
            public int? Volume { get; set; }
            public int? NumberOfValves { get; set; }
            public string ValvesArrangement { get; set; }
            public string FuelType { get; set; }
            public int? Power { get; set; }
            public int? Torque { get; set; }
            public int? TorqueRotations { get; set; }
            public double? FuelConsumptionInCity { get; set; }
            public double? FuelConsumptionOnRoad { get; set; }
            public double? FuelConsumptionCombined { get; set; }
            public double? FuelTankVolume { get; set; }

            public SInternalCombustionEngine(InternalCombustionEngine engine)
            {
                if (engine == null)
                    return;
                if (engine.Location != null)
                    Location = engine.EngineLocation.Name;
                Volume = engine.Volume;
                NumberOfValves = engine.NumberOfValves;
                if (engine.ValvesArrangementId != null)
                    ValvesArrangement = engine.ValvesArrangement.Name;
                if (engine.FuelTypeId != null)
                    FuelType = engine.FuelType.Name;
                Power = engine.Power;
                Torque = engine.Torque;
                TorqueRotations = engine.TorqueRotations;
                FuelConsumptionInCity = engine.FuelConsumptionInCity;
                FuelConsumptionOnRoad = engine.FuelConsumptionOnRoad;
                FuelConsumptionCombined = engine.FuelConsumptionCombined;
                FuelTankVolume = engine.FuelTankVolume;
            }
        }
        class SModification
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Model { get; set; }
            public string ModelLine { get; set; }
            public string Brand { get; set; }
            public string FullName { get; set; }
            public int? MaxSpeed { get; set; }
            public double? Velocity { get; set; }
            public byte? NumberOfSeats { get; set; }
            public int? Length { get; set; }
            public int? Height { get; set; }
            public int? Width { get; set; }
            public int? CurbWeight { get; set; }
            public int? TotalWeight { get; set; }
            public int? CargoVolume { get; set; }
            public int? MaxCargoVolume { get; set; }
            public int? Wheelbase { get; set; }
            public double? FrontTrackWidth { get; set; }
            public double? BackTrackWidth { get; set; }
            public SInternalCombustionEngine InternalCombustionEngine { get; set; }
            public SElectricEngine ElectricEngine { get; set; }
            public string TransmissionType { get; set; }
            public int? TransmissionStages { get; set; }
            public string Drive { get; set; }
            public string Wheels { get; set; }
            public string Tires { get; set; }

            public SModification(Modification modification)
            {
                if (modification == null)
                    return;
                Id = modification.Id;
                Name = modification.Name;
                Model = modification.CarModel.Name;
                ModelLine = modification.CarModel.ModelLine.Name;
                Brand = modification.CarModel.ModelLine.Brand.Name;
                FullName = modification.FullName;
                MaxSpeed = modification.MaxSpeed;
                Velocity = modification.Velocity;
                NumberOfSeats = modification.NumberOfSeats;
                Length = modification.Length;
                Height = modification.Height;
                Width = modification.Width;
                CurbWeight = modification.CurbWeight;
                TotalWeight = modification.TotalWeight;
                CargoVolume = modification.CargoVolume;
                MaxCargoVolume = modification.MaxCargoVolume;
                Wheelbase = modification.Wheelbase;
                FrontTrackWidth = modification.FrontTrackWidth;
                BackTrackWidth = modification.BackTrackWidth;
                ElectricEngine = new SElectricEngine(modification.ElectricEngine);
                InternalCombustionEngine = new SInternalCombustionEngine(modification.InternalCombustionEngine);
                if (modification.TransmissionTypeId != null)
                    TransmissionType = modification.TransmissionType.Name;
                TransmissionStages = modification.TransmissionStages;
                Drive = modification.DriveName;
                Wheels = modification.WheelFormula;
                Tires = modification.TireFormula;
            }
        }

        private string ToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private object Cast(Brand brand)
        {
            return new { brand.Id, brand.Name };
        }

        private IEnumerable Cast(IEnumerable<Brand> brands)
        {
            return brands.Select(Cast);
        }

        private object Cast(ModelLine modelLine)
        {
            return new { modelLine.Id, modelLine.Name, Brand = modelLine.Brand.Name, modelLine.FullName };
        }

        private IEnumerable Cast(IEnumerable<ModelLine> modelLines)
        {
            return modelLines.Select(Cast);
        }

        private object Cast(CarModel model)
        {
            return new
            {
                model.Id,
                model.Name,
                ModelLine = model.ModelLine.Name,
                Brand = model.ModelLine.Brand.Name,
                model.FullName,
                model.StartYearOfProduction,
                model.EndYearOfProduction,
                BodyType = model.BodyType == null ? null : model.BodyType.Name
            };
        }

        private IEnumerable Cast(IEnumerable<CarModel> models)
        {
            return models.Select(Cast);
        }

        private SModification Cast(Modification m)
        {
            return new SModification(m);
        }

        private IEnumerable<SModification> Cast(IEnumerable<Modification> modifications)
        {
            return modifications.Select(Cast);
        }

        public string Brands()
        {
            return ToJson(Cast(DataManager.Cars.Brands()));
        }

        public string Brand(int id)
        {
            return ToJson(Cast(DataManager.Cars.GetBrand(id)));
        }

        public string ModelLines()
        {
            return ToJson(Cast(DataManager.Cars.ModelLines()));
        }

        public string ModelLine(int id)
        {
            return ToJson(Cast(DataManager.Cars.GetModelLine(id)));
        }

        public string Models()
        {
            return ToJson(Cast(DataManager.Cars.CarModels()));
        }

        public string Model(int id)
        {
            return ToJson(Cast(DataManager.Cars.GetCarModel(id)));
        }

        public string Modifications()
        {
            return ToJson(Cast(DataManager.Cars.Modifications()));
        }

        public string Modification(int id)
        {
            return ToJson(Cast(DataManager.Cars.GetModification(id)));
        }
    }
}