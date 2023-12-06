using ConnmIntel.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Monitoring
{
    public class MonitoringByDifferenceSearchPagedDto:NamedSearchPagedDto
    {
        public string TypeName { get; set; }
        public string Bin { get; set; }

    }
}
