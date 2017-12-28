using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SE406_Saguay
{
    public class ConstructionDesign
    {
        [Required(ErrorMessage = "Construction Design Id is required")]
        public Guid ConstructionDesignId { get; set; }

        [Required(ErrorMessage = "Construction Design Type is required ")]
        [MinLength(5, ErrorMessage = "Minimum length of Construction Design Type is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Construction Design Type is 50 characters")]
        public string ConstructionDesignType { get; set; }


    }
}
