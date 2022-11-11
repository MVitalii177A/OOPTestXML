using System;
using System.Collections.Generic;

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
                Console.WriteLine(((IShowInfo)v).ShowInfo());
                Console.WriteLine("\n");
            });

            Console.ReadLine();
        }

        private static List<Scooter> GetScooters()
        {
            return new List<Scooter>
            {
                new Scooter
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Electric,
                        Power = 50,
                        Volume = 0,
                        SerialNumber = "EL12345"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 100,
                        WheelsCount = 2,
                        SerialNumber = "SC12345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 0,
                        TransmissionType = TransmissionTypeEnum.CVT,
                        Manufacturer = "Jatco"
                    }
                },
            };
        }

        private static List<Bus> GetBuses()
        {
            return new List<Bus>
            {
                new Bus
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Diesel,
                        Power = 200,
                        Volume = 3,
                        SerialNumber = "D12345"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 5000,
                        WheelsCount = 6,
                        SerialNumber = "B12345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 6,
                        TransmissionType = TransmissionTypeEnum.Automatic,
                        Manufacturer = "Ford"
                    },

                    PassengerSeatCount = 15,
                    PassengerStandCount = 25
                },
            };
        }

        private static List<Cargo> GetCargos()
        {
            return new List<Cargo>
            {
                new Cargo
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Petrol,
                        Volume = 6,
                        Power = 425,
                        SerialNumber = "MAN12345"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 12000,
                        WheelsCount = 8,
                        SerialNumber = "CH12345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 6,
                        TransmissionType = TransmissionTypeEnum.Manual,
                        Manufacturer = "GM"
                    },

                    CargoType = CargoTypeEnum.Tipper
                }
            };
        }

        private static List<PassengerCar> GetCars()
        {
            return new List<PassengerCar>
            {
                new PassengerCar
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Diesel,
                        Power = 125,
                        Volume = 1.5M,
                        SerialNumber = "DS12345"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 1200,
                        WheelsCount = 4,
                        SerialNumber = "F12345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 5,
                        Manufacturer = "Jatco",
                        TransmissionType = TransmissionTypeEnum.Automatic
                    },

                    DriveUnit = DriveUnitEnum.RearWheelDrive

                    
                },
            };
        }
    }
}
