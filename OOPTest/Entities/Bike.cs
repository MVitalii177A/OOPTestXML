using OOPTest.Classes;
using OOPTest.Interfaces;

namespace OOPTest.Entities
{
    [Serializable]
    public class Bike : Vehicle, IShowInfo
    {
        public bool HasCradle { get; set; }

        public string ShowInfo()
        {
            return string.Concat(ShowCommonInfo(), string.Format("\n\tHasCradle={0}", HasCradle));
        }
    }
}
