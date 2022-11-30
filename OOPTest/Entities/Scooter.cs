using OOPTest.Classes;
using OOPTest.Interfaces;

namespace OOPTest.Entities
{
    [Serializable]
    public class Scooter : Vehicle, IShowInfo
    {
        public decimal MaxSpeed { get; set; }

        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), string.Format("\n\tMaxSpeed={0}", MaxSpeed));
        }
    }
}
