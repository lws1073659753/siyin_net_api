using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Pallet
{
    public class PrintPalletTagDto
    {
        public string ScanPn { get; set; }
        public int ScanPnQty { get; set; }
        public string BoxName { get; set; }
        public int BoxCount { get; set; }
    }
}
