using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SE406_Saguay
{
    public class Inspection
    {

        public Guid InspectionId { get; set; }

        [Required(ErrorMessage = "BridgeId is required")]
        public Guid BridgeId { get; set; }

        [Required(ErrorMessage = "Inspector Id is required")]
        public Guid InspectorId { get; set; }

        [Required(ErrorMessage = "Inspection Date is required")]
        public DateTime InspectionDate { get; set; }

        [Required(ErrorMessage = "Deck Inspectoin Code Id is required")]
        public Guid DeckInspectionCodeId { get; set; }

        [Required(ErrorMessage = "Superstructure Inspection Code Id is required")]
        public Guid SuperstructureInspectionCodeId { get; set; }

        [Required(ErrorMessage = "Substructure Inspection Code Id is required")]
        public Guid SubstructureInspectionCodeId { get; set; }

        [MaxLength(2000, ErrorMessage = "Max length of Location is 255 characters")]
        public string InspectionNotes { get; set; }
    }
}
