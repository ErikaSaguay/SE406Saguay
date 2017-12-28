using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SE406_Saguay
{
    public class County
    {
        [Required(ErrorMessage ="County Id is required")]
        public Guid CountyId { get; set; }

        [Required(ErrorMessage = "County Name is required ")]
        [MinLength(5, ErrorMessage = "Minimum length of County Name is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of County Name is 50 characters")]
        public string CountyName { get; set; }
    }
}
