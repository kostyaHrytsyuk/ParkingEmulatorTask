namespace ParkingEmulatorTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking parking = Parking.Instance;

            Menu.MenuMap(parking);
            
        }
    }
}
