using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SE406_Saguay
{
    public class MaintenanceRecordViewModel
    {
        public List<MaintenanceRecord> MaintenanceRecordList { get; set; }
        public MaintenanceRecord NewMaintenanceRecord { get; set; }
        public List<SelectListItem> MaintenanceActions { get; set; }
        public List<SelectListItem> Inspectors { get; set; }
    }
}
