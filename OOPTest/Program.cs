using System.Xml.Linq;
using System.Xml.Serialization;
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
            vehicles.AddRange(GetBikes());
            
            //методы
            Task1(vehicles);
            Task2(vehicles);
            Task3(vehicles);

            vehicles.ForEach(v =>
            {
                Console.WriteLine(v.GetType().Name);
                Console.WriteLine(((IShowInfo)v).ShowInfo());
                Console.WriteLine("\n");
            });

            Console.ReadLine();
        }

        private static void Task1(List<Vehicle> vehicles)
        {
            //Фильтрация всех автомобилей по обьему двигателя
            List<Vehicle> v = vehicles.Where(X => X.Engine != null && X.Engine.Volume > 1.5M).ToList();

            //Создаем экземпляр штатного сериализатора для типа List<Vehicle>
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Vehicle>));
            //Создаем экземпляр класса потока записи в файл
            StreamWriter myWriter = new StreamWriter("MuscleCars.xml");
            //Сериализуем список в поток 
            mySerializer.Serialize(myWriter, v);
            //Закрываем поток записи файла
            myWriter.Close();

            //Создаем экземпляр класса XML документа
            XDocument xDoc = new XDocument();
            //Создаем корневую ноду документа
            XElement root = new XElement("vehicles");
            
            //Перебор элементов списка
            foreach(var item in v)
            {   
                //Добавление сериализованных данных в корневую ноду документа
                root.Add(SerializeVehicle(item));
            }

            //Добавление корневой ноды в документ
            xDoc.Add(root);
            //Запись документа в файл
            xDoc.Save("MuscleCars2.xml");
        }

        private static void Task2(List<Vehicle> vehicles)
        {
            //Фильтрация по типу автомобиля
            List<Vehicle> v2 = vehicles.Where(X => X is Bus || X is Cargo).ToList();

            XDocument xDoc = new XDocument();
            XElement root = new XElement("engines");

            foreach (Vehicle busOrCargo in v2)
            {
                Engine engine = busOrCargo.Engine;

                //Создаем XML элемент для сериализации двигателя
                XElement xElement = new XElement("engine");  
                XAttribute engineType = new XAttribute("EngineType", engine.EngineType.ToString());
                XAttribute serialNumber = new XAttribute("SerialNumber", engine.SerialNumber);
                XAttribute power = new XAttribute("Power", engine.Power);
                xElement.Add(engineType, serialNumber, power);
                root.Add(xElement);
            }

            xDoc.Add(root);
            xDoc.Save("Engines.xml");
        }

        private static void Task3(List<Vehicle> vehicles)
        {
            //Групировка автомобилей по типу трансмиссии с предварительной фильтрацией плохо заполненных экземпляров класса
            IEnumerable<IGrouping<TransmissionTypeEnum, Vehicle>> transmissionGroups =
                vehicles.Where(X => X.Transmission != null).GroupBy(X => X.Transmission.TransmissionType);

            XDocument xDoc2 = new XDocument();
            XElement root2 = new XElement("groups");

            foreach (IGrouping<TransmissionTypeEnum, Vehicle> group in transmissionGroups)
            {
                XElement xGroup = new XElement("group");
                XAttribute attr = new XAttribute("TransmissionType", group.Key.ToString());
                xGroup.Add(attr);

                XElement xVehicles = new XElement("vehicles");

                foreach (Vehicle item in group)
                {                          
                    xVehicles.Add(SerializeVehicle(item));
                }

                xGroup.Add(xVehicles);
                root2.Add(xGroup);
            }

            xDoc2.Add(root2);
            xDoc2.Save("Groups.xml");
        }

        //Кастомизированная сериализация автомобиля
        private static XElement SerializeVehicle(Vehicle item)
        {
            XElement xVehicle = new XElement("vehicle");

            //Сериализуем имя класса
            xVehicle.Add(new XAttribute("ClassName", item.GetType()));

            //Сериализуем уникальные свойства автомобиля любого класса
            SerializeAnyVehicle(xVehicle, item);

            XElement xEngine = new XElement("Engine");
            Engine engine = item.Engine;
            if (engine != null)
            {               
                xEngine.Add(new XAttribute("EngineType", engine.EngineType.ToString()));
                xEngine.Add(new XAttribute("SerialNumber", engine.SerialNumber ?? "null"));
                xEngine.Add(new XAttribute("Volume", engine.Volume));
                xEngine.Add(new XAttribute("Power", engine.Power));
            }

            XElement xChassis = new XElement("Chassis");
            Chassis chassis = item.Chassis;
            if (chassis != null)
            {               
                xChassis.Add(new XAttribute("SerialNumber", chassis.SerialNumber ?? "null")); //??-Защита от обращения к null
                xChassis.Add(new XAttribute("WheelsCount", chassis.WheelsCount));
                xChassis.Add(new XAttribute("WeightLoad", chassis.WeightLoad));
            }

            XElement xTransmission = new XElement("Transmission");
            Transmission transmission = item.Transmission;
            if (transmission != null)
            {               
                xTransmission.Add(new XAttribute("TransmissionType", transmission.TransmissionType.ToString()));
                xTransmission.Add(new XAttribute("GearsCount", transmission.GearsCount));
                xTransmission.Add(new XAttribute("Manufacturer", transmission.Manufacturer ?? "null"));
            }

            xVehicle.Add(xEngine, xChassis, xTransmission);

            return xVehicle;
        }

        private static void SerializeAnyVehicle(XElement xVehicle, Vehicle item)
        {
            //Проверем к какому классу принадлежит экземпляр чтобы корректно сериализовать его уникальные свойства
            if(item is Bus)
            {
                SerializeBus(xVehicle, (Bus)item);
            }
            else if(item is Cargo)
            {
                SerializeCargo(xVehicle, (Cargo)item);
            }
            else if(item is PassengerCar)
            {
                SerializePassengerCar(xVehicle, (PassengerCar)item);
            }
            else if(item is Scooter)
            {
                SerializeScooter(xVehicle, (Scooter)item);
            }
            else
            {
                //Сообщение о невозможности сериализовать объект
                xVehicle.Add(new XAttribute("Error", "Can't Serialize Attribute"));
            }
        }

        //Сюда можно добавить новый метод сериализации уникальных свойств байка,
        //который можно будет вызвать в методе "SerializeAnyVehicle"

        private static void SerializeBus(XElement xVehicle, Bus item)
        {
            xVehicle.Add(new XAttribute("PassengerSeatCount", item.PassengerSeatCount));
            xVehicle.Add(new XAttribute("PassengerStandCount", item.PassengerStandCount));
        }

        private static void SerializeCargo(XElement xVehicle, Cargo item)
        {
            xVehicle.Add(new XAttribute("CargoType", item.CargoType.ToString()));
        }

        private static void SerializePassengerCar(XElement xVehicle, PassengerCar item)
        {
            xVehicle.Add(new XAttribute("DriveUnit", item.DriveUnit.ToString()));
        }

        private static void SerializeScooter(XElement xVehicle, Scooter item)
        {
            xVehicle.Add(new XAttribute("MaxSpeed", item.MaxSpeed));
        }

        
        #region Создание списков экземпляров классов

        
        private static List<Bus> GetBuses()
        {
            return new List<Bus>
            {
                new Bus
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Petrol,
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

                new Bus
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Diesel,
                        Power = 150,
                        Volume = 1.6M,
                        SerialNumber = "D123451"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 3000,
                        WheelsCount = 4,
                        SerialNumber = "B123451"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 6,
                        TransmissionType = TransmissionTypeEnum.Manual,
                        Manufacturer = "ANKAI"
                    },

                    PassengerSeatCount = 10,
                    PassengerStandCount = 15
                },

                new Bus
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Electric,
                        Power = 120,
                        Volume = 0,
                        SerialNumber = "EL123451"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 2550,
                        WheelsCount = 4,
                        SerialNumber = "CH123451"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 1,
                        TransmissionType = TransmissionTypeEnum.CVT,
                        Manufacturer = "AYATS"
                    },

                    PassengerSeatCount = 10,
                    PassengerStandCount = 15
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
                },

                new Cargo
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Diesel,
                        Volume = 4,
                        Power = 320,
                        SerialNumber = "MAN123452"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 10000,
                        WheelsCount = 6,
                        SerialNumber = "CH12345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 8,
                        TransmissionType = TransmissionTypeEnum.Automatic,
                        Manufacturer = "GM"
                    },

                    CargoType = CargoTypeEnum.Platform
                },

                new Cargo
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Electric,
                        Volume = 0,
                        Power = 350,
                        SerialNumber = "EL012345"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 8000,
                        WheelsCount = 6,
                        SerialNumber = "CH412345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 6,
                        TransmissionType = TransmissionTypeEnum.DCT,
                        Manufacturer = "GP"
                    },

                    CargoType = CargoTypeEnum.Wagon
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
                        Volume = 1.4M,
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

                new PassengerCar
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Petrol,
                        Power = 180,
                        Volume = 1.7M,
                        SerialNumber = "DP12345"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 1100,
                        WheelsCount = 4,
                        SerialNumber = "FB12345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 5,
                        Manufacturer = "Jatco",
                        TransmissionType = TransmissionTypeEnum.CVT
                    },

                    DriveUnit = DriveUnitEnum.FrontWheelDrive
                },

                new PassengerCar
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Electric,
                        Power = 220,
                        Volume = 0,
                        SerialNumber = "EL12345"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 1250,
                        WheelsCount = 4,
                        SerialNumber = "FB12345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 5,
                        Manufacturer = "BMW",
                        TransmissionType = TransmissionTypeEnum.Automatic
                    },

                    DriveUnit = DriveUnitEnum.AllWheelDrive
                },
            };
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
                    },

                    MaxSpeed = 100,
                },

                new Scooter
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Petrol,
                        Power = 60,
                        Volume = 0.8M,
                        SerialNumber = "P12345"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 90,
                        WheelsCount = 2,
                        SerialNumber = "SC512345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 0,
                        TransmissionType = TransmissionTypeEnum.CVT,
                        Manufacturer = "Jatco"
                    },

                    MaxSpeed = 120,
                },

                new Scooter
                {
                    Engine = new Engine
                    {
                        EngineType = EngineTypeEnum.Petrol,
                        Power = 120,
                        Volume = 1,
                        SerialNumber = "P412345"
                    },

                    Chassis = new Chassis
                    {
                        WeightLoad = 130,
                        WheelsCount = 2,
                        SerialNumber = "SC522345"
                    },

                    Transmission = new Transmission
                    {
                        GearsCount = 5,
                        TransmissionType = TransmissionTypeEnum.Automatic,
                        Manufacturer = "WV"
                    },

                    MaxSpeed = 120,
                },
            };
        }

        private static List<Bike> GetBikes()
        {
            return new List<Bike>
            {
                new Bike
                {
                    HasCradle = true,
                    Engine = new Engine
                    {
                        Volume = 2
                    }
                }
            };
        }

        #endregion
    }
}
