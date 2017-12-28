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
    public class ConstructionDesignController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ConstructionDesignViewModel constructionDesignVm = new ConstructionDesignViewModel();
            using (var db = new ConstructionDesignDBContext())
            {
                constructionDesignVm.ConstructionDesignList = db.ConstructionDesigns.ToList();
                constructionDesignVm.NewConstructionDesign= new ConstructionDesign();
            }
            return View(constructionDesignVm);
        }
        [HttpPost]
        public IActionResult Index(ConstructionDesignViewModel constructionDesignVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ConstructionDesignDBContext())
                {
                    db.ConstructionDesigns.Add(constructionDesignVm.NewConstructionDesign);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            ConstructionDesignViewModel constructionDesignVm = new ConstructionDesignViewModel();
            using (ConstructionDesignDBContext db = new ConstructionDesignDBContext())
            {

                constructionDesignVm.NewConstructionDesign = db.ConstructionDesigns.Where(
                    e => e.ConstructionDesignId == id).SingleOrDefault();

                return View(constructionDesignVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(ConstructionDesignViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (ConstructionDesignDBContext db = new ConstructionDesignDBContext())
                {

                    ConstructionDesign constructionDesign = obj.NewConstructionDesign;

                    constructionDesign.ConstructionDesignId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(constructionDesign).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            ConstructionDesignViewModel constructionDesignVm = new ConstructionDesignViewModel();
            using (ConstructionDesignDBContext db = new ConstructionDesignDBContext())
            {
                using (var dbB = new BridgeDBContext())
                {
                    BridgeViewModel bridgeVm = new BridgeViewModel();
                    bridgeVm.BridgeList = dbB.Bridges.ToList();
                    bridgeVm.NewBridge = dbB.Bridges.Where(
                    e => e.ConstructionDesignId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (bridgeVm.NewBridge == null)
                    {
                        constructionDesignVm.NewConstructionDesign = new ConstructionDesign();
                        //retrieve primary key/id from route data
                        constructionDesignVm.NewConstructionDesign.ConstructionDesignId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(constructionDesignVm.NewConstructionDesign).State =
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
