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
    public class FunctionalClassController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            FunctionalClassViewModel functionalClassVm = new FunctionalClassViewModel();
            using (var db = new FunctionalClassDBContext())
            {
                functionalClassVm.FunctionalClassList = db.FunctionalClasses.ToList();
                functionalClassVm.NewFunctionalClass = new FunctionalClass();
            }
            return View(functionalClassVm);
        }
        [HttpPost]
        public IActionResult Index(FunctionalClassViewModel functionalClassVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FunctionalClassDBContext())
                {
                    db.FunctionalClasses.Add(functionalClassVm.NewFunctionalClass);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            FunctionalClassViewModel functionalClassVm = new FunctionalClassViewModel();
            using (FunctionalClassDBContext db = new FunctionalClassDBContext())
            {

                functionalClassVm.NewFunctionalClass = db.FunctionalClasses.Where(
                    e => e.FunctionalClassId == id).SingleOrDefault();

                return View(functionalClassVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(FunctionalClassViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (FunctionalClassDBContext db = new FunctionalClassDBContext())
                {

                    FunctionalClass functionalClass = obj.NewFunctionalClass;

                    functionalClass.FunctionalClassId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(functionalClass).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            FunctionalClassViewModel functionalClassVm = new FunctionalClassViewModel();
            using (FunctionalClassDBContext db = new FunctionalClassDBContext())
            {
                using (var dbB = new BridgeDBContext())
                {
                    BridgeViewModel bridgeVm = new BridgeViewModel();
                    bridgeVm.BridgeList = dbB.Bridges.ToList();
                    bridgeVm.NewBridge = dbB.Bridges.Where(
                    e => e.FunctionalClassId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (bridgeVm.NewBridge == null)
                    {
                        functionalClassVm.NewFunctionalClass = new FunctionalClass();
                        //retrieve primary key/id from route data
                        functionalClassVm.NewFunctionalClass.FunctionalClassId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(functionalClassVm.NewFunctionalClass).State =
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
