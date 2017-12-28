using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ApplicationInsights;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE406_Saguay.Controllers
{
    public class InspectionCodeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            InspectionCodeViewModel inspectionCodeVm = new InspectionCodeViewModel();
            using (var db = new InspectionCodeDBContext())
            {
                inspectionCodeVm.InspectionCodeList = db.InspectionCodes.ToList();
                inspectionCodeVm.NewInspectionCode = new InspectionCode();
            }
            return View(inspectionCodeVm);
        }
        [HttpPost]
        public IActionResult Index(InspectionCodeViewModel inspectionCodeVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new InspectionCodeDBContext())
                {
                    db.InspectionCodes.Add(inspectionCodeVm.NewInspectionCode);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            InspectionCodeViewModel inspectionCodeVm = new InspectionCodeViewModel();
            using (InspectionCodeDBContext db = new InspectionCodeDBContext())
            {

                inspectionCodeVm.NewInspectionCode = db.InspectionCodes.Where(
                    e => e.InspectionCodeId == id).SingleOrDefault();


                return View(inspectionCodeVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(InspectionCodeViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (InspectionCodeDBContext db = new InspectionCodeDBContext())
                {

                    InspectionCode inspectionCode = obj.NewInspectionCode;

                    inspectionCode.InspectionCodeId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(inspectionCode).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            InspectionCodeViewModel inspectionCVm = new InspectionCodeViewModel();
            using (InspectionCodeDBContext db = new InspectionCodeDBContext())
            {
                using (var dbE = new InspectionDBContext())
                {
                    InspectionViewModel inspectionVm = new InspectionViewModel();
                    inspectionVm.InspectionList = dbE.Inspections.ToList();
                    inspectionVm.NewInspection = dbE.Inspections.Where(
                    e => e.DeckInspectionCodeId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (inspectionVm.NewInspection == null)
                    {
                        inspectionCVm.NewInspectionCode = new InspectionCode();
                        //retrieve primary key/id from route data
                        inspectionCVm.NewInspectionCode.InspectionCodeId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(inspectionCVm.NewInspectionCode).State =
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
    }
}
