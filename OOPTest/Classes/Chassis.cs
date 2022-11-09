using OOPTest.Enums;

namespace OOPTest.Classes
{
    public sealed class Chassis
    {
        public string SerialNumber { get; set; }

        public int WheelsCount { get; set; }

        public decimal WeightLoad { get; set; }

        public string GetInfo()
        {
            return string.Format("SerialNumber={0}, WheelsCount={1}, WeightLoad={2}", SerialNumber, WheelsCount, WeightLoad);
        }
    }
}
