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
    public class AddressController : Controller
    {
        private UserContext db = new UserContext();
        private string defaultErrorMessage = "Unable to save changes.";

        public ActionResult Create(int userID, bool? errorOccurred)
        {
            if (errorOccurred.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = defaultErrorMessage;
            }
            ViewBag.UserID = userID;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Address address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    address.DateAdded = DateTime.Now;

                    db.Addresses.Add(address);
                    db.SaveChanges();

                    TempData["DataUrl"] = "data-url=/User/Edit/" + address.UserID.ToString();
                    return RedirectToAction("Edit", "User", new { id = address.UserID });
                }
            }
            catch
            {
                TempData["DataUrl"] = "data-url=/Address/Create";
                return RedirectToAction("Create", "Address", new { errorOccurred = true });
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", address.UserID);
            return View(address);
        }

        public ActionResult Edit(int id, bool? errorOccurred)
        {
            Address address = db.Addresses.Find(id);
            if (errorOccurred.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = defaultErrorMessage;
            }
            return View(address);
        }

        [HttpPost]
        public ActionResult Edit(Address address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    address.DateModified = DateTime.Now;

                    db.Entry(address).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["DataUrl"] = "data-url=/User/Edit/" + address.UserID;
                    return RedirectToAction("Edit", "User", new { id = address.UserID });
                }
            }
            catch (DataException)
            {
                TempData["DataUrl"] = "data-url=/User/Edit/" + address.AddressID;
                return RedirectToAction("Edit", "User", new { id = address.AddressID, errorOccurred = true });
            }
            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(FormCollection formCollection)
        {
            int addressID = 0;
            int userID = 0;

            try
            {
                foreach (string _formData in formCollection)
                {
                    switch (_formData)
                    {
                        case "AddressID":
                            addressID = int.Parse(formCollection[_formData]);
                            break;
                        case "UserID":
                            userID = int.Parse(formCollection[_formData]);
                            break;
                    }
                }

                Address address = db.Addresses.Find(addressID);
                address.DateModified = DateTime.Now;
                address.IsDeleted = true;
                db.SaveChanges();

            }
            catch (DataException)
            {
                TempData["DataUrl"] = "data-url=/Address/Edit/" + addressID;
                return RedirectToAction("Edit", "Address", new { id = addressID, errorOccurred = true });
            }

            TempData["DataUrl"] = "data-url=/User/Edit/" + userID;
            return RedirectToAction("Edit", "User", new { id = userID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}