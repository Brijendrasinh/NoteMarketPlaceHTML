using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using WebApplication5MVCdemo.CommanClasses;

namespace WebApplication5MVCdemo.Controllers
{
    public class CountryController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: Country
        public ActionResult ManageCountry()
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    ManageCountryViewModel Model = new ManageCountryViewModel();
                    Model.getCountryData_Results = db.GetCountryData().ToList();
                    return View(Model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult AddCountry(int? ID)
        {

            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    ManageCountryViewModel Model = new ManageCountryViewModel();
                    if (ID != null)
                    {
                        Country countryData = db.Countries.Where(x => x.ID == ID).FirstOrDefault();
                        Model.CountryName = countryData.Name;
                        Model.CountryCode = countryData.CountryCode;
                        return View(Model);
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult AddCountry(ManageCountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Country countryData = db.Countries.Where(x => x.CountryCode.Equals(model.CountryCode)).FirstOrDefault();
                int AddedBy = Convert.ToInt32(Session["ID"]);
                if (countryData != null)
                {
                    countryData.Name = model.CountryName;
                    countryData.CountryCode = model.CountryCode;
                    countryData.ModifiedDate = DateTime.Now;
                    countryData.ModifiedBy = AddedBy;
                    countryData.IsActive = true;
                    db.SaveChanges();
                }
                else
                {
                    Country NewEntry = new Country()
                    {
                        Name = model.CountryName,
                        CountryCode = model.CountryCode,
                        CreatedBy = AddedBy,
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                    };
                    db.Countries.Add(NewEntry);
                    db.SaveChanges();
                }
                return RedirectToAction("ManageCountry", "Country");
            }
            else
            {
                return View(model);
            }
            
        }
        public ActionResult DeleteCountry(int? ID)
        {
            if (ID != null)
            {
                Country countryData = db.Countries.Where(x => x.ID == ID).FirstOrDefault();
                countryData.IsActive = false;
                db.SaveChanges();
            }
            return RedirectToAction("ManageCountry", "Country");
        }
    }
}