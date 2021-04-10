using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using WebApplication5MVCdemo.CommanClasses;


namespace NoteMarketPlace.Controllers
{
    public class AdminController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: Admin
        public ActionResult Dashboard()
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    int statusInReview = Convert.ToInt32(Enums.ReferenceNoteStatus.InReview);
                    int statusSubmittedForReview = Convert.ToInt32(Enums.ReferenceNoteStatus.SubmittedForReview);
                    int member = Convert.ToInt32(Enums.UserRoleId.Member);

                    AdminDashboardViewModel Model = new AdminDashboardViewModel();
                    Model.NotesUnderReviewCount = db.SellNotes.Where(x => x.Status == (statusInReview) || x.Status == (statusSubmittedForReview)).Count();

                    var date = DateTime.Now.AddDays(-7).Date;
                    Model.NewDownloadCount = db.Downloads.Where(x => x.CreatedDate > date).Count();

                    Model.NewRegistrationCount = db.Users.Where(x => x.CreatedDate > date && x.RoleID == member).Count();

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

        public ActionResult GetFilterDashboard(int month = 0)
        {
            AdminDashboardViewModel Model = new AdminDashboardViewModel();
            
            Model.getDahboardData_Results = db.NewGetDashboadrd(month).ToList();

            return PartialView("_Dashboard", Model);
            
        }

        public ActionResult PublishedNotes(int? sellerid)
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    PublishedNotesViewModel Model = new PublishedNotesViewModel();
                    Model.SellerName = db.SellNotes.Where(x => x.IsActive == true).Select(x => new SelectListItem()
                    {
                        Value = x.SellerID.ToString(),
                        Text = x.User.FirstName + " " + x.User.LastName
                    }).Distinct().ToList();
                    if (sellerid != null)
                    {
                        Model.Seller = (int)sellerid;
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

        public ActionResult GetFilterSellerPublishedNotes(string SellerId = null)
        {
            if (string.IsNullOrEmpty(SellerId))
                SellerId = null;
            PublishedNotesViewModel Model = new PublishedNotesViewModel();
            List<GetPublishedNotesData_Result> getData = db.GetPublishedNotesData(SellerId).ToList();
            Model.getPublishedNotesData_Results = getData;
            return PartialView("_PublishedNotes", Model);
        }

        public ActionResult RejectedNotes(int? sellerid)
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    RejectedNotesViewModel Model = new RejectedNotesViewModel();
                    Model.SellerName = db.SellNotes.Where(x => x.IsActive ==true).Select(x => new SelectListItem()
                    {
                        Value = x.SellerID.ToString(),
                        Text = x.User.FirstName + " " + x.User.LastName
                    }).Distinct().ToList();
                    if (sellerid != null)
                    {
                        Model.Seller = (int)sellerid;
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

        public ActionResult GetFilterSellerRejectedNotes(string SellerId = null)
        {
            if (string.IsNullOrEmpty(SellerId))
                SellerId = null;
            RejectedNotesViewModel Model = new RejectedNotesViewModel();
            List<GetRejectedNotesData_Result> getData = db.GetRejectedNotesData(SellerId).ToList();
            Model.getRejectedNotesData_Results = getData;
            return PartialView("_RejectedNotes", Model);
        }

        [HttpPost]
        public ActionResult Unpublish()
        {
            int id = Convert.ToInt32(Request.Form["hiddenId"]);
            SellNote getSellnote = db.SellNotes.Where(x => x.ID == id).FirstOrDefault();
            getSellnote.Status = Convert.ToInt32(Enums.ReferenceNoteStatus.Removed);
            getSellnote.AdminRemarks = Request.Form["remarks"];
            getSellnote.ModifiedDate = DateTime.Now;
            getSellnote.RejectedBy = Convert.ToInt32(Session["ID"]);
            getSellnote.ModifiedBy = Convert.ToInt32(Session["ID"]);
            db.SaveChanges();
            return RedirectToAction("PublishedNotes", "Admin");
        }

        public ActionResult DownloadedNotes(int? noteid, int? sellerid, int? buyerid)
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    DownloadedNotesViewModel Model = new DownloadedNotesViewModel();
                    Model.SellerName = db.Downloads.Select(x => new SelectListItem()
                    {
                        Value = x.Seller.ToString(),
                        Text = x.User.FirstName + " " + x.User.LastName
                    }).Distinct().ToList();
                    Model.NoteTitle = db.Downloads.Select(x => new SelectListItem()
                    {
                        Value = x.NoteID.ToString(),
                        Text = x.SellNote.Title
                    }).Distinct().ToList();
                    Model.BuyerName = db.Downloads.Select(x => new SelectListItem()
                    {
                        Value = x.Downloader.ToString(),
                        Text = x.User1.FirstName + " " + x.User1.LastName
                    }).Distinct().ToList();
                    if (noteid != null)
                    {
                        Model.NoteID = (int)noteid;
                    }
                    if (sellerid != null)
                    {
                        Model.Seller = (int)sellerid;
                    }
                    if (buyerid != null)
                    {
                        Model.Buyer = (int)buyerid;
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

        public ActionResult GetFilterDownloadedNotes(int NoteId = 0, int SellerId = 0, int BuyerId = 0)
        {
            DownloadedNotesViewModel Model = new DownloadedNotesViewModel();
            List<GetDownloadedNotesData_Result> getData = db.GetDownloadedNotesData(NoteId, SellerId, BuyerId).ToList();
            Model.getDownloadedNotesData_Results = getData;
            return PartialView("_DownloadedNotes", Model);
        }
    }
}