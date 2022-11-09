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
        return string.Format("SerialNumber={0}, EngineType={1}, Volume={2}, Power={3}", SerialNumber, EngineType.ToString(), Volume, Power);
        }
    }
}
