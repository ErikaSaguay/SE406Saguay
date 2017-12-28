using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE406_Saguay
{
    public class Bridge
    {
        public Guid BridgeId { get; set; }

        [Required(ErrorMessage ="Material Design Id is required")]
        public Guid MaterialDesignId { get; set; }

        [Required(ErrorMessage = "Construction Design Id is required")]
        public Guid ConstructionDesignId { get; set; }

        [Required(ErrorMessage = "Functional Class Id is required")]
        public Guid FunctionalClassId { get; set; }

        [Required(ErrorMessage = "Status Id is required")]
        public Guid StatusId { get; set; }

        [Required(ErrorMessage ="County Id is required")]
        public Guid CountyId { get; set; }

        [Required(ErrorMessage = "NbiNumber is required")]
        [MinLength(5, ErrorMessage = "Minimum length of NbiNumber is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of NbiNumber is 100 characters")]
        public string NbiNumber { get; set; }

        public decimal? Rating { get; set; }

        [MaxLength(25, ErrorMessage = "Max length of NbiNumber is 25 characters")]
        public string RouteNumber { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MinLength(5, ErrorMessage = "Minimum length of City is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of City is 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Intersecting Feature is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Intersecting Feature is 5 characters")]
        [MaxLength(255, ErrorMessage = "Max length of IntersectingFeature is 255 characters")]
        public string IntersectingFeature { get; set; }

        [Required(ErrorMessage = "Carried Feature is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Carried Feature is 5 characters")]
        [MaxLength(255, ErrorMessage = "Max length of Carried Feature is 255 characters")]
        public string CarriedFeature { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Location is 5 characters")]
        [MaxLength(255, ErrorMessage = "Max length of Location is 255 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Built is required")]
        [Range(1600, 2050, ErrorMessage = "Built out of range")]
        public int Built { get; set; }

        [Range(1600,2025 , ErrorMessage = "Reconstructed out of range")]
        public int? Reconstructed { get; set; }

        [Required(ErrorMessage = "Total Length is required")]
        public decimal TotalLength { get; set; }

    }
}
