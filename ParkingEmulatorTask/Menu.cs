using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace ParkingEmulatorTask
{
    static class Menu
    {
        #region Parking creation
        private static bool IsCustomized { get; set; } = false;

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

        #region Car Addition
        public static double InputedBalanceValidation()
        {
            Console.WriteLine("Input your first payment");
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

    }
}
