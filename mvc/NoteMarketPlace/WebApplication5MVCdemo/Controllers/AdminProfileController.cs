using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.CommanClasses;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.Controllers
{
    public class AdminProfileController : Controller
    {

        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: AdminProfile
        public ActionResult Profiles()
        {

            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    AdminProfileViewModel Model = new AdminProfileViewModel();
                    Model.countries = db.Countries.Where(x => x.IsActive == true).ToList();
                   
                    User userModel = db.Users.Where(x => x.ID == id).FirstOrDefault();

                    Model.user = userModel;

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

        [HttpPost]
        public ActionResult Profiles(AdminProfileViewModel formData, HttpPostedFileBase ProfilePictureFile)
        {
            int userid = Convert.ToInt32(Session["ID"]);
            UserProfile userProfile = db.UserProfiles.Where(x => x.UserID == userid).FirstOrDefault();
            
            string ProfilePictureFileName;

            if (userProfile != null)
            {
                userProfile.CountryCode = formData.userProfile.CountryCode;
                userProfile.ModifiedDate = DateTime.Now;
                userProfile.SecondaryEmailAddress = formData.userProfile.SecondaryEmailAddress;
                userProfile.PhoneNumber = formData.userProfile.PhoneNumber;



                try
                {

                    db.SaveChanges();

                    if (ProfilePictureFile != null && userProfile.ProfilePicture != null)
                    {

                        var fileExtension = Path.GetExtension(ProfilePictureFile.FileName);
                        ProfilePictureFileName = "DP_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + fileExtension;

                        var File = CheckifPathExistForCurrentUser() + userProfile.ProfilePicture;
                        if (System.IO.File.Exists(File))
                        {
                            System.IO.File.Delete(File);
                        }
                        userProfile.ProfilePicture = ProfilePictureFileName;
                        ProfilePictureFileName = CheckifPathExistForCurrentUser() + ProfilePictureFileName;
                        ProfilePictureFile.SaveAs(ProfilePictureFileName);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Profile", "AdminProfile");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                UserProfile userProfiledata = new UserProfile()
                {
                    UserID = userid,
                    PhoneNumber = formData.userProfile.PhoneNumber,
                    SecondaryEmailAddress = formData.userProfile.SecondaryEmailAddress,
                    AddressLine1 = "Admin",
                    AddressLine2 = "Admin",
                    City = "Admin",
                    State = "Admin",
                    ZipCode = "Admin",
                    Country = "Admin",
                    CountryCode = formData.userProfile.CountryCode,
                    CreatedDate = DateTime.Now,
                    CreatedBy = userid,
                };
                db.UserProfiles.Add(userProfiledata);
                db.SaveChanges();
                if (ProfilePictureFile != null )
                {

                    var fileExtension = Path.GetExtension(ProfilePictureFile.FileName);
                    ProfilePictureFileName = "DP_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + fileExtension;
                    userProfiledata.ProfilePicture = ProfilePictureFileName;
                    ProfilePictureFileName = CheckifPathExistForCurrentUser() + ProfilePictureFileName;
                    ProfilePictureFile.SaveAs(ProfilePictureFileName);
                    db.SaveChanges();
                }
                return RedirectToAction("Dashboard", "Admin");
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

    }
}