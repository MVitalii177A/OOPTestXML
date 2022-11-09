using OOPTest.Classes;
using OOPTest.Enums;

namespace OOPTest.Entities
{
    public class Cargo : Vehicle
    {
        public CargoTypeEnum CargoType { get; set; }

        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), "CargoType={0}", CargoType.ToString());
        }
    }
}
