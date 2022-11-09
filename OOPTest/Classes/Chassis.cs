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
            return string.Format("\n\tSerialNumber={0}, \n\tWheelsCount={1}, \n\tWeightLoad={2}", SerialNumber, WheelsCount, WeightLoad);
        }
    }
}
