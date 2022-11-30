using OOPTest.Entities;
using OOPTest.Enums;
using System.Xml.Serialization;

namespace OOPTest.Classes
{
    [Serializable]
    [XmlInclude(typeof(Bus)),
    XmlInclude(typeof(Cargo)),
    XmlInclude(typeof(PassengerCar)),
    XmlInclude(typeof(Scooter)),
    XmlInclude(typeof(Bike)),
    XmlType]
    public abstract class Vehicle
    {
        [XmlElement]
        public Engine Engine { get; set; }

        [XmlElement]
        public Chassis Chassis { get; set; }

        [XmlElement]
        public Transmission Transmission { get; set; }

        public string ShowCommonInfo()
        {
            return string.Concat(
                "------------\nEngine", Engine != null ? Engine.GetInfo() : " null",
                "\nChassis", Chassis != null ? Chassis.GetInfo() : " null",
                "\nTransmission", Transmission != null ? Transmission.GetInfo() : " null");
        }

    }
}
