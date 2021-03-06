﻿using System;
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
    public class UserController : Controller
    {
        private IUnitOfWork unitOfWork;
        private string defaultErrorMessage = "Error occurred.";

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ViewResult Index(bool? errorOccurred)
        {
            if (errorOccurred.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = defaultErrorMessage;
            }

            var users = unitOfWork.UserRepository.GetUsers();

            return View("Index",users);
        }

        public ViewResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.UserRepository.InsertUser(user);
                    unitOfWork.Save();

                    TempData["DataUrl"] = "data-url=/User";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, defaultErrorMessage);
            }
            return View("Create",user);
        }

        public ActionResult Edit(int id, bool? errorOccurred)
        {
            if (id == 0)
            {
                TempData["DataUrl"] = "data-url=/User";
                return RedirectToAction("Index", "User", new { errorOccurred = true });
            }

            User user = unitOfWork.UserRepository.GetUserByID(id);

            if (errorOccurred.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = defaultErrorMessage;
            }

            if (user.IsDeleted == true)
            {
                ModelState.AddModelError(string.Empty, "The record has been already deleted by another user. Click the Back button to come back to the list.");
                ViewBag.ButtonsDisabled = true;
            }

            return View("Edit",user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.UserRepository.UpdateUser(user);
                    unitOfWork.Save();

                    TempData["DataUrl"] = "data-url=/User";
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (User)entry.GetDatabaseValues().ToObject();
                var clientValues = (User)entry.Entity;

                if (databaseValues.IsDeleted == true)
                {
                    ModelState.AddModelError(string.Empty, "The record has been already deleted by another user. Click the Back button to come back to the list.");
                    ViewBag.ButtonsDisabled = true;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The record was modified by another user. "
                    + "Please confirm the Save operation or click the Back button.");

                    if (databaseValues.FirstName != clientValues.FirstName)
                        ModelState.AddModelError("FirstName", "Current value: " + databaseValues.FirstName);
                    if (databaseValues.LastName != clientValues.LastName)
                        ModelState.AddModelError("LastName", "Current value: " + databaseValues.LastName);
                    if (databaseValues.Email != clientValues.Email)
                        ModelState.AddModelError("Email", "Current value: " + databaseValues.Email);
                }

                user.Timestamp = databaseValues.Timestamp;

                return View("Edit", user);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, defaultErrorMessage);
            }
            return View("Edit",user);
        }

        public ViewResult Delete()
        {
            return View("Delete");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(FormCollection formCollection)
        {
            int userID = 0;
            Byte[] timestamp = new Byte[8];
            bool deleteConfirmed = false;

            User user = new User();

            try
            {
                foreach (string _formData in formCollection)
                {
                    switch (_formData)
                    {
                        case "UserToDeleteID":
                            userID = int.Parse(formCollection[_formData]);
                            break;
                        case "TimestampToDelete":
                            timestamp = Convert.FromBase64String(formCollection[_formData]);
                            break;
                        case "DeleteConfirmed":
                            deleteConfirmed = true;
                            break;
                    }
                }

                user = unitOfWork.UserRepository.GetUserByID(userID);

                if (user.IsDeleted == true)
                {
                    ModelState.AddModelError(string.Empty, "The record has been already deleted by another user. Click 'Back' button to come back to list.");
                    ViewBag.ButtonsDisabled = true;
                    return View("Edit", user);
                }

                if ((!user.Timestamp.SequenceEqual(timestamp)) && (!deleteConfirmed))
                {
                    ModelState.AddModelError(string.Empty, "The record was modified by another user. "
                        + "Please confirm the Delete operation or click the Back button.");
                    ViewBag.ButtonsDisabled = true;
                    ViewBag.ConfirmDeletionButton = true;
                    return View("Edit", user);
                }

                user.Timestamp = timestamp;

                unitOfWork.UserRepository.DeleteUser(user.UserID);
                unitOfWork.Save();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (User)entry.GetDatabaseValues().ToObject();
                var clientValues = (User)entry.Entity;

                ModelState.AddModelError(string.Empty, "The record was modified by another user. "
                + "The current values have been displayed. "
                + "Please confirm the Delete operation or click the Back button.");

                user.Timestamp = databaseValues.Timestamp;

                return View("Edit", user);

            }
            catch
            {
                TempData["DataUrl"] = "data-url=/User";
                return RedirectToAction("Index", "User", new { errorOccurred = true });
            }

            TempData["DataUrl"] = "data-url=/User";
            return RedirectToAction("Index", "User");
        }

        public ViewResult Problem()
        {
            return View("Error");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}