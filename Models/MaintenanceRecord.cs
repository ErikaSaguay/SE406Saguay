using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SE406_Saguay
{
    public class MaintenanceRecord
    {
       [Key]
        public Guid MaintenanceRecordId { get; set; }

        [Required(ErrorMessage = "Maintenance Action Id is required")]
        public Guid MaintenanceActionId { get; set; }

        [Required(ErrorMessage = "Inspector Id is required")]
        public Guid InspectorId { get; set; }

        [Required(ErrorMessage = "Maintenance Projected Start is required")]
        public DateTime MaintenanceProjectedStart { get; set; }

        [Required(ErrorMessage = "Maintenance Projected End is required")]
        public DateTime MaintenanceProjectedEnd { get; set; }

        public DateTime? MaintenanceActualStart { get; set; }
        public DateTime? MaintenanceActualEnd { get; set; }

        [Required(ErrorMessage = "Maintenance Projected Cost is required")]
        public decimal MaintenanceProjectedCost { get; set; }

        public decimal? MaintenanceActualCost { get; set; }

        [MaxLength(1000, ErrorMessage = "Max length of Maintenance Notes is 1000 characters")]
        public string MaintenanceNotes { get; set; }

        [MaxLength(1000, ErrorMessage = "Max length of Inspector Notes is 1000 characters")]
        public string InspectorNotes { get; set; }

    }
}
