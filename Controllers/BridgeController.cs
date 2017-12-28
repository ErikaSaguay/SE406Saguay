using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Rendering;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE406_Saguay
{
    public class BridgeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            BridgeViewModel bridgeVm = new BridgeViewModel();
            using (var db = new BridgeDBContext())
            {
                bridgeVm.BridgeList = db.Bridges.ToList();
                bridgeVm.NewBridge = new Bridge();
                bridgeVm.MaterialDesigns = GetMaterialDesignsDLL();
                bridgeVm.ConstructionDesigns = GetConstructionDesignsDDL();
                bridgeVm.FunctionalClasses = GetFunctionalClassesDDL();
                bridgeVm.StatusCodes = GetStatusCodesDDL();
                bridgeVm.Counties = GetCountiesDDL();
            }
            return View(bridgeVm);
        }
        [HttpPost]
        public IActionResult Index(BridgeViewModel bridgeVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BridgeDBContext())
                {
                    db.Bridges.Add(bridgeVm.NewBridge);
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            BridgeViewModel bridgeVm = new BridgeViewModel();
            using (BridgeDBContext db = new BridgeDBContext())
            {

                bridgeVm.NewBridge = db.Bridges.Where(
                    e => e.BridgeId == id).SingleOrDefault();

                bridgeVm.MaterialDesigns = GetMaterialDesignsDLL();
                bridgeVm.ConstructionDesigns = GetConstructionDesignsDDL();
                bridgeVm.FunctionalClasses = GetFunctionalClassesDDL();
                bridgeVm.StatusCodes = GetStatusCodesDDL();
                bridgeVm.Counties = GetCountiesDDL();

                return View(bridgeVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(BridgeViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (BridgeDBContext db = new BridgeDBContext())
                {

                    Bridge bridge= obj.NewBridge;

                    bridge.BridgeId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(bridge).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            BridgeViewModel bridgeVm = new BridgeViewModel();
            using (BridgeDBContext db = new BridgeDBContext())
            {
                using (var dbE = new InspectionDBContext())
                {
                    InspectionViewModel inspectionVm = new InspectionViewModel();
                    inspectionVm.InspectionList = dbE.Inspections.ToList();
                    inspectionVm.NewInspection = dbE.Inspections.Where(
                    e => e.BridgeId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (inspectionVm.NewInspection == null)
                    {
                        bridgeVm.NewBridge = new Bridge();
                        //retrieve primary key/id from route data
                        bridgeVm.NewBridge.BridgeId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(bridgeVm.NewBridge).State =
                            EntityState.Deleted;
                        //persist changes
                        db.SaveChanges();
                        TempData["ResultMessage"] = "Object deleted";
                    }
                    else
                    {
                        TempData["ResultMessage"] =
                            "Object has dependencies, cannot delete!!!!";
                    }
                }

            }
            return RedirectToAction("Index");
        }
        private static List<SelectListItem> GetMaterialDesignsDLL()
        {
            List<SelectListItem> materialDesigns = new List<SelectListItem>();
            MaterialDesignViewModel mDVM = new MaterialDesignViewModel();
            using (var db = new MaterialDesignDBContext())
            {
                mDVM.MaterialDesignList = db.MaterialDesigns.ToList();
            }
            foreach (MaterialDesign mD in mDVM.MaterialDesignList)
            {
                materialDesigns.Add(new SelectListItem
                {
                    Text = mD.MaterialDesignType,
                    Value = mD.MaterialDesignId.ToString()
                });
            }
            return materialDesigns;
        }
        private static List<SelectListItem> GetConstructionDesignsDDL()
        {
            List<SelectListItem> consDesigns = new List<SelectListItem>();
            ConstructionDesignViewModel cDVM = new ConstructionDesignViewModel();
            using (var db = new ConstructionDesignDBContext())
            {
                cDVM.ConstructionDesignList = db.ConstructionDesigns.ToList();
            }
            foreach (ConstructionDesign cD in cDVM.ConstructionDesignList)
            {
                consDesigns.Add(new SelectListItem
                {
                    Text = cD.ConstructionDesignType,
                    Value = cD.ConstructionDesignId.ToString()
                });
            }
            return consDesigns;
        }
        private static List<SelectListItem> GetFunctionalClassesDDL()
        {
            List<SelectListItem> funcClasses = new List<SelectListItem>();
            FunctionalClassViewModel fCVM = new FunctionalClassViewModel();
            using (var db = new FunctionalClassDBContext())
            {
                fCVM.FunctionalClassList = db.FunctionalClasses.ToList();
            }
            foreach (FunctionalClass fC in fCVM.FunctionalClassList)
            {
                funcClasses.Add(new SelectListItem
                {
                    Text = fC.FunctionalClassType,
                    Value = fC.FunctionalClassId.ToString()
                });
            }
            return funcClasses;
        }
        private static List<SelectListItem> GetStatusCodesDDL()
        {
            List<SelectListItem> statCodes = new List<SelectListItem>();
            StatusCodeViewModel sCVM = new StatusCodeViewModel();
            using (var db = new StatusCodeDBContext())
            {
                sCVM.StatusCodeList = db.StatusCodes.ToList();
            }
            foreach (StatusCode sC in sCVM.StatusCodeList)
            {
                statCodes.Add(new SelectListItem
                {
                    Text = sC.StatusName,
                    Value = sC.StatusCodeId.ToString()
                });
            }
            return statCodes;
        }
        private static List<SelectListItem> GetCountiesDDL()
        {
            List<SelectListItem> counties = new List<SelectListItem>();
            CountyViewModel cVM = new CountyViewModel();
            using (var db = new CountyDBContext())
            {
                cVM.CountyList = db.Counties.ToList();
            }
            foreach (County cT in cVM.CountyList)
            {
                counties.Add(new SelectListItem
                {
                    Text = cT.CountyName,
                    Value = cT.CountyId.ToString()
                });
            }
            return counties;
        }

    }
}
