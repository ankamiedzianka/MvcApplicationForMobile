using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplicationForMobile.Models;

namespace MvcApplicationForMobile.Controllers
{
    public class UserController : Controller
    {
        private UserContext db = new UserContext();
        private string defaultErrorMessage = "Unable to save changes.";

        public ViewResult Index(bool? errorOccurred)
        {
            if (errorOccurred.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = defaultErrorMessage;
            }
            return View(db.Users.Where(u=> u.IsDeleted == false || u.IsDeleted == null).OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToList());
        }

        public ActionResult Create(bool? errorOccurred)
        {
            if (errorOccurred.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = defaultErrorMessage;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.DateAdded = DateTime.Now;

                    db.Users.Add(user);
                    db.SaveChanges();

                    TempData["DataUrl"] = "data-url=/User";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["DataUrl"] = "data-url=/User/Create";
                return RedirectToAction("Index", "User", new { errorOccurred = true });
            }
            return View(user);
        }

        public ActionResult Edit(int id, bool? errorOccurred)
        {
            User user = db.Users.Find(id);
            
            if (errorOccurred.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = defaultErrorMessage;
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.DateModified = DateTime.Now;

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["DataUrl"] = "data-url=/User";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["DataUrl"] = "data-url=/User/Edit/" + user.UserID;
                return RedirectToAction("Edit", "User", new { id = user.UserID, errorOccurred = true });
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(FormCollection formCollection)
        {
            int userID = 0;

            try
            {
                foreach (string _formData in formCollection)
                {
                    if (_formData == "UserToDeleteID")
                    {
                        userID = int.Parse(formCollection[_formData]);
                        break;
                    }
                }

                User user = db.Users.Find(userID);
                user.IsDeleted = true;
                user.DateModified = DateTime.Now;
                db.SaveChanges();

            }
            catch
            {
                TempData["DataUrl"] = "data-url=/User/Edit/" + userID;
                return RedirectToAction("Edit", "User", new { id = userID, errorOccurred = true });
            }

            TempData["DataUrl"] = "data-url=/User";
            return RedirectToAction("Index", "User");
        }

        public ActionResult Problem()
        {
            return View("Error");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}