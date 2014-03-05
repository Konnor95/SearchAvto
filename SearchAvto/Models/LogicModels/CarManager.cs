using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchAvto.Models.DataModels;
using SearchAvto.Models.ViewModels;

namespace SearchAvto.Models.LogicModels
{
    public class CarManager : Manager
    {
        public CarManager(DatabaseEntities data)
            : base(data)
        {
        }

        public IEnumerable<Brand> Brands()
        {
            return Data.Brands;
        }

        public Brand GetBrand(int id)
        {
            return Data.Brands.FirstOrDefault(x => x.Id == id);
        }

        public Brand GetBrand(string name)
        {
            return Data.Brands.FirstOrDefault(x => x.Name == name);
        }

        public Brand GetBrand(string name, int currentBrandId)
        {
            return Data.Brands.FirstOrDefault(x => x.Name == name && x.Id != currentBrandId);
        }

        public ProcessResult AddBrand(string name, HttpPostedFileBase imageUpload, HttpServerUtilityBase server)
        {
            if (String.IsNullOrEmpty(name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            Brand b = GetBrand(name);
            if (b != null)
            {
                return ProcessResults.SuchBrandExists;
            }
            var newBrand = new Brand { Name = name };
            b = Data.Brands.Add(newBrand);
            Data.SaveChanges();
            if (imageUpload != null)
            {
                if (imageUpload.ContentLength <= 0 || !SecurityManager.IsImage(imageUpload))
                {
                    return ProcessResults.InvalidImageFormat;
                }
                b.Image = SaveImage(b.Id, StaticSettings.BrandsUploadFolderPath, imageUpload, server);
                Data.SaveChanges();
            }
            ProcessResult result = ProcessResults.BrandAdded;
            result.AffectedObjectId = b.Id;
            return result;
        }

        public ProcessResult EditBrand(int id, string name, HttpPostedFileBase imageUpload, bool deleteImage, HttpServerUtilityBase server)
        {
            Brand b = GetBrand(id);
            if (String.IsNullOrEmpty(name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            if (b == null)
            {
                return ProcessResults.NoSuchBrand;
            }
            if (GetBrand(name, b.Id) != null)
            {
                return ProcessResults.SuchBrandExists;
            }
            b.Name = name;
            if (deleteImage)
            {
                DeleteImage(b.Image, server);
                b.Image = null;

            }
            else if (imageUpload != null)
            {
                if (imageUpload.ContentLength <= 0 || !SecurityManager.IsImage(imageUpload))
                {
                    return ProcessResults.InvalidImageFormat;
                }
                b.Image = SaveImage(b.Id, StaticSettings.BrandsUploadFolderPath, imageUpload, server);
                Data.SaveChanges();
            }
            Data.SaveChanges();
            return ProcessResults.BrandChanged;
        }

        public ProcessResult DeleteBrand(int id, HttpServerUtilityBase server)
        {
            Brand b = GetBrand(id);
            if (b == null)
            {
                return ProcessResults.NoSuchBrand;
            }
            string img = b.Image;
            Data.Brands.Remove(b);
            Data.SaveChanges();
            DeleteImage(img, server);
            return ProcessResults.BrandDeleted;
        }

        public ProcessResult AddModelLine(int brandId, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            Brand b = GetBrand(brandId);
            if (b == null)
            {
                return ProcessResults.NoSuchBrand;
            }
            if (ModelLines(b.Id, name) != null)
            {
                return ProcessResults.SuchModelLineExists;
            }
            var newModelLine = new ModelLine { BrandId = brandId, Name = name };
            ModelLine m = Data.ModelLines.Add(newModelLine);
            Data.SaveChanges();
            ProcessResult result = ProcessResults.ModelLineAdded;
            result.AffectedObjectId = m.Id;
            return result;
        }

        public ProcessResult EditModelLine(int modelLineId, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return ProcessResults.TitleCannotBeEmpty;
            }
            ModelLine m = GetModelLine(modelLineId);
            if (m == null)
            {
                return ProcessResults.NoSuchModelLine;
            }
            if (ModelLines(m.BrandId, name, m.Id) != null)
            {
                return ProcessResults.SuchModelLineExists;
            }
            m.Name = name;
            Data.SaveChanges();
            return ProcessResults.ModelLineChanged;
        }

        public ProcessResult DeleteModelLine(int id)
        {
            ModelLine m = GetModelLine(id);
            if (m == null)
            {
                return ProcessResults.NoSuchModelLine;
            }
            int brandId = m.BrandId;
            Data.ModelLines.Remove(m);
            Data.SaveChanges();
            ProcessResult result = ProcessResults.ModelLineDeleted;
            result.AffectedObjectId = brandId;
            return result;
        }

        public IEnumerable<ModelLine> ModelLines()
        {
            return Data.ModelLines;
        }

        public IEnumerable<ModelLine> ModelLines(int brandId)
        {
            return Data.ModelLines.Where(x => x.BrandId == brandId);
        }

        public ModelLine ModelLines(int brandId, string name)
        {
            return Data.ModelLines.FirstOrDefault(x => x.BrandId == brandId && x.Name == name);
        }

        public ModelLine ModelLines(int brandId, string name, int currentModelLineId)
        {
            return
                Data.ModelLines.FirstOrDefault(x => x.BrandId == brandId && x.Name == name && x.Id != currentModelLineId);
        }

        public ModelLine GetModelLine(int modelLineId)
        {
            return Data.ModelLines.FirstOrDefault(x => x.Id == modelLineId);
        }

        public IEnumerable<CarModel> CarModels()
        {
            return Data.CarModels;
        }

        public IEnumerable<CarModel> CarModels(int modelLineId)
        {
            return Data.CarModels.Where(x => x.ModelLineId == modelLineId);
        }

        public CarModel GetCarModel(int id)
        {
            return Data.CarModels.FirstOrDefault(x => x.Id == id);
        }

        /* public CarModel GetCarModel(string name,int? start,int? end)
         {
             if(start.HasValue&&end.HasValue)
             return Data.CarModels.FirstOrDefault(x => x.Name == name && (x.ProductionStarted.HasValue && x.ProductionStarted.Value.Year == start) && (x.ProductionEnded.HasValue && x.ProductionEnded.Value.Year == end));
             if (start.HasValue)
                 return Data.CarModels.FirstOrDefault(x => x.Name == name && (x.ProductionStarted.HasValue && x.ProductionStarted.Value.Year == start));
             if (end.HasValue)
                 return
                     Data.CarModels.FirstOrDefault(
                         x => x.Name == name && (x.ProductionEnded.HasValue && x.ProductionEnded.Value.Year == end));
             return Data.CarModels.FirstOrDefault(x => x.Name == name);
         }*/

        public ProcessResult AddCarModel(int modelLineId, string name, int type, int? startYear, int? endYear,
            HttpPostedFileBase imageUpload, HttpServerUtilityBase server)
        {
            ModelLine modelLine = GetModelLine(modelLineId);
            if (modelLine == null)
            {
                return ProcessResults.NoSuchModelLine;
            }
            /* if (GetCarModel(name, startYear, endYear) != null)
             {
                 return ProcessResults.SuchModelExists;
             }*/
            if (GetBodyType(type) == null)
            {
                return ProcessResults.NoSuchBodyType;
            }
            var newModel = new CarModel
            {
                ModelLineId = modelLineId,
                Name = name,
                BodyTypeId = type,
                ProductionStarted = GetYear(startYear),
                ProductionEnded = GetYear(endYear)
            };
            newModel = Data.CarModels.Add(newModel);
            Data.SaveChanges();
            if (imageUpload != null)
            {
                if (imageUpload.ContentLength <= 0 || !SecurityManager.IsImage(imageUpload))
                {
                    return ProcessResults.InvalidImageFormat;
                }
                newModel.Image = SaveImage(newModel.Id, StaticSettings.BrandsUploadFolderPath, imageUpload, server);
                Data.SaveChanges();
            }
            ProcessResult result = ProcessResults.BrandAdded;
            result.AffectedObjectId = newModel.Id;
            return result;
        }

        public ProcessResult EditCarModel(int id, string name, int type, int? startYear, int? endYear,
            HttpPostedFileBase imageUpload, bool deleteImage, HttpServerUtilityBase server)
        {
            CarModel model = GetCarModel(id);
            if (model == null)
            {
                return ProcessResults.NoSuchModel;
            }
            /*if (GetCarModel(name, startYear, endYear) != null)
            {
                 return ProcessResults.SuchModelExists;
            }*/
            if (GetBodyType(type) == null)
            {
                return ProcessResults.NoSuchBodyType;
            }
            model.Name = name;
            model.BodyTypeId = type;
            model.ProductionStarted = GetYear(startYear);
            model.ProductionEnded = GetYear(endYear);
            Data.SaveChanges();
            if (deleteImage)
            {
                DeleteImage(model.Image, server);
                model.Image = null;

            }
            else if (imageUpload != null)
            {
                if (imageUpload.ContentLength <= 0 || !SecurityManager.IsImage(imageUpload))
                {
                    return ProcessResults.InvalidImageFormat;
                }
                model.Image = SaveImage(model.Id, StaticSettings.BrandsUploadFolderPath, imageUpload, server);
                Data.SaveChanges();
            }
            return ProcessResults.ModelChanged;
        }

        public ProcessResult DeleteCarModel(int id, HttpServerUtilityBase server)
        {
            CarModel b = GetCarModel(id);
            if (b == null)
            {
                return ProcessResults.NoSuchModel;
            }
            int modelLineId = b.ModelLineId;
            string img = b.Image;
            Data.CarModels.Remove(b);
            Data.SaveChanges();
            DeleteImage(img, server);
            ProcessResult result = ProcessResults.ModelDeleted;
            result.AffectedObjectId = modelLineId;
            return result;
        }

        public IEnumerable<Modification> Modifications()
        {
            return Data.Modifications;
        }

        public IEnumerable<Modification> Modifications(int modelId)
        {
            return Data.Modifications.Where(x => x.ModelId == modelId);
        }

        public Modification GetModification(int modificationId)
        {
            return Data.Modifications.FirstOrDefault(x => x.Id == modificationId);
        }

        public ProcessResult AddModification(Modification mod, int ice, int ee)
        {
            CarModel model = GetCarModel(mod.ModelId);
            if (model == null)
            {
                return ProcessResults.NoSuchModel;
            }
            if (ice == 0)
            {
                mod.InternalCombustionEngineId = null;
                mod.InternalCombustionEngine = null;
            }
            else if (ice == 1)
            {
                InternalCombustionEngine i = mod.InternalCombustionEngine;
                if (i.Location <= 0) i.Location = null;
                if (i.FuelTypeId <= 0) i.FuelTypeId = null;
                if (i.ValvesArrangementId <= 0) i.ValvesArrangementId = null;
                i = Data.InternalCombustionEngines.Add(i);
                Data.SaveChanges();
                mod.InternalCombustionEngineId = i.Id;
            }
            if (ee == 0)
            {
                mod.ElectricEngine = null;
                mod.ElectricEngineId = null;
            }
            else if (ee == 1)
            {
                ElectricEngine e = mod.ElectricEngine;
                if (e.BatteryTypeId <= 0) e.BatteryTypeId = null;
                e = Data.ElectricEngines.Add(e);
                Data.SaveChanges();
                mod.ElectricEngineId = e.Id;
            }
            if (mod.TransmissionTypeId <= 0) mod.TransmissionTypeId = null;
            if (mod.Drive <= 0) mod.Drive = null;
            if (mod.TireCarcassTypeId <= 0) mod.TireCarcassTypeId = null;
            mod = Data.Modifications.Add(mod);
            Data.SaveChanges();
            ProcessResult result = ProcessResults.ModificationAdded;
            result.AffectedObjectId = mod.Id;
            return result;
        }

        //Иправить двигатели
        public ProcessResult EditModification(Modification mod, int ice, int ee)
        {
            Modification m = GetModification(mod.Id);
            if (m == null) return ProcessResults.NoSuchModification;
            if (ice == 0)
            {
                m.InternalCombustionEngineId = null;
                m.InternalCombustionEngine = null;
            }
            else if (ice == 1)
            {
                InternalCombustionEngine i = mod.InternalCombustionEngine;
                if (i.Location <= 0) i.Location = null;
                if (i.FuelTypeId <= 0) i.FuelTypeId = null;
                if (i.ValvesArrangementId <= 0) i.ValvesArrangementId = null;
                if (m.InternalCombustionEngineId == null)
                {
                    i = Data.InternalCombustionEngines.Add(i);
                    Data.SaveChanges();
                    m.InternalCombustionEngineId = i.Id;
                }
                else
                {
                    m.InternalCombustionEngine = i;
                }
            }
            else
            {
                m.InternalCombustionEngineId = mod.InternalCombustionEngineId;
            }
            if (ee == 0)
            {
                m.ElectricEngine = null;
                m.ElectricEngineId = null;
            }
            else if (ee == 1)
            {
                ElectricEngine e = mod.ElectricEngine;
                if (e.BatteryTypeId <= 0) e.BatteryTypeId = null;
                if (m.ElectricEngineId == null)
                {
                    e = Data.ElectricEngines.Add(e);
                    Data.SaveChanges();
                    m.ElectricEngineId = e.Id;
                }
                else
                {
                    m.ElectricEngine = e;
                }
            }
            else
            {
                m.ElectricEngineId = mod.ElectricEngineId;
            }
            if (mod.TransmissionTypeId <= 0) mod.TransmissionTypeId = null;
            if (mod.Drive <= 0) mod.Drive = null;
            if (mod.TireCarcassTypeId <= 0) mod.TireCarcassTypeId = null;
            m.BackTrackWidth = mod.BackTrackWidth;
            m.CargoVolume = mod.CargoVolume;
            m.CurbWeight = mod.CurbWeight;
            m.Drive = mod.Drive;
            m.FrontTrackWidth = mod.FrontTrackWidth;
            m.Height = mod.Height;
            m.Length = mod.Length;
            m.MaxCargoVolume = mod.MaxCargoVolume;
            m.MaxSpeed = mod.MaxSpeed;
            m.Name = mod.Name;
            m.NumberOfDrivenWheels = mod.NumberOfDrivenWheels;
            m.NumberOfSeats = mod.NumberOfSeats;
            m.NumberOfWheels = mod.NumberOfWheels;
            m.TireCarcassTypeId = mod.TireCarcassTypeId;
            m.TireMountingDiameter = mod.TireMountingDiameter;
            m.TireProfileHeight = mod.TireProfileHeight;
            m.TireProfileWidth = mod.TireProfileWidth;
            m.TotalWeight = mod.TotalWeight;
            m.TransmissionStages = mod.TransmissionStages;
            m.TransmissionTypeId = mod.TransmissionTypeId;
            m.Velocity = mod.Velocity;
            m.Wheelbase = mod.Wheelbase;
            m.Width = mod.Width;
            Data.SaveChanges();
            return ProcessResults.ModificationChanged;
        }

        public ProcessResult DeleteModification(int id)
        {
            Modification m = GetModification(id);
            if (m == null)
            {
                return ProcessResults.NoSuchModification;
            }
            int model = m.ModelId;
            if (m.InternalCombustionEngineId != null &&
                Data.Modifications.FirstOrDefault(x => x.InternalCombustionEngineId == m.InternalCombustionEngineId) != null)
            {
                Data.InternalCombustionEngines.Remove(m.InternalCombustionEngine);
            }
            if (m.ElectricEngineId != null &&
                Data.Modifications.FirstOrDefault(x => x.ElectricEngineId == m.ElectricEngineId) != null)
            {
                Data.ElectricMotors.Remove(m.ElectricEngine.ElectricMotor);
                Data.ElectricMotors.Remove(m.ElectricEngine.ElectricMotor1);
                Data.ElectricEngines.Remove(m.ElectricEngine);
            }
            Data.Modifications.Remove(m);
            Data.SaveChanges();
            ProcessResult result = ProcessResults.ModificationDeleted;
            result.AffectedObjectId = model;
            return result;
        }


        public IEnumerable<BodyClass> BodyClasses
        {
            get
            {
                return Data.BodyClasses;
            }
        }


        public IEnumerable<EngineLocation> EngineLocations
        {
            get
            {
                return Data.EngineLocations;
            }
        }

        public IEnumerable<ValvesArrangement> ValvesArrangements
        {
            get
            {
                return Data.ValvesArrangements;
            }
        }

        public IEnumerable<FuelType> FuelTypes
        {
            get
            {
                return Data.FuelTypes;
            }
        }

        public IEnumerable<BatteryType> BatteryTypes
        {
            get
            {
                return Data.BatteryTypes;
            }
        }

        public IEnumerable<TransmissionType> TransmissionTypes
        {
            get
            {
                return Data.TransmissionTypes;
            }
        }

        public IEnumerable<TireCarcassType> TireCarcassTypes
        {
            get
            {
                return Data.TireCarcassTypes;
            }
        }
        public BodyType GetBodyType(int id)
        {
            return Data.BodyTypes.FirstOrDefault(x => x.Id == id);
        }

        private DateTime? GetYear(int? year)
        {
            if (!year.HasValue) return null;
            return new DateTime(year.Value, 1, 1);
        }

        public CarModel FindCarModel(TestModel results)
        {
            int max = 0;
            int id = 1;
            foreach (var model in Data.CarModels)
            {
                int counter = 0;
                if (results.Age > 0 && results.Age == model.TAge) ++counter;
                if (model.BodyTypeId.HasValue && results.BodyType > 0 && results.BodyType == (byte)model.BodyType.BodyClassId) ++counter;
                if (results.Color > 0 && results.Color == model.TColor) ++counter;
                if (results.Distance > 0 && results.Distance == model.TDistance) ++counter;
                if (results.Electric > 0 && results.Electric != 2 && results.Electric == model.TElectric) ++counter;
                if (results.MainFeature > 0 && results.MainFeature == model.TMainFeature) ++counter;
                if (results.NegativeTrait > 0 && results.NegativeTrait == model.TNegativeTrait) ++counter;
                if (results.OutLand > 0 && results.OutLand == model.TOutLand) ++counter;
                if (results.Passengers > 0 && results.Passengers == model.TPassengers) ++counter;
                if (results.PositiveTrait > 0 && results.PositiveTrait == model.TPositiveTrait) ++counter;
                if (results.Price > 0 && results.Price == model.TPrice) ++counter;
                if (results.Speed > 0 && results.Speed == model.TSpeed) ++counter;
                if (counter > max)
                {
                    max = counter;
                    id = model.Id;
                }
            }
            return GetCarModel(id);
        }
    }
}