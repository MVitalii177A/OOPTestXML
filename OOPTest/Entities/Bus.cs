using OOPTest.Classes;

namespace OOPTest.Entities
{
    public class Bus : Vehicle
    {
        public int PassengerSeatCount { get; set; }

        public int PassengerStandCount { get; set; }

        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), string.Format("PassengerSeatCount={0}, PassengerStandCount={1}", PassengerSeatCount, PassengerStandCount));
        }
    }
}
