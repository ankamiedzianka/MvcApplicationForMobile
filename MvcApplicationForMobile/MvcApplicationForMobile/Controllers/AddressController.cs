using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplicationForMobile.Models;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Data.Entity.Validation;
using MvcApplicationForMobile.DAL;

namespace MvcApplicationForMobile.Controllers
{
    public class AddressController : Controller
    {
        private IAddressRepository addressRepository;
        private string defaultErrorMessage = "Error occurred.";

         public AddressController()
        {
            this.addressRepository = new AddressRepository(new UserContext());
        }
        public AddressController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

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
                    addressRepository.InsertAddress(address);
                    addressRepository.Save();

                    TempData["DataUrl"] = "data-url=/User/Edit/" + address.UserID.ToString();
                    return RedirectToAction("Edit", "User", new { id = address.UserID });
                }
            }
            catch
            {
                TempData["DataUrl"] = "data-url=/Address/Create";
                return RedirectToAction("Create", "Address", new { errorOccurred = true });
            }

            ViewBag.UserID = address.UserID;
            return View(address);
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                TempData["DataUrl"] = "data-url=/User";
                return RedirectToAction("Index", "User", new { errorOccurred = true });
            }

            Address address = addressRepository.GetAddressByID(id);

            if (address.IsDeleted == true)
            {
                ModelState.AddModelError(string.Empty, "The record has been already deleted by another user. Click the Back button.");
                ViewBag.ButtonsDisabled = true;
            }

            return View(address);
        }

        [HttpPost]
        public ActionResult Edit(Address address)
        {
            try
            {
                //address.User = 
                  //  db.Users.Where(u => u.UserID == address.UserID).Single();

                if (ModelState.IsValid)
                {

                    addressRepository.UpdateAddress(address);
                    addressRepository.Save();

                    TempData["DataUrl"] = "data-url=/User/Edit/" + address.UserID;
                    return RedirectToAction("Edit", "User", new { id = address.UserID });
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (Address)entry.GetDatabaseValues().ToObject();
                var clientValues = (Address)entry.Entity;

                if (databaseValues.IsDeleted == true)
                {
                    ModelState.AddModelError(string.Empty, "The record has been already deleted by another user. Click the Back button.");
                    ViewBag.ButtonsDisabled = true;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The record was modified by another user. "
                    + "Please confirm the Save operation or click the Back button.");

                    if (databaseValues.Country != clientValues.Country)
                        ModelState.AddModelError("Country", "Current value: " + databaseValues.Country);
                    if (databaseValues.Line1 != clientValues.Line1)
                        ModelState.AddModelError("Line1", "Current value: " + databaseValues.Line1);
                    if (databaseValues.Line2 != clientValues.Line2)
                        ModelState.AddModelError("Line2", "Current value: " + databaseValues.Line2);
                    if (databaseValues.PostCode != clientValues.PostCode)
                        ModelState.AddModelError("PostCode", "Current value: " + databaseValues.PostCode);
                }

                address.Timestamp = databaseValues.Timestamp;

                return View("Edit", address);
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, defaultErrorMessage);
            }
            return View(address);
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(FormCollection formCollection)
        {
            int addressID = 0;
            
            Byte[] timestamp = new Byte[8];
            bool deleteConfirmed = false;

            Address address = new Address();

            try
            {
                foreach (string _formData in formCollection)
                {
                    switch (_formData)
                    {
                        case "AddressToDeleteID":
                            addressID = int.Parse(formCollection[_formData]);
                            break;
                        case "TimestampAddressToDelete":
                            timestamp = Convert.FromBase64String(formCollection[_formData]);
                            break;
                        case "DeleteConfirmed":
                            deleteConfirmed = true;
                            break;
                    }
                }

                address = addressRepository.GetAddressByID(addressID);

                if (address.IsDeleted == true)
                {
                    ModelState.AddModelError(string.Empty, "The record has been already deleted by another user. Click 'Back' button.");
                    ViewBag.ButtonsDisabled = true;
                    return View("Edit", address);
                }

                if ((!address.Timestamp.SequenceEqual(timestamp)) && (!deleteConfirmed))
                {
                    ModelState.AddModelError(string.Empty, "The record was modified by another user. "
                        + "Please confirm the Delete operation or click the Back button.");
                    ViewBag.ButtonsDisabled = true;
                    ViewBag.ConfirmDeletionButton = true;
                    return View("Edit", address);
                }

                address.Timestamp = timestamp;

                addressRepository.DeleteAddress(addressID);
                addressRepository.Save();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (Address)entry.GetDatabaseValues().ToObject();
                var clientValues = (Address)entry.Entity;

                ModelState.AddModelError(string.Empty, "The record was modified by another user. "
                + "The current values have been displayed. "
                + "Please confirm the Delete operation or click the Back button.");

                address.Timestamp = databaseValues.Timestamp;

                return View("Edit", address);

            }
            catch
            {
                TempData["DataUrl"] = "data-url=/Address/Edit/" + addressID;
                return RedirectToAction("Edit", "Address", new { id = addressID, errorOccurred = true });
            }

            TempData["DataUrl"] = "data-url=/User/Edit/" + address.User.UserID;
            return RedirectToAction("Edit", "User", new { id = address.User.UserID });
        }

        protected override void Dispose(bool disposing)
        {
            addressRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}