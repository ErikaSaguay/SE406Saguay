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
    public class MaterialDesignController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            MaterialDesignViewModel materialDesignVm = new MaterialDesignViewModel();
            using (var db = new MaterialDesignDBContext())
            {
                materialDesignVm.MaterialDesignList = db.MaterialDesigns.ToList();
                materialDesignVm.NewMaterialDesign = new MaterialDesign();
            }
            return View(materialDesignVm);
        }
        [HttpPost]
        public IActionResult Index(MaterialDesignViewModel materialDesignVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MaterialDesignDBContext())
                {
                    db.MaterialDesigns.Add(materialDesignVm.NewMaterialDesign);
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            MaterialDesignViewModel materialDesignVm = new MaterialDesignViewModel();
            using (MaterialDesignDBContext db = new MaterialDesignDBContext())
            {

                materialDesignVm.NewMaterialDesign = db.MaterialDesigns.Where(
                    e => e.MaterialDesignId == id).SingleOrDefault();


                return View(materialDesignVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(MaterialDesignViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (MaterialDesignDBContext db = new MaterialDesignDBContext())
                {

                    MaterialDesign materialD = obj.NewMaterialDesign;

                    materialD.MaterialDesignId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(materialD).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            MaterialDesignViewModel materialDesignVm = new MaterialDesignViewModel();
            using (MaterialDesignDBContext db = new MaterialDesignDBContext())
            {
                using (var dbE = new BridgeDBContext())
                {
                    BridgeViewModel bridgeVm = new BridgeViewModel();
                    bridgeVm.BridgeList = dbE.Bridges.ToList();
                    bridgeVm.NewBridge = dbE.Bridges.Where(
                    e => e.MaterialDesignId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (bridgeVm.NewBridge == null)
                    {
                        materialDesignVm.NewMaterialDesign = new MaterialDesign();
                        //retrieve primary key/id from route data
                        materialDesignVm.NewMaterialDesign.MaterialDesignId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(materialDesignVm.NewMaterialDesign).State =
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
