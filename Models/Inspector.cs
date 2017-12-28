using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE406_Saguay
{
    public class Inspector
    {

        public Guid InspectorId { get; set; }

        [Required(ErrorMessage = "Inspector First Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspector First Name is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Inspector First Name is 50 characters")]
        public string InspectorFirst { get; set; }

        [Required(ErrorMessage = "Inspector Last Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspector Last Name is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Inspector Last Name is 50 characters")]
        public string InspectorLast { get; set; }

        [Required(ErrorMessage = "Inspector organization is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspector organization is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Inspector organization is 50 characters")]
        public string InspectorOrg { get; set; }

        [Required(ErrorMessage = "Inspector Certificate Effective is required")]
        public DateTime InspectorCertEffective { get; set; }

        [Required(ErrorMessage = "Inspector Certificate Expires is required")]
        public DateTime InspectorCertExpires { get; set; }
    }
}
