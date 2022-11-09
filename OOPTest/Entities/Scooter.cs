using OOPTest.Classes;
using OOPTest.Interfaces;

namespace OOPTest.Entities
{
    public class Scooter : Vehicle, IShowInfo
    {
        public decimal MaxSpeed { get; set; }

        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), string.Format("MaxSpeed={0}", MaxSpeed));
        }
    }
}
