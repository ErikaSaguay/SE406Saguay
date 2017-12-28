using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SE406_Saguay
{
    public class InspectionCode
    {
        [Required(ErrorMessage = "Inspection Code Id is required")]
        public Guid InspectionCodeId { get; set; }

        [Required(ErrorMessage = "Inspection Code Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspection Code Name is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Inspection Code Name is 50 characters")]
        public string InspectionCodeName { get; set; }

        [Required(ErrorMessage = "Inspectoin Code Description is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspectoin Code Description is 5 characters")]
        [MaxLength(400, ErrorMessage = "Max length of Inspectoin Code Description is 400 characters")]
        public string InspectoinCodeDesc { get; set; }
    }
}
