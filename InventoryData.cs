using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Inventory_Sorter
{
    public class InventoryData
    {
        public InventoryData(string warehouse, int totalMaterialAmount)
        {
            Warehouse = warehouse;
            TotalMaterialAmount = totalMaterialAmount;
        }
        public string Warehouse { get; set; }
        public int TotalMaterialAmount { get; set; }
        public Material Material { get; set; }
    }

    public class Material
    {
        public Material(string materialId, int materialAmount)
        {
            MaterialId = materialId;
            MaterialAmount = materialAmount;
        }
        public string MaterialId { get; set; }
        public int MaterialAmount { get; set; }
    }
}
