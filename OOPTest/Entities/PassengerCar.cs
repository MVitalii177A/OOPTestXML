using OOPTest.Classes;
using OOPTest.Enums;
using OOPTest.Interfaces;
using System.Xml.Serialization;

namespace OOPTest.Entities
{
    [Serializable]  
    public class PassengerCar : Vehicle, IShowInfo
    {
        [XmlIgnore]
        public DriveUnitEnum DriveUnit { get; set; }
        
        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), "\nDriveUnit \n\tDriveUnit=", DriveUnit);
        }
    }
}
