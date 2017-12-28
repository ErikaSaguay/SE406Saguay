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
    public class CountyController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            CountyViewModel countyVm = new CountyViewModel();
            using (var db = new CountyDBContext())
            {
                countyVm.CountyList = db.Counties.ToList();
                countyVm.NewCounty = new County();
            }
            return View(countyVm);
        }
        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(CountyViewModel countyVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CountyDBContext())
                {
                    db.Counties.Add(countyVm.NewCounty);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            CountyViewModel countyVm = new CountyViewModel();
            using (CountyDBContext db = new CountyDBContext())
            {

                countyVm.NewCounty = db.Counties.Where(
                    e => e.CountyId == id).SingleOrDefault();


                return View(countyVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(CountyViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (CountyDBContext db = new CountyDBContext())
                {

                    County counties = obj.NewCounty;

                    counties.CountyId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(counties).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            CountyViewModel countyVm = new CountyViewModel();
            using (CountyDBContext db = new CountyDBContext())
            {
                using (var dbB = new BridgeDBContext())
                {
                    BridgeViewModel bridgeVm = new BridgeViewModel();
                    bridgeVm.BridgeList = dbB.Bridges.ToList();
                    bridgeVm.NewBridge = dbB.Bridges.Where(
                    e => e.CountyId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (bridgeVm.NewBridge == null)
                    {
                        countyVm.NewCounty = new County();
                        //retrieve primary key/id from route data
                        countyVm.NewCounty.CountyId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(countyVm.NewCounty).State =
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
