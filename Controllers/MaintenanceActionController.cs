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
    public class MaintenanceActionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            MaintenanceActionViewModel maintenanceActionVm = new MaintenanceActionViewModel();
            using (var db = new MaintenanceActionDBContext())
            {
                maintenanceActionVm.MaintenanceActionList = db.MaintenanceActions.ToList();
                maintenanceActionVm.NewMaintenanceAction = new  MaintenanceAction();
            }
            return View(maintenanceActionVm);
        }
        [HttpPost]
        public IActionResult Index(MaintenanceActionViewModel maintenanceActionVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MaintenanceActionDBContext())
                {
                    db.MaintenanceActions.Add(maintenanceActionVm.NewMaintenanceAction);
                    db.SaveChanges();
                    
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            MaintenanceActionViewModel maintenanceActionVm = new MaintenanceActionViewModel();
            using (MaintenanceActionDBContext db = new MaintenanceActionDBContext())
            {

                maintenanceActionVm.NewMaintenanceAction = db.MaintenanceActions.Where(
                    e => e.MaintenanceActionId == id).SingleOrDefault();



                return View(maintenanceActionVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(MaintenanceActionViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (MaintenanceActionDBContext db = new MaintenanceActionDBContext())
                {

                    MaintenanceAction maintenanceA = obj.NewMaintenanceAction;

                    maintenanceA.MaintenanceActionId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(maintenanceA).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            MaintenanceActionViewModel maintenanceActionVm = new MaintenanceActionViewModel();
            using (MaintenanceActionDBContext db = new MaintenanceActionDBContext())
            {
                using (var dbE = new MaintenanceRecordDBContext())
                {
                    MaintenanceRecordViewModel maintenanceRVm = new MaintenanceRecordViewModel();
                    maintenanceRVm.MaintenanceRecordList = dbE.MaintenanceRecords.ToList();
                    maintenanceRVm.NewMaintenanceRecord = dbE.MaintenanceRecords.Where(
                    e => e.MaintenanceActionId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (maintenanceRVm.NewMaintenanceRecord == null)
                    {
                        maintenanceActionVm.NewMaintenanceAction = new MaintenanceAction();
                        //retrieve primary key/id from route data
                        maintenanceActionVm.NewMaintenanceAction.MaintenanceActionId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(maintenanceActionVm.NewMaintenanceAction).State =
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
