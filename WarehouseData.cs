namespace Inventory_Sorter
{
    public class WarehouseData
    {
        public WarehouseData(string warehouse, int totalMaterialAmount)
        {
            Warehouse = warehouse;
            TotalMaterialAmount = totalMaterialAmount;
        }
        public string Warehouse { get; set; }
        public int TotalMaterialAmount { get; set; }
        public Material Material { get; set; }
    }
}
