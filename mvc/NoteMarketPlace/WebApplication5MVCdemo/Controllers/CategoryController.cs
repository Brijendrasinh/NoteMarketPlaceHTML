using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using WebApplication5MVCdemo.CommanClasses;

namespace WebApplication5MVCdemo.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();

        public ActionResult ManageCategory()
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    ManageCategoryViewModel Model = new ManageCategoryViewModel();
                    Model.getCategoryData_Results = db.GetCategoryData().ToList();
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

        public ActionResult AddCategory(int? ID)
        {

            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    ManageCategoryViewModel Model = new ManageCategoryViewModel();
                    if (ID != null)
                    {
                        NoteCategory countryData = db.NoteCategories.Where(x => x.ID == ID).FirstOrDefault();
                        Model.CategoryName = countryData.Name;
                        Model.Description = countryData.Description;
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
        public ActionResult AddCategory(ManageCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                NoteCategory countryData = db.NoteCategories.Where(x => x.Name.Equals(model.CategoryName)).FirstOrDefault();
                int AddedBy = Convert.ToInt32(Session["ID"]);
                if (countryData != null)
                {
                    countryData.Name = model.CategoryName;
                    countryData.Description = model.Description;
                    countryData.ModifiedDate = DateTime.Now;
                    countryData.ModifiedBy = AddedBy;
                    countryData.IsActive = true;
                    db.SaveChanges();
                }
                else
                {
                    NoteCategory NewEntry = new NoteCategory()
                    {
                        Name = model.CategoryName,
                        Description = model.Description,
                        CreatedBy = AddedBy,
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                    };
                    db.NoteCategories.Add(NewEntry);
                    db.SaveChanges();
                }
                return RedirectToAction("ManageCategory", "Category");
            }
            else
            {
                return View(model);
            }
            
        }
        public ActionResult DeleteCategory(int? ID)
        {
            if (ID != null)
            {
                NoteCategory countryData = db.NoteCategories.Where(x => x.ID == ID).FirstOrDefault();
                countryData.IsActive = false;
                db.SaveChanges();
            }
            return RedirectToAction("ManageCategory", "Category");
        }
    }
}