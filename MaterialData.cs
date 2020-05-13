using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Sorter
{
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
