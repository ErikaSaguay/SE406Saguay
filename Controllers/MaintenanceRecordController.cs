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
    public class MaintenanceRecordController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            MaintenanceRecordViewModel maintenanceRecordVm = new MaintenanceRecordViewModel();
            using (var db = new MaintenanceRecordDBContext())
            {
                maintenanceRecordVm.MaintenanceRecordList = db.MaintenanceRecords.ToList();
                maintenanceRecordVm.NewMaintenanceRecord = new MaintenanceRecord();
                maintenanceRecordVm.MaintenanceActions = GetMaintenanceActionsDLL();
                maintenanceRecordVm.Inspectors = GetInspectorsDLL();
            }
            return View(maintenanceRecordVm);
        }
        [HttpPost]
        public IActionResult Index(MaintenanceRecordViewModel maintenanceRecordVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MaintenanceRecordDBContext())
                {
                    db.MaintenanceRecords.Add(maintenanceRecordVm.NewMaintenanceRecord);
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            MaintenanceRecordViewModel maintenanceRecordVm = new MaintenanceRecordViewModel();
            using (MaintenanceRecordDBContext db = new MaintenanceRecordDBContext())
            {

                maintenanceRecordVm.NewMaintenanceRecord = db.MaintenanceRecords.Where(
                    e => e.MaintenanceRecordId == id).SingleOrDefault();

                maintenanceRecordVm.MaintenanceActions = GetMaintenanceActionsDLL();
                maintenanceRecordVm.Inspectors = GetInspectorsDLL();

                return View(maintenanceRecordVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(MaintenanceRecordViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (MaintenanceRecordDBContext db = new MaintenanceRecordDBContext())
                {

                    MaintenanceRecord maintenanceR = obj.NewMaintenanceRecord;

                    maintenanceR.MaintenanceRecordId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(maintenanceR).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            MaintenanceRecordViewModel maintenanceRecordVm = new MaintenanceRecordViewModel();
            using (MaintenanceRecordDBContext db = new MaintenanceRecordDBContext())
            {
                //instantiate object from view model
                maintenanceRecordVm.NewMaintenanceRecord = new MaintenanceRecord();
                //retrieve primary key/id from route data
                maintenanceRecordVm.NewMaintenanceRecord.MaintenanceRecordId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(maintenanceRecordVm.NewMaintenanceRecord).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        private static List<SelectListItem> GetMaintenanceActionsDLL()
        {
            List<SelectListItem> maintenances = new List<SelectListItem>();
            MaintenanceActionViewModel mAVM = new MaintenanceActionViewModel();
            using (var db = new MaintenanceActionDBContext())
            {
                mAVM.MaintenanceActionList = db.MaintenanceActions.ToList();
            }
            foreach (MaintenanceAction Ma in mAVM.MaintenanceActionList)
            {
                maintenances.Add(new SelectListItem
                {
                    Text = Ma.MaintenanceActionName,
                    Value = Ma.MaintenanceActionId.ToString()
                });
            }
            return maintenances;
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
    }
}
