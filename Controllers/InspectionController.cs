using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Rendering;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE406_Saguay.Controllers
{
    public class InspectionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            InspectionViewModel inspectionVm = new InspectionViewModel();
            using (var db = new InspectionDBContext())
            {
                inspectionVm.InspectionList = db.Inspections.ToList();
                inspectionVm.NewInspection = new Inspection();
                inspectionVm.Bridges = GetBridgesDLL();
                inspectionVm.Inspectors = GetInspectorsDLL();
                inspectionVm.DeckInspectionCodes = GetInspectionCodesDLL();
                inspectionVm.SuperstructureInspectionCodes = GetSuperstructureInspectionCodesDLL();
                inspectionVm.SubstructureInspectionCodes = GetSubstructureInspectionCodesDLL();
            }
            return View(inspectionVm);
        }
        [HttpPost]
        public IActionResult Index(InspectionViewModel inspectionVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new InspectionDBContext())
                {
                    db.Inspections.Add(inspectionVm.NewInspection);
                    db.SaveChanges();
                    
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            InspectionViewModel inspectionVm = new InspectionViewModel();
            using (InspectionDBContext db = new InspectionDBContext())
            {

                inspectionVm.NewInspection = db.Inspections.Where(
                    e => e.InspectionId == id).SingleOrDefault();

                inspectionVm.Bridges = GetBridgesDLL();
                inspectionVm.Inspectors = GetInspectorsDLL();
                inspectionVm.DeckInspectionCodes = GetInspectionCodesDLL();
                inspectionVm.SuperstructureInspectionCodes = GetSuperstructureInspectionCodesDLL();
                inspectionVm.SubstructureInspectionCodes = GetSubstructureInspectionCodesDLL();

                return View(inspectionVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(InspectionViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (InspectionDBContext db = new InspectionDBContext())
                {

                    Inspection inspection = obj.NewInspection;

                    inspection.InspectionId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(inspection).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            InspectionViewModel inspectionVm = new InspectionViewModel();
            using (InspectionDBContext db = new InspectionDBContext())
            {
                using (var dbE = new InspectionDBContext())
                {
                    InspectionViewModel inspectionSupVm = new InspectionViewModel();
                    inspectionSupVm.InspectionList = dbE.Inspections.ToList();
                    inspectionSupVm.NewInspection = dbE.Inspections.Where(
                    e => e.SuperstructureInspectionCodeId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (inspectionSupVm.NewInspection == null)
                    {
                        inspectionVm.NewInspection = new Inspection();
                        //retrieve primary key/id from route data
                        inspectionVm.NewInspection.SuperstructureInspectionCodeId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(inspectionVm.NewInspection).State =
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
        private static List<SelectListItem> GetBridgesDLL()
        {
            List<SelectListItem> bridges = new List<SelectListItem>();
            BridgeViewModel bVM = new BridgeViewModel();
            using (var db = new BridgeDBContext())
            {
                bVM.BridgeList = db.Bridges.ToList();
            }
            foreach (Bridge Br in bVM.BridgeList)
            {
                bridges.Add(new SelectListItem
                {
                    Text = Br.BridgeId.ToString(),
                    Value = Br.BridgeId.ToString()
                });
            }
            return bridges;
        }
        private static List<SelectListItem> GetInspectorsDLL()
        {
            List<SelectListItem> inspectors = new List<SelectListItem>();
            InspectorViewModel iVM = new InspectorViewModel();
            using (var db = new InspectorDBContext())
            {
                iVM.InspectorList = db.Inspectors.ToList();
            }
            foreach (Inspector Ins in iVM.InspectorList)
            {
                inspectors.Add(new SelectListItem
                {
                    Text = Ins.InspectorFirst + " " + Ins.InspectorLast,
                    Value = Ins.InspectorId.ToString()
                });
            }
            return inspectors;
        }
        private static List<SelectListItem> GetInspectionCodesDLL()
        {
            List<SelectListItem> deckInsCode = new List<SelectListItem>();
            InspectionCodeViewModel iCVM = new InspectionCodeViewModel();
            using (var db = new InspectionCodeDBContext())
            {
                iCVM.InspectionCodeList = db.InspectionCodes.ToList();
            }
            foreach (InspectionCode InsC in iCVM.InspectionCodeList)
            {
                deckInsCode.Add(new SelectListItem
                {
                    Text = InsC.InspectionCodeName,
                    Value = InsC.InspectionCodeId.ToString()
                });
            }
            return deckInsCode;
        }
        private static List<SelectListItem> GetSuperstructureInspectionCodesDLL()
        {
            List<SelectListItem> SupInsCode = new List<SelectListItem>();
            InspectionViewModel siCVM = new InspectionViewModel();
            using (var db = new InspectionDBContext())
            {
                siCVM.InspectionList = db.Inspections.ToList();
            }
            foreach (Inspection InsC in siCVM.InspectionList)
            {
                SupInsCode.Add(new SelectListItem
                {
                    Text = InsC.SuperstructureInspectionCodeId.ToString(),
                    Value = InsC.SuperstructureInspectionCodeId.ToString()
                });
            }
            return SupInsCode;
        }
        private static List<SelectListItem> GetSubstructureInspectionCodesDLL()
        {
            List<SelectListItem> SupInsCode = new List<SelectListItem>();
            InspectionViewModel siCVM = new InspectionViewModel();
            using (var db = new InspectionDBContext())
            {
                siCVM.InspectionList = db.Inspections.ToList();
            }
            foreach (Inspection InsC in siCVM.InspectionList)
            {
                SupInsCode.Add(new SelectListItem
                {
                    Text = InsC.SubstructureInspectionCodeId.ToString(),
                    Value = InsC.SubstructureInspectionCodeId.ToString()
                });
            }
            return SupInsCode;
        }
    }
}
