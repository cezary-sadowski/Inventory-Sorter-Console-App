using System;
using System.IO;

namespace Inventory_Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            var inputData = File.ReadAllLines(@"C:\Users\Czarek\source\repos\Inventory Sorter\Data\InputData.txt");

            foreach (var data in inputData)
            {
                Console.WriteLine(data);
            }

            Console.ReadLine();
        }
    }
}
