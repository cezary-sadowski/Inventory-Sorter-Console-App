using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Inventory_Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var materialInventoryData = File.ReadAllLines(@"C:\Users\Czarek\source\repos\Inventory Sorter\Data\InputData.txt");
            var materialDataWithNoHashLines = DeleteIgnoredLines(materialInventoryData);
            var materials = new List<Material>();
            var warehouses = new List<WarehouseData>();
            

            foreach (var materialData in materialDataWithNoHashLines)
            {
                string[] cuttedData = materialData.Split(new char[] { ';', '|'});
                Dictionary<string, int> amountPerWarehouse = GetMaterialAmountPerWarehouse(cuttedData);
                var material = new Material(cuttedData[1], amountPerWarehouse);
                materials.Add(material);
            }

            foreach (var m in materials)
            {
                foreach (var w in m.AmountPerWarehouse)
                {
                    var warehouse = new WarehouseData(w.Key, w.Value);
                    warehouse.Material = m;

                    warehouses.Add(warehouse);
                }
                
            }

            var final = warehouses.GroupBy(w => w.Warehouse);

            foreach(var l in final)
            {
                Console.WriteLine(l.Key);

                foreach (var p in l)
                {
                    Console.WriteLine($"{p.Material.MaterialId}: {p.TotalMaterialAmount}");
                }
                
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
