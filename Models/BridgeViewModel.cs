using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SE406_Saguay
{
    public class BridgeViewModel
    {
        public List<Bridge> BridgeList { get; set; }
        public Bridge NewBridge { get; set; }
        public List<SelectListItem> MaterialDesigns { get; set; }
        public List<SelectListItem> ConstructionDesigns { get; set; }
        public List<SelectListItem> FunctionalClasses { get; set; }
        public List<SelectListItem> StatusCodes { get; set; }
        public List<SelectListItem> Counties { get; set; }
    }
}
