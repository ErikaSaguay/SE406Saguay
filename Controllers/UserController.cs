using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ApplicationInsights;

namespace SE406_Saguay
{
    public class UserController :Controller
    {
        public IActionResult Index()
        {
            UserViewModel userVm = new UserViewModel();
            using (var db = new UserDBContext())
            {
                userVm.UserList = db.Users.ToList();
                userVm.NewUser = new User();
            }
            return View(userVm);
        }
        [HttpPost]
        public IActionResult Index(UserViewModel userVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new UserDBContext())
                {
                    db.Users.Add(userVm.NewUser);
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            UserViewModel userVm = new UserViewModel();
            using (UserDBContext db = new UserDBContext())
            {

                userVm.NewUser = db.Users.Where(
                    e => e.UserID == id).SingleOrDefault();


                return View(userVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(UserViewModel obj)
        {

            if (ModelState.IsValid)
            {
                using (UserDBContext db = new UserDBContext())
                {

                    User user = obj.NewUser;

                    user.UserID = Guid.Parse(RouteData.Values["id"].ToString());

                    db.Entry(user).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            UserViewModel userVm = new UserViewModel();
            using (UserDBContext db = new UserDBContext())
            {
                //instantiate object from view model
                userVm.NewUser = new User();
                //retrieve primary key/id from route data
                userVm.NewUser.UserID =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(userVm.NewUser).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
