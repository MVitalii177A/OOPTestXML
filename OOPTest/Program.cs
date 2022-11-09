using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOPTest.Classes;
using OOPTest.Entities;
using OOPTest.Enums;
using OOPTest.Interfaces;

namespace OOPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            
            vehicles.AddRange(GetCars());
            vehicles.AddRange(GetCargos());
            vehicles.AddRange(GetBuses());
            vehicles.AddRange(GetScooters());

            vehicles.ForEach(v =>
            {
                Console.WriteLine(v.GetType().Name);
                Console.WriteLine(((IShowInfo)v).IShowInfo());
            });

            Console.ReadLine();
        }
        
    }
}
