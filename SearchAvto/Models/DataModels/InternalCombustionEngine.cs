//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SearchAvto.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class InternalCombustionEngine
    {
        public InternalCombustionEngine()
        {
            this.Modifications = new HashSet<Modification>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Location { get; set; }
        public Nullable<int> Volume { get; set; }
        public Nullable<int> NumberOfValves { get; set; }
        public Nullable<int> ValvesArrangementId { get; set; }
        public Nullable<int> FuelTypeId { get; set; }
        public Nullable<int> Power { get; set; }
        public Nullable<int> Torque { get; set; }
        public Nullable<int> TorqueRotations { get; set; }
        public Nullable<double> FuelConsumptionInCity { get; set; }
        public Nullable<double> FuelConsumptionOnRoad { get; set; }
        public Nullable<double> FuelConsumptionCombined { get; set; }
        public Nullable<double> FuelTankVolume { get; set; }
    
        public virtual EngineLocation EngineLocation { get; set; }
        public virtual FuelType FuelType { get; set; }
        public virtual ValvesArrangement ValvesArrangement { get; set; }
        public virtual ICollection<Modification> Modifications { get; set; }
    }
}
