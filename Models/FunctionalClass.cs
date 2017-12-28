using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SE406_Saguay
{
    public class FunctionalClass
    {
        [Required(ErrorMessage = "Functional Class Id is required")]
        public Guid FunctionalClassId { get; set; }

        [Required(ErrorMessage = "Functional Class Type is required ")]
        [MinLength(5, ErrorMessage = "Minimum length of Functional Class Type is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Functional Class Type is 50 characters")]
        public string FunctionalClassType { get; set; }
    }
}
