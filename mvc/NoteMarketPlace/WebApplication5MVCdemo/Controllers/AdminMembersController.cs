using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.CommanClasses;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.Controllers
{
    public class AdminMembersController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: AdminMembers
        public ActionResult Members()
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    AdminMembersViewModel Model = new AdminMembersViewModel();
                    List<NewGetMembersData_Result> data = db.NewGetMembersData().ToList();
                    Model.getMembersData_Results = data;
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

        public ActionResult MemberDetails(int? SellerID)
        {

            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    User userData = db.Users.Where(x => x.ID == SellerID).FirstOrDefault();
                    UserProfile userProfileData = db.UserProfiles.Where(x => x.UserID == SellerID).FirstOrDefault();
                    AdminMemberDetailsViewModel Model = new AdminMemberDetailsViewModel();
                    if (userData != null)
                    {
                        Model.user = userData;
                    }
                    if (userProfileData != null)
                    {
                        Model.userProfile = userProfileData;

                    }
                    if (SellerID != null)
                    {
                        List<NewGetMembersDetails_Result> getData = db.NewGetMembersDetails(SellerID).ToList();
                        Model.getMembersDetails_Results = getData;
                        Model.SellerID = (int)SellerID;
                    }

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

        public ActionResult Deactivate(int? SellerID)
        {
            User user = db.Users.Where(x => x.ID == (int)SellerID).FirstOrDefault();
            user.IsActive = false;
            List<SellNote> notes = db.SellNotes.Where(x => x.SellerID == SellerID).ToList();
            foreach (var data in notes)
            {
                data.Status = Convert.ToInt32(Enums.ReferenceNoteStatus.Removed);
            }
            db.SaveChanges();
            return RedirectToAction("Members", "AdminMembers");
        }
    }
}