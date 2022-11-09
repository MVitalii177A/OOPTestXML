using OOPTest.Enums;

namespace OOPTest.Classes
{
    public abstract class Vehicle
    {
        public Engine Engine { get; set; }

        public Chassis Chassis { get; set; }

        public Transmission Transmission { get; set; }

        public string ShowCommonInfo()
        {
            return string.Concat("------------\nEngine", Engine.GetInfo(), "\nChassis", Chassis.GetInfo(), "\nTransmission", Transmission.GetInfo());
        }

    }
}
