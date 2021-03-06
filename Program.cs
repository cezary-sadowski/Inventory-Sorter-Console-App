﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Inventory_Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathToSourceData = Path.Combine(Directory.GetCurrentDirectory(), "Data", "InputData.txt");
            var materialInventoryData = File.ReadAllLines(pathToSourceData);
            var materialDataWithNoHashLines = DeleteIgnoredLines(materialInventoryData);
            var materials = GetListOfMaterials(materialDataWithNoHashLines);
            var warehouses = GetListOfWarehousesWithMaterials(materials);
            var grouppedWarehouses = warehouses.GroupBy(w => w.Warehouse);
            var warehouseWithTotal = GetWarehousesWithTotalAmount(grouppedWarehouses);
            SortAndLogDataToConsole(warehouseWithTotal, grouppedWarehouses);
            Console.ReadLine();
        }

        static void SortAndLogDataToConsole(Dictionary<string, int> warehouseWithTotal,
            IEnumerable<IGrouping<string, WarehouseData>> grouppedWarehouses)
        {
            var sortedWarehouse = warehouseWithTotal
                .OrderByDescending(s => s.Value)
                .ThenByDescending(s => s.Key);

            foreach(var warehouse in sortedWarehouse)
            {
                Console.WriteLine($"{warehouse.Key} (total {warehouse.Value})");

                var currentWarehouse = grouppedWarehouses.Where(c => c.Key.Equals(warehouse.Key));
                foreach(var current in currentWarehouse)
                {
                    var orderedByMaterial = current.OrderBy(t => t.Material.MaterialId);
                    foreach(var material in orderedByMaterial)
                    {
                        Console.WriteLine($"{material.Material.MaterialId}: {material.TotalMaterialAmount}");
                    }
                }
                Console.WriteLine();
            }
        }

        static Dictionary<string, int> GetWarehousesWithTotalAmount(IEnumerable<IGrouping<string, WarehouseData>> grouppedWarehouses)
        {
            var warehouseWithTotal = new Dictionary<string, int>();

            foreach (var warehouse in grouppedWarehouses)
            {
                var total = 0;
                foreach (var data in warehouse)
                {
                    total += data.TotalMaterialAmount;
                }

                warehouseWithTotal.Add(warehouse.Key, total);
            }

            return warehouseWithTotal;
        }

        static List<WarehouseData> GetListOfWarehousesWithMaterials(List<Material> materials)
        {
            var warehouses = new List<WarehouseData>();

            foreach (var material in materials)
            {
                foreach (var amount in material.AmountPerWarehouse)
                {
                    var warehouse = new WarehouseData(amount.Key, amount.Value);
                    warehouse.Material = material;

                    warehouses.Add(warehouse);
                }
            }

            return warehouses;
        }

        static List<Material> GetListOfMaterials(string[] materials)
        {
            var materialsList = new List<Material>();

            foreach (var materialData in materials)
            {
                string[] cuttedData = materialData.Split(new char[] { ';', '|' });
                Dictionary<string, int> amountPerWarehouse = GetMaterialAmountPerWarehouse(cuttedData);
                var material = new Material(cuttedData[1], amountPerWarehouse);
                materialsList.Add(material);
            }

            return materialsList;
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
