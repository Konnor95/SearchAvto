namespace SearchAvto.Models.DataModels
{
    public partial class ElectricEngine
    {
        public bool HasBattery
        {
            get { return BatteryTypeId != null && BatteryNominalVoltage!=null; }
        }

        public bool HasFirstMotor
        {
            get { return ElectricMotor != null; }
        }

        public bool HasSecondMotor
        {
            get { return ElectricMotor1 != null; }
        }

        public bool HasAnything
        {
            get { return HasBattery || HasFirstMotor || HasSecondMotor; }
        }
    }
}