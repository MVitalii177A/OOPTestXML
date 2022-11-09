using OOPTest.Enums;

namespace OOPTest.Classes
{
    public sealed class Transmission
    {
        public TransmissionTypeEnum TransmissionType { get; set; }

        public byte GearsCount { get; set; }

        public string Manufacturer { get; set; }

        public string GetInfo()
        {
            return string.Format("\n\tTransmissionType={0}, \n\tGearsCount={1}, \n\tManufacturer={2}", TransmissionType.ToString(), GearsCount, Manufacturer);
        }
    }
}
