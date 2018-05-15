using System;
using System.Linq;
using System.Threading;
using ParkingEmulatorLogic;

namespace ParkingConsoleMenu
{
    static class Menu
    {
        #region Parking creation & closing
        private static bool IsCustomized = false;
        private static bool IsClosed = false;
        private static Parking _parking;

        public static void ParkingCustomization()
        {
            if (IsCustomized)
            {
                Console.WriteLine("You already customized your parking!\nIf you want to do it again - restart the program");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(new string('-', 15));
                Console.WriteLine();
                Console.WriteLine("Current parking settings are:");
                Console.WriteLine($"Parking space: {Settings.ParkingSpace}\nCharging time: {Settings.Timeout}\nFine size: {Settings.Fine * 100}%");

                Console.WriteLine("\nIf you want to create a parking with these settings enter C.");
                Console.WriteLine("If you want to change value of settings enter next symbols");
                Console.WriteLine("P - for Parking space. Entered value must be less than 900 ");
                Console.WriteLine("T - for Charging time. Enter value in seconds");
                Console.WriteLine("F - for Fine. Enter value in percents");
                Console.Write("Enter symbol: ");

                var propFirstLetter = Console.ReadKey().Key.ToString().First();
                Console.WriteLine();

                switch (propFirstLetter)
                {
                    case 'P':
                    case 'T':
                    case 'F':
                        SettingsPropertiesModificator(propFirstLetter);
                        break;
                    case 'C':
                        _parking = Parking.Instance;
                        Console.WriteLine("Parking created!");
                        break;
                    default:
                        Console.WriteLine("You entered a wrong value!");
                        ParkingCustomization();
                        break;
                }
            }
            Thread.Sleep(1500);
            Console.Clear();
            IsCustomized = true;
        }

