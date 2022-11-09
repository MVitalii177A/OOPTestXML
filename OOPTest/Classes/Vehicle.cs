namespace OOPTest.Classes
{
    public abstract class Vehicle
    {
        public Engine Engine { get; set; }

        public Chassis Chassis { get; set; }

        public Transmission Transmission { get; set; }

        public string ShowCommonInfo()
        {
            return string.Concat("Engine", Engine.GetInfo(), "Chassis", Chassis.GetInfo(), "Transmission", Transmission.GetInfo());
        }

    }
}
