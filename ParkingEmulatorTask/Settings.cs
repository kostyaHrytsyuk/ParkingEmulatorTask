using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingEmulatorTask
{
    static class Settings
    {
        private static TimeSpan Timeout { get; set; } = new TimeSpan(0,0,3);

        public static Dictionary<CarType, decimal> PriceSet => new Dictionary<CarType, decimal>()
        {
             { CarType.Passenger, 3.00m }
           , { CarType.Truck, 10.00m }
           , { CarType.Bus, 5.00m}
           , { CarType.Motorcycle,1.50m}
        };

        private static int ParkingSpace { get; set; } = 50;

        private static double Fine { get; set; } = 0.03;

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
                Console.WriteLine($"Parking space: {ParkingSpace}\nCharging time: {Timeout}\nFine size: {Fine * 100}%");
                
                Console.WriteLine("\nIf you want to create a parking with these settings enter C.");
                Console.WriteLine("If you want to change value of settings enter next symbols");
                Console.WriteLine("P - for Parking space. Entered value must be less than 1000 ");
                Console.WriteLine("T - for Charging time. Enter value in seconds");
                Console.WriteLine("F - for Fine. Enter value in percents");
                Console.Write("Enter symbol: ");

                var propFirstLetter = Console.ReadLine().First();

                switch (propFirstLetter)
                {
                    case 'P':
                    case 'T':
                    case 'F':
                        SettingsPropertiesModificator(propFirstLetter);
                        break;
                    case 'C':
                        Console.WriteLine("Created");
                        break;
                    default:
                        Console.WriteLine("You entered a wrong value!");
                        ParkingCustomization();
                        break;
                }
            }

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
                        if (newValue < 1000)
                        {
                            ParkingSpace = (int) newValue;
                            Console.WriteLine("Parking Space value was changed!");
                        }
                        else
                        {
                            Console.WriteLine("You entered a wrong value!");
                        }
                        ParkingCustomization();
                        break;
                    case 'T':
                        Timeout = new TimeSpan(0, 0, (int) newValue);
                        Console.WriteLine("Timeout value was changed!");
                        ParkingCustomization();
                        break;
                    case 'F':
                        Fine = (double)newValue / 100;
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
                
    }
}
