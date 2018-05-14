using System;
using System.Collections.Generic;
using System.IO;

namespace ParkingEmulatorTask
{
    class Transaction
    {
        private static bool isFirstTransaction = true;
        public readonly DateTime TransactionTime;
        public readonly int CarId;
        public readonly double WrittenOffMoney;

        public Transaction(int carId, double fee)
        {
            CarId = carId;
            WrittenOffMoney = fee;
            TransactionTime = DateTime.Now;
        }

        public static void AddToTransactionLog(List<Transaction> transactions)
        {
            try
            {
                if (isFirstTransaction)
                {
                    using (StreamWriter writer = new StreamWriter("./Transactions.log"))
                    {
                        writer.WriteLine("CarId\tWritten Off Money\tTransaction Time");
                    }
                    isFirstTransaction = false;
                }

                using (StreamWriter writer = new StreamWriter("./Transactions.log", true))
                {
                    foreach (var transaction in transactions)
                    {
                        writer.WriteLine(transaction.CarId + "\t" + transaction.WrittenOffMoney.ToString("F") + "\t\t\t" + transaction.TransactionTime);
                        writer.WriteLine();
                    }

                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("There is no file with transactions\nContact administrator");                                
            }
            
            
        }

        public static string GetTransactionLog()
        {
            return File.ReadAllText("./Transactions.log");
        }
    }
}
