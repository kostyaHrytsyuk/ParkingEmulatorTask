using System;
using System.Linq;
using System.Threading;

namespace ParkingEmulatorTask
{
    static class Menu
    {
        #region Parking creation & closing
        private static bool IsCustomized = false;
        private static bool IsClosed = false;

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

        public static void MenuMap(Parking parking)
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
                    parking.AddCar();
                    break;
                case "D":
                    TakeCarFromParking(parking);
                    break;
                case "C":
                    parking.GetAllCars();
                    break;
                case "S":
                    GetFreeParkingSpace(parking);
                    break;
                case "B":
                    GetParkingBalance(parking);
                    break;
                case "P":
                    GetPrices();                    
                    break;
                case "T":
                    GetAllTransactions();
                    break;
                case "L":
                    GetLastMinuteTransactions(parking);
                    break;
                case "X":
                    CloseParking(parking);
                    break;
                default:
                    break;
            }

            if (!IsClosed)
            {
                ReturnToMenu(parking);
            }

        }

        public static void ReturnToMenu(Parking parking)
        {
            Console.WriteLine("To return to main menu - press R");
            var input = Console.ReadKey().Key.ToString();
            Console.WriteLine();
            if (input == "R")
            {
                Console.Clear();
                MenuMap(parking);
            }
            else
            {
                Console.WriteLine("You entered a wrong value!\t");
                ReturnToMenu(parking);
            }
        }
                

        #endregion

        #region Car Addition
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
        public static void TakeCarFromParking(Parking parking)
        {
            int carId;
            Console.WriteLine("Enter you Car Id");

            var input = Console.ReadLine();

            if (int.TryParse(input, out carId))
            {
                parking.DeleteCar(carId);
            }
            else
            {
                Console.WriteLine("You enter a wrong value!");
                ReturnToMenu(parking);
            }
        }

        
        
        public static void CarBalanceRefilling(Car car, Parking parking)
        {
            Console.WriteLine($"To take car {car.Id} from parking, insert {-car.Balance}");
            double fine = InputedBalanceValidation();
            car.Balance += fine;
            parking.ActiveBalance -= fine;
            parking.PassiveBalance += fine;            
        }
        #endregion

        public static void GetParkingBalance(Parking parking)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Parking balance\t");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Passive balance\t");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Active balance");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{parking.ActiveBalance + parking.PassiveBalance}\t\t");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{parking.PassiveBalance}\t\t");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(parking.ActiveBalance);

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void GetFreeParkingSpace(Parking parking)
        {
            var freeSpaces = Settings.ParkingSpace - parking.Cars.Count;
            Console.WriteLine("Current parking fullness:");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Free spaces: {freeSpaces}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Booked places: {parking.Cars.Count}");

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

        public static void GetLastMinuteTransactions(Parking parking)
        {
            Console.WriteLine("CarId\tWritten Off Money\tTransaction Time");
            foreach (var transaction in parking.LastMinuteTransactions)
            {
                Console.WriteLine(transaction.CarId + "\t" + transaction.WrittenOffMoney.ToString("F") + "\t\t\t" + transaction.TransactionTime);
            }
        }

        public static void GetAllTransactions()
        {
            Console.WriteLine(Transaction.GetTransactionLog());
        }

        public static void CloseParking(Parking parking)
        {
            IsClosed = true;
            Console.Clear();
            parking.CloseParking();
            Console.WriteLine("Thank you for choosing our parking!\nHave a nice day!");
            Thread.Sleep(3000);
        }
    }
}
