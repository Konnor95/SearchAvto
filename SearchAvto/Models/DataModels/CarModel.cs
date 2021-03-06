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
    
    public partial class CarModel
    {
        public CarModel()
        {
            this.Modifications = new HashSet<Modification>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> ProductionStarted { get; set; }
        public Nullable<System.DateTime> ProductionEnded { get; set; }
        public int ModelLineId { get; set; }
        public Nullable<int> BodyTypeId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public byte TColor { get; set; }
        public byte TMainFeature { get; set; }
        public byte TEngine { get; set; }
        public byte TAge { get; set; }
        public byte TSpeed { get; set; }
        public byte TSize { get; set; }
        public byte TOutLand { get; set; }
        public byte TPassengers { get; set; }
        public byte TDistance { get; set; }
        public byte TPrice { get; set; }
    
        public virtual BodyType BodyType { get; set; }
        public virtual ModelLine ModelLine { get; set; }
        public virtual ICollection<Modification> Modifications { get; set; }
    }
}
