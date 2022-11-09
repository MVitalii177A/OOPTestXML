using OOPTest.Classes;
using OOPTest.Interfaces;
using OOPTest.Enums;

namespace OOPTest.Entities
{
    public class Bus : Vehicle, IShowInfo
    {
        public int PassengerSeatCount { get; set; }

        public int PassengerStandCount { get; set; }

        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), string.Format("\n\tPassengerSeatCount={0}, \n\tPassengerStandCount={1}", PassengerSeatCount, PassengerStandCount));
        }
    }
}