        private static void SettingsPropertiesModificator(char propFirstLetter)
        {
            Console.Write("Enter value: ");
            var propValue = Console.ReadLine();
            uint newValue = 0;

            if (uint.TryParse(propValue, out newValue))
            {
                switch (propFirstLetter)
                {
                    case 'P':
                        if (newValue < 900)
                        {
                            Settings.ParkingSpace = (int)newValue;
                            Console.WriteLine("Parking Space value was changed!");
                        }
                        else
                        {
                            Console.WriteLine("You entered a wrong value!");
                        }
                        ParkingCustomization();
                        break;
                    case 'T':
                        Settings.Timeout = new TimeSpan(0, 0, (int)newValue);
                        Console.WriteLine("Timeout value was changed!");
                        ParkingCustomization();
                        break;
                    case 'F':
                        Settings.Fine = (double)newValue / 100;
                        Console.WriteLine("Fine value was changed!");
                        ParkingCustomization();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("You entered a wrong value!");
                ParkingCustomization();
            }
        }
        #endregion

        #region Menu Navigation
        public static void MenuMap()
        {
            Console.WriteLine("Parking functionality:");
            Console.WriteLine("1. To add car to the parking - press A");
            Console.WriteLine("2. To took car from the parking - press D");
            Console.WriteLine("3. To get information about all cars on the parking - press C");
            Console.WriteLine("4. To get information about free parking space - press S");
            Console.WriteLine("5. To get information about parking money balance - press B");
            Console.WriteLine("6. To get information about prices - press P");
            Console.WriteLine("7. To get all transactions - press T");
            Console.WriteLine("8. To get transactions during last minute - press L");
            Console.WriteLine("9. To exit the parking - press X");
            Console.Write("Enter the value: ");
            var input = Console.ReadKey().Key.ToString();
            Console.Clear();
            switch (input)
            {
                case "A":
                    AddCar();
                    break;
                case "D":
                    TakeCarFromParking();
                    break;
                case "C":
                    _parking.GetAllCars();
                    break;
                case "S":
                    GetFreeParkingSpace();
                    break;
                case "B":
                    GetCurrentProfit();
                    break;
                case "P":
                    GetPrices();                    
                    break;
                case "T":
                    GetAllTransactions();
                    break;
                case "L":
                    GetLastMinuteTransactions();
                    break;
                case "X":
                    CloseParking();
                    break;
                default:
                    break;
            }

            if (!IsClosed)
            {
                ReturnToMenu();
            }

        }

        public static void ReturnToMenu()
        {
            Console.WriteLine("To return to main menu - press R");
            var input = Console.ReadKey().Key.ToString();
            Console.WriteLine();
            if (input == "R")
            {
                Console.Clear();
                MenuMap();
            }
            else
            {
                Console.WriteLine("You entered a wrong value!\t");
                ReturnToMenu();
            }
        }
        #endregion

        #region Car Addition
        public static void AddCar()
        {
            CarType carType = InputedCarValidation();

            var firstPayment = InputedBalanceValidation();

            var car = new Car(carType, firstPayment);

            _parking.Cars.Add(car);
            Parking.CarsIds.Add(car.Id);
            Console.WriteLine($"Vehicle {car.CarType} with Id {car.Id} was added to parking");
            Thread.Sleep(1500);
        }
        public static double InputedBalanceValidation()
        {
            Console.WriteLine("Input your payment");
            double firstPayment;
            var inputValue = Console.ReadLine();

            if (double.TryParse(inputValue, out firstPayment) && firstPayment > 0)
            {
                return firstPayment;
            }
            else
            {
                Console.WriteLine("You entered a wrong value!");
                Console.WriteLine(new string('-', 15));
                return firstPayment = InputedBalanceValidation();
            }
        }

        public static CarType InputedCarValidation()
        {
            Console.WriteLine("Car Types:");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("1 - Passenger");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2 - Truck");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("3 - Bus");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("4 - Motorcycle");

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("Enter your car type:");

            var inputValue = Console.ReadLine()[0].ToString();
            uint carTypeKey = 0;
            CarType carType = CarType.Passenger;
            if (uint.TryParse(inputValue, out carTypeKey) && (carTypeKey < 5 && carTypeKey > 0))
            {
                switch (carTypeKey)
                {
                    case 1:
                        return CarType.Passenger;
                    case 2:
                        return CarType.Truck;
                    case 3:
                        return CarType.Bus;
                    case 4:
                        return CarType.Motorcycle;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You entered a wrong value!");
                Console.WriteLine(new string('-', 15));
                return carType = InputedCarValidation();
            }

            return carType;
        }
        #endregion

        #region Car Deletion
        public static void TakeCarFromParking()
        {
            int carId;
            Console.WriteLine("Enter you Car Id");

            var input = Console.ReadLine();

            if (int.TryParse(input, out carId))
            {
                DeleteCar(carId);
            }
            else
            {
                Console.WriteLine("You enter a wrong value!");
                ReturnToMenu();
            }
        }

        public static void DeleteCar(int carId)
        {
            var carDel = _parking.Cars.Find(item => item.Id == carId);

            if (carDel == null)
            {
                Console.WriteLine($"There is no car with such {carId} on the parking");
            }
            else
            {
                if (carDel.Balance < 0)
                {
                    CarBalanceRefilling(carDel);
                    DeleteCar(carId);
                }
                else
                {
                    _parking.RemoveCarFromParking(carId);
                    Console.WriteLine("Now you can take your car from the parking\nHave a nice day!");
                    Thread.Sleep(2000);
                }
            }

        }      

        public static void CarBalanceRefilling(Car car)
        {
            Console.WriteLine($"To take car {car.Id} from parking, insert {-car.Balance}");
            double fine = InputedBalanceValidation();
            _parking.CarBalanceRefilling(fine, car.Id);            
        }
        #endregion

        public static void GetCurrentProfit()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Parking balance\t");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Passive balance\t");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Active balance");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{_parking.CommonBalance}\t\t");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{_parking.PassiveBalance}\t\t");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(_parking.ActiveBalance);

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void GetFreeParkingSpace()
        {
            var freeSpaces = _parking.FreeSpace;
            Console.WriteLine("Current parking fullness:");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Free spaces: {freeSpaces}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Booked places: {_parking.Cars.Count}");

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void GetPrices()
        {
            Console.WriteLine("\tPrice\tCar Type");
            foreach (var price in Settings.PriceSet)
            {
                Console.WriteLine("\t" + price.Value + "\t" + price.Key);
            }
        }

        public static void GetLastMinuteTransactions()
        {
            Console.WriteLine("CarId\tWritten Off Money\tTransaction Time");
            foreach (var transaction in _parking.LastMinuteTransactions)
            {
                Console.WriteLine(transaction.CarId + "\t" + transaction.WrittenOffMoney.ToString("F") + "\t\t\t" + transaction.TransactionTime);
            }
        }

        public static void GetAllTransactions()
        {
            Console.WriteLine(Transaction.GetTransactionLog());
        }

        public static void CloseParking()
        {
            IsClosed = true;
            Console.Clear();
            _parking.CloseParking();
            Console.WriteLine("Thank you for choosing our parking!\nHave a nice day!");
            Thread.Sleep(3000);
        }
    }
}
