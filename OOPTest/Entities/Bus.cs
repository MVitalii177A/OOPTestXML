using OOPTest.Classes;
using OOPTest.Enums;
using OOPTest.Interfaces;

namespace OOPTest.Entities
{
    public class Bus : Vehicle, IShowInfo
    {
        public int PassengerSeatCount { get; set; }

        public int PassengerStandCount { get; set; }

        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), string.Format("PassengerSeatCount={0}, PassengerStandCount={1}", PassengerSeatCount, PassengerStandCount));
        }
    }
}
