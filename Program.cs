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
                string[] cuttedData = materialData.Split(new char[] { ';', '|'});
                Dictionary<string, int> amountPerWarehouse = GetMaterialAmountPerWarehouse(cuttedData);
                var material = new Material(cuttedData[0], amountPerWarehouse);
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

        static Dictionary<string, int> GetMaterialAmountPerWarehouse(string[] data)
        {
            var dataList = data.ToList().Skip(2);
            var amountPerWarehouse = new Dictionary<string, int>();
            
            foreach (var d in dataList)
            {
                var cuttedData = d.Split(',');
                amountPerWarehouse.Add(cuttedData[0], int.Parse(cuttedData[1]));
            }
            return amountPerWarehouse;
        }
            
    }
}
