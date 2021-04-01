using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.CommanClasses;
using WebApplication5MVCdemo.Models;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace NoteMarketPlace.Controllers
{
    public class ProfileController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: Profile
        public ActionResult MyProfile()
        {
            if (Session["EmailID"] != null)
            {
                var countries = db.Countries.Where(x => x.IsActive == true).ToList();
                var genders = db.Genders.ToList();

                var viewModel = new UserProfileCombine
                {
                    Countries = countries,
                    Genders = genders
                };
                viewModel.Email = Session["EmailID"].ToString();

                User user = db.Users.Where(x => x.EmailID.Equals(viewModel.Email)).FirstOrDefault();
                viewModel.FirstName = user.FirstName;
                ViewBag.firstName = user.FirstName;
                viewModel.LastName = user.LastName;
                ViewBag.lastName = user.LastName;
                ViewBag.emailid = user.EmailID;
                UserProfile userProfile = new UserProfile();
                userProfile.UserID = Convert.ToInt32(Session["ID"]);
                userProfile = db.UserProfiles.Where(x => x.UserID.Equals(userProfile.UserID)).FirstOrDefault();
                viewModel.UserProfile = userProfile;

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult MyProfile(UserProfileCombine user, HttpPostedFileBase ProfilePictureFile)
        {
            user.UserProfile.UserID = Convert.ToInt32(Session["ID"]);
            UserProfile userProfile = db.UserProfiles.Where(x => x.UserID == (user.UserProfile.UserID)).FirstOrDefault();
            if(userProfile == null)
            {
                string ProfilePictureFileName;
                var userProfileDetail = new UserProfile()
                {
                    AddressLine1 = user.UserProfile.AddressLine1,
                    AddressLine2 = user.UserProfile.AddressLine2,
                    City = user.UserProfile.City,
                    College = user.UserProfile.College,
                    Country = user.UserProfile.Country,
                    CountryCode = user.UserProfile.CountryCode,
                    CreatedDate = DateTime.Now,
                    DOB = user.UserProfile.DOB,
                    Gender = user.UserProfile.Gender,
                    PhoneNumber = user.UserProfile.PhoneNumber,
                    State = user.UserProfile.State,
                    University = user.UserProfile.University,
                    UserID = Convert.ToInt32(Session["ID"]),
                    ZipCode = user.UserProfile.ZipCode
                };

                try
                {
                    db.UserProfiles.Add(userProfileDetail);
                    db.SaveChanges();
                    
                    if (ProfilePictureFile != null)
                    {

                        var fileExtension = Path.GetExtension(ProfilePictureFile.FileName);
                        ProfilePictureFileName = "DP_" + fileExtension;
                        userProfileDetail.ProfilePicture = ProfilePictureFileName;
                        ProfilePictureFileName = CheckifPathExistForCurrentUser() + ProfilePictureFileName;
                        
                        ProfilePictureFile.SaveAs(ProfilePictureFileName);
                        db.SaveChanges();
                    }

                    return RedirectToAction("MyProfile", "Profile");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                string ProfilePictureFileName;

                userProfile.AddressLine1 = user.UserProfile.AddressLine1;
                userProfile.AddressLine2 = user.UserProfile.AddressLine2;
                userProfile.City = user.UserProfile.City;
                userProfile.College = user.UserProfile.College;
                userProfile.Country = user.UserProfile.Country;
                userProfile.CountryCode = user.UserProfile.CountryCode;
                userProfile.CreatedDate = DateTime.Now;
                userProfile.DOB = user.UserProfile.DOB;
                userProfile.Gender = user.UserProfile.Gender;
                userProfile.PhoneNumber = user.UserProfile.PhoneNumber;
                userProfile.State = user.UserProfile.State;
                userProfile.University = user.UserProfile.University;
                userProfile.ZipCode = user.UserProfile.ZipCode;
                

                try
                {
                   
                    db.SaveChanges();
                   
                    if (ProfilePictureFile != null && userProfile.ProfilePicture != null)
                    {

                        var fileExtension = Path.GetExtension(ProfilePictureFile.FileName);
                        ProfilePictureFileName = "DP_" + fileExtension;

                        
                        if (System.IO.File.Exists(userProfile.ProfilePicture))
                        {
                            System.IO.File.Delete(userProfile.ProfilePicture);
                        }

                        userProfile.ProfilePicture = ProfilePictureFileName;
                        ProfilePictureFileName = CheckifPathExistForCurrentUser() + ProfilePictureFileName;
                        ProfilePictureFile.SaveAs(ProfilePictureFileName);
                        db.SaveChanges();
                    }

                    return RedirectToAction("MyProfile", "Profile");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
        }

        public String CheckifPathExistForCurrentUser()
        {
            // Check Path for specific user exist or not. If not exists then create
            var pathofUploadImage = Server.MapPath(ConstantStrings.Notes_Upload_Path + Session["ID"].ToString() + "/");
            if (!System.IO.File.Exists(pathofUploadImage))
            {
                DirectorySecurity sec = new DirectorySecurity();
                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                Directory.CreateDirectory(pathofUploadImage, sec);

            }
            return pathofUploadImage;
        }

        public ActionResult MyDownloads()
        {
            if(Session["ID"] != null)
            {
                int ID = Convert.ToInt32(Session["ID"]);
                var user = db.Downloads.Where(x => x.Downloader == ID && x.IsSellerHasAllowedDownloads == true).ToList();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult MySoldNotes()
        {
            if (Session["ID"] != null)
            {
                int ID = Convert.ToInt32(Session["ID"]);
                var user = db.Downloads.Where(x => x.Seller == ID).GroupBy(x => new { x.NoteID, x.Downloader}).Select(x => x.FirstOrDefault()).ToList();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult MyRejectedNotes()
        {
            if(Session["ID"] != null)
            {
                int ID = Convert.ToInt32(Session["ID"]);
                int status = Convert.ToInt32(Enums.ReferenceNoteStatus.Rejected);
                var user = db.SellNotes.Where(x => x.SellerID == ID && x.Status == status).ToList();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult Rating()
        {
            var star = Convert.ToDecimal(Request.Form["rate"]);
            var comment = Request.Form["Comment"];
            var noteID = Convert.ToInt32(Request.Form["hiddenNoteid"]);
            var ID = Convert.ToInt32(Request.Form["hiddenId"]);
            int ReviewerId = Convert.ToInt32(Session["ID"]); 
            NoteReview noteReview = new NoteReview()
            {
                NoteID = noteID,
                DownloadsID = ID,
                Ratings = star,
                Comments =comment,
                ReviewedByID = ReviewerId,
                CreatedDate = DateTime.Now,
            };
            db.NoteReviews.Add(noteReview);
            db.SaveChanges();
            noteReview.CreatedBy = noteReview.ID;
            db.SaveChanges();
            return RedirectToAction("MyDownloads", "Profile");
        }

        public ActionResult ReportNote()
        {
            var comment = Request.Form["Remark"];
            var noteID = Convert.ToInt32(Request.Form["hiddenNoteid"]);
            var ID = Convert.ToInt32(Request.Form["hiddenId"]);
            int ReportersId = Convert.ToInt32(Session["ID"]);
            NoteReport noteReport = new NoteReport()
            {
                NoteID = noteID,
                DownloaderID = ID,
                Remarks = comment,
                ReportedByID = ReportersId,
                CreatedDate = DateTime.Now,
            };
            db.NoteReports.Add(noteReport);
            db.SaveChanges();
            noteReport.CreatedBy = noteReport.ID;
            db.SaveChanges();
            return RedirectToAction("MyDownloads", "Profile");
        }
    }
}