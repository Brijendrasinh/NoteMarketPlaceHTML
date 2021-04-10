using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using WebApplication5MVCdemo.CommanClasses;

namespace WebApplication5MVCdemo.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();

        public ActionResult ManageAdministrator()
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                int SuperAdmin = Convert.ToInt32(Enums.UserRoleId.SuperAdmin);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    if (user.RoleID == SuperAdmin)
                    {
                        List<ManageAdministratorViewModel> Model = new List<ManageAdministratorViewModel>();
                        Model = (from u in db.Users
                                 join up in db.UserProfiles on u.ID equals up.UserID
                                 where (u.RoleID == 2)
                                 select new ManageAdministratorViewModel()
                                 {
                                     ID = u.ID,
                                     Email = u.EmailID,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     PhoneNumber = up.PhoneNumber,
                                     CountryCode = up.CountryCode,
                                     Active = u.IsActive,
                                     DateAdded = (DateTime)u.CreatedDate,
                                 }).ToList();
                        return View(Model);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }

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

        public ActionResult AddAdministrator(int? ID)
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                int SuperAdmin = Convert.ToInt32(Enums.UserRoleId.SuperAdmin);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    if (user.RoleID == SuperAdmin)
                    {
                        ManageAdministratorViewModel Model = new ManageAdministratorViewModel();
                        Model.countries = db.Countries.Where(x => x.IsActive == true).ToList();

                        if (ID != null)
                        {
                            User user1 = db.Users.Where(x => x.ID == ID).FirstOrDefault();
                            UserProfile userProfile = db.UserProfiles.Where(x => x.UserID == ID).FirstOrDefault();
                            Model.FirstName = user1.FirstName;
                            Model.LastName = user1.LastName;
                            Model.Email = user1.EmailID;
                            Model.CountryCode = userProfile.CountryCode;
                            Model.PhoneNumber = userProfile.PhoneNumber;
                            return View(Model);
                        }
                        return View(Model);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }

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
        public ActionResult AddAdministrator(ManageAdministratorViewModel model)
        {
            int AddedBy = Convert.ToInt32(Session["ID"]);
            User user = db.Users.Where(x => x.EmailID.Equals(model.Email)).FirstOrDefault();
            if (user != null)
            {
                UserProfile userProfile = db.UserProfiles.Where(x => x.UserID == user.ID).FirstOrDefault();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.ModifiedBy = AddedBy;
                user.ModifiedDate = DateTime.Now;
                user.IsActive = true;
                if (userProfile != null)
                {
                    userProfile.CountryCode = model.CountryCode;
                    userProfile.PhoneNumber = model.PhoneNumber;
                    userProfile.ModifiedDate = DateTime.Now;
                    userProfile.ModifiedBy = AddedBy;
                    db.SaveChanges();
                }
                else
                {
                    UserProfile NewEntry = new UserProfile()
                    {
                        UserID = user.ID,
                        PhoneNumber = model.PhoneNumber,
                        AddressLine1 = "Admin",
                        AddressLine2 = "Admin",
                        City = "Admin",
                        State = "Admin",
                        ZipCode = "Admin",
                        Country = "Admin",
                        CountryCode = model.CountryCode,
                        CreatedDate = DateTime.Now,
                        CreatedBy = AddedBy,
                    };
                    db.UserProfiles.Add(NewEntry);
                    db.SaveChanges();
                }
                db.SaveChanges();
            }
            else
            {
                int Admin = Convert.ToInt32(Enums.UserRoleId.Admin);
                User NewEntry = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailID = model.Email,
                    RoleID = Admin,
                    CreatedBy = AddedBy,
                    CreatedDate = DateTime.Now,
                    IsEmailVerified = true,
                    Password = "admin",
                    IsActive = true,
                };
                db.Users.Add(NewEntry);
                db.SaveChanges();
                UserProfile NewProfile = new UserProfile()
                {
                    UserID = NewEntry.ID,
                    PhoneNumber = model.PhoneNumber,
                    CountryCode = model.CountryCode,
                    AddressLine1 = "Admin",
                    AddressLine2 = "Admin",
                    City = "Admin",
                    State = "Admin",
                    ZipCode = "Admin",
                    Country = "Admin",
                    CreatedDate = DateTime.Now,
                    CreatedBy = AddedBy,

                };
                db.UserProfiles.Add(NewProfile);
                db.SaveChanges();
            }
            return RedirectToAction("ManageAdministrator", "Administrator");
        }
        public ActionResult DeleteAdministrator(int? ID)
        {
            if (ID != null)
            {
                User countryData = db.Users.Where(x => x.ID == ID).FirstOrDefault();
                countryData.IsActive = false;
                db.SaveChanges();
            }
            return RedirectToAction("ManageAdministrator", "Administrator");
        }
    }
}