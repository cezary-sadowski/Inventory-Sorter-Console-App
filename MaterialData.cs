using System.Collections.Generic;

namespace Inventory_Sorter
{
    public class Material
    {
        public Material(string materialId, Dictionary<string, int> amountPerWarehouse)
        {
            MaterialId = materialId;
            AmountPerWarehouse = amountPerWarehouse;
        }
        public string MaterialId { get; set; }
        public Dictionary<string, int> AmountPerWarehouse { get; set; }
    }
}
