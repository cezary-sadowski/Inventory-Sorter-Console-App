using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace Inventory_Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var inventoryData = File.ReadAllLines(@"C:\Users\Czarek\source\repos\Inventory Sorter\Data\InputData.txt");
            var inventoryDataWithNoHashLines = DeleteLinesStartingWithHash(inventoryData);

            foreach (var data in inventoryDataWithNoHashLines)
            {
                Console.WriteLine(data);
            }

            Console.ReadLine();
        }

        static string[] DeleteLinesStartingWithHash(string[] inventoryData)
        {
            var formattedData = inventoryData
                .Where(d => !d.StartsWith('#'))
                .ToArray();

            return formattedData;
        }
            
    }
}
