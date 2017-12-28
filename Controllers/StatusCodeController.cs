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
    public class StatusCodeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            StatusCodeViewModel statusCodeVm = new StatusCodeViewModel();
            using (var db = new StatusCodeDBContext())
            {
                statusCodeVm.StatusCodeList = db.StatusCodes.ToList();
                statusCodeVm.NewStatusCode = new StatusCode();
            }
            return View(statusCodeVm);
        }
        [HttpPost]
        public IActionResult Index(StatusCodeViewModel statusCodeVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new StatusCodeDBContext())
                {
                    db.StatusCodes.Add(statusCodeVm.NewStatusCode);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            StatusCodeViewModel statusCodeVm = new StatusCodeViewModel();
            using (StatusCodeDBContext db = new StatusCodeDBContext())
            {

                statusCodeVm.NewStatusCode = db.StatusCodes.Where(
                    e => e.StatusCodeId == id).SingleOrDefault();


                return View(statusCodeVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(StatusCodeViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (StatusCodeDBContext db = new StatusCodeDBContext())
                {

                    StatusCode statusCode = obj.NewStatusCode;

                    statusCode.StatusCodeId = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(statusCode).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            StatusCodeViewModel statusCodeVm = new StatusCodeViewModel();
            using (StatusCodeDBContext db = new StatusCodeDBContext())
            {
                using (var dbB = new BridgeDBContext())
                {
                    BridgeViewModel bridgeVm = new BridgeViewModel();
                    bridgeVm.BridgeList = dbB.Bridges.ToList();
                    bridgeVm.NewBridge = dbB.Bridges.Where(
                    e => e.StatusId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (bridgeVm.NewBridge == null)
                    {
                        statusCodeVm.NewStatusCode = new StatusCode();
                        //retrieve primary key/id from route data
                        statusCodeVm.NewStatusCode.StatusCodeId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(statusCodeVm.NewStatusCode).State =
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
