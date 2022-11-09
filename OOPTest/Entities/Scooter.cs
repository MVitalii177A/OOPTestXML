using OOPTest.Classes;
using OOPTest.Enums;
using OOPTest.Interfaces;

namespace OOPTest.Entities
{
    public class Scooter : Vehicle
    {
        public decimal MaxSpeed { get; set; }

        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), string.Format("MaxSpeed={0}", MaxSpeed));
        }
    }
}
