using OOPTest.Enums;
using System.Xml.Serialization;

namespace OOPTest.Classes
{
    [Serializable]
    public sealed class Engine
    {
        [XmlElement]
        public EngineTypeEnum EngineType { get; set; }

        [XmlAttribute]
        public string SerialNumber { get; set; }
        
        [XmlAttribute]
        public decimal Volume { get; set; }

        [XmlAttribute]
        public decimal Power { get; set; }

        public string GetInfo()
        {
            return string.Format("\n\tSerialNumber={0}, \n\tEngineType={1}, \n\tVolume={2}, \n\tPower={3}", SerialNumber, EngineType.ToString(), Volume, Power);
        }
    }
}
