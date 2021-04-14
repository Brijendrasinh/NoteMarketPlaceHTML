using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using WebApplication5MVCdemo.CommanClasses;


namespace WebApplication5MVCdemo.Controllers
{
    public class TypeController : Controller
    {
        // GET: Type
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        public ActionResult ManageType()
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    ManageTypeViewModel Model = new ManageTypeViewModel();
                    Model.getNoteTypeData_Results = db.GetNoteTypeData().ToList();
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

        public ActionResult AddType(int? ID)
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    ManageTypeViewModel Model = new ManageTypeViewModel();
                    if (ID != null)
                    {
                        NoteType countryData = db.NoteTypes.Where(x => x.ID == ID).FirstOrDefault();
                        Model.Type = countryData.Name;
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
        public ActionResult AddType(ManageTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                NoteType countryData = db.NoteTypes.Where(x => x.Name.Equals(model.Type)).FirstOrDefault();
                int AddedBy = Convert.ToInt32(Session["ID"]);
                if (countryData != null)
                {
                    countryData.Name = model.Type;
                    countryData.Description = model.Description;
                    countryData.ModifiedDate = DateTime.Now;
                    countryData.ModifiedBy = AddedBy;
                    countryData.IsActive = true;
                    db.SaveChanges();
                }
                else
                {
                    NoteType NewEntry = new NoteType()
                    {
                        Name = model.Type,
                        Description = model.Description,
                        CreatedBy = AddedBy,
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                    };
                    db.NoteTypes.Add(NewEntry);
                    db.SaveChanges();
                }
                return RedirectToAction("ManageType", "Type");
            }
            else
            {
                return View(model);
            }
            
        }
        public ActionResult DeleteType(int? ID)
        {
            if (ID != null)
            {
                NoteType countryData = db.NoteTypes.Where(x => x.ID == ID).FirstOrDefault();
                countryData.IsActive = false;
                db.SaveChanges();
            }
            return RedirectToAction("ManageType", "Type");
        }
    }
}