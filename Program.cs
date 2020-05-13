using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Inventory_Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var materialInventoryData = File.ReadAllLines(@"C:\Users\Czarek\source\repos\Inventory Sorter\Data\InputData.txt");
            var materialDataWithNoHashLines = DeleteIgnoredLines(materialInventoryData);
            var materials = new List<Material>();
            var warehouses = new List<InventoryData>();
            

            foreach (var materialData in materialDataWithNoHashLines)
            {
                string[] cuttedData = materialData.Split(new char[] { ';', '|', ','});
                int totalAmount = GetTotalMaterialAmount(cuttedData);

                var material = new Material(cuttedData[1], totalAmount);
                materials.Add(material);
            }

            Console.ReadLine();
        }

        static string[] DeleteIgnoredLines(string[] inventoryData)
        {
            var formattedData = inventoryData
                .Where(d => !d.StartsWith('#') && !String.IsNullOrEmpty(d))
                .ToArray();

            return formattedData;
        }

        static int GetTotalMaterialAmount(string[] data)
        {
            int totalAmount = 0;
            foreach (var d in data)
            {
                int amount;
                if (int.TryParse(d, out amount))
                {
                    totalAmount += amount;
                }
            }
            return totalAmount;
        }
            
    }
}
