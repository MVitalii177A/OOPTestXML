using OOPTest.Classes;
using OOPTest.Enums;
using OOPTest.Interfaces;

namespace OOPTest.Entities
{
    public class PassengerCar : Vehicle, IShowInfo
    {
        public DriveUnitEnum DriveUnit { get; set; }
        
        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), "\nDriveUnit \n\tDriveUnit=", DriveUnit);
        }
    }
}
