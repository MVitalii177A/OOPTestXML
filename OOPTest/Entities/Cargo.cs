using OOPTest.Classes;
using OOPTest.Enums;
using OOPTest.Interfaces;

namespace OOPTest.Entities
{
    public class Cargo : Vehicle, IShowInfo
    {
        public CargoTypeEnum CargoType { get; set; }

        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), String.Format("\nCargoType \n\tCargoType={0}", CargoType.ToString()));
        }
    }
}
