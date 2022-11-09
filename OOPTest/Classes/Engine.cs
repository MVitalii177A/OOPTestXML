using OOPTest.Enums;

namespace OOPTest.Classes
{
    public sealed class Engine
    {
        public EngineTypeEnum EngineType { get; set; }

        public string SerialNumber { get; set; }

        public decimal Volume { get; set; }

        public decimal Power { get; set; }

        public string GetInfo()
        {
            return string.Format("\n\tSerialNumber={0}, \n\tEngineType={1}, \n\tVolume={2}, \n\tPower={3}", SerialNumber, EngineType.ToString(), Volume, Power);
        }
    }
}
