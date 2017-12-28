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
    public class InspectorController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            InspectorViewModel inspectorVm = new InspectorViewModel();
            using (var db = new InspectorDBContext())
            {
                inspectorVm.InspectorList = db.Inspectors.ToList();
                inspectorVm.NewInspector = new Inspector();
            }
            return View(inspectorVm);
        }
        [HttpPost]
        public IActionResult Index(InspectorViewModel inspectionCodeVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new InspectorDBContext())
                {
                    db.Inspectors.Add(inspectionCodeVm.NewInspector);
                    db.SaveChanges();
                    
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            InspectorViewModel inspectorVm = new InspectorViewModel();
            using (InspectorDBContext db = new InspectorDBContext())
            {

                inspectorVm.NewInspector = db.Inspectors.Where(
                    e => e.InspectorId == id).SingleOrDefault();



                return View(inspectorVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(InspectorViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (InspectorDBContext db = new InspectorDBContext())
                {

                    Inspector inspector = obj.NewInspector;

                    inspector.InspectorId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(inspector).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            if (checkSuper(id) == true && checkSubst(id) == true )
            {
                TempData["ResultMessage"] = "Object deleted";
            }else
            {
                TempData["ResultMessage"] =
                            "Object has dependencies, cannot delete!!!!";
            }
            return RedirectToAction("Index");
        }
        private bool checkSuper(Guid id)
        {
            InspectorViewModel inspectorVm = new InspectorViewModel();
            using (InspectorDBContext db = new InspectorDBContext())
            {
                using (var dbE = new InspectionDBContext())
                {
                    InspectionViewModel inspectionVm = new InspectionViewModel();
                    inspectionVm.InspectionList = dbE.Inspections.ToList();
                    inspectionVm.NewInspection = dbE.Inspections.Where(
                    e => e.InspectorId == id).FirstOrDefault();
                    //instantiate object from view model

                    if (inspectionVm.NewInspection == null)
                    {
                        inspectorVm.NewInspector = new Inspector();
                        //retrieve primary key/id from route data
                        inspectorVm.NewInspector.InspectorId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(inspectorVm.NewInspector).State =
                            EntityState.Deleted;
                        //persist changes
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }

        }
        private bool checkSubst(Guid id)
        {
            InspectorViewModel inspectorVm = new InspectorViewModel();
            using (InspectorDBContext db = new InspectorDBContext())
            {
                using (var dbE = new MaintenanceRecordDBContext())
                {
                    MaintenanceRecordViewModel maintenanceVM = new MaintenanceRecordViewModel();
                    maintenanceVM.MaintenanceRecordList = dbE.MaintenanceRecords.ToList();
                    maintenanceVM.NewMaintenanceRecord = dbE.MaintenanceRecords.Where(
                    e => e.InspectorId == id).FirstOrDefault();
                    //instantiate object from view model

                    if (maintenanceVM.NewMaintenanceRecord == null)
                    {
                        inspectorVm.NewInspector = new Inspector();
                        //retrieve primary key/id from route data
                        inspectorVm.NewInspector.InspectorId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(inspectorVm.NewInspector).State =
                            EntityState.Deleted;
                        //persist changes
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }

        }

    }
}
