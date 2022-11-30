using OOPTest.Enums;
using System.Xml.Serialization;

namespace OOPTest.Classes
{
    [Serializable]
    public sealed class Chassis
    {
        [XmlAttribute]
        public string SerialNumber { get; set; }

        [XmlAttribute]
        public int WheelsCount { get; set; }

        [XmlAttribute]
        public decimal WeightLoad { get; set; }

        public string GetInfo()
        {
            return string.Format("\n\tSerialNumber={0}, \n\tWheelsCount={1}, \n\tWeightLoad={2}", SerialNumber, WheelsCount, WeightLoad);
        }
    }
}
