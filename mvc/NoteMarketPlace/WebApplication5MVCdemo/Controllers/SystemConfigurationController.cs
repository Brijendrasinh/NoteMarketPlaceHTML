using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using WebApplication5MVCdemo.CommanClasses;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace WebApplication5MVCdemo.Controllers
{
    public class SystemConfigurationController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: SystemConfiguration
        public ActionResult ManageSystemConfiguration()
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
                        return View();
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
        public ActionResult ManageSystemConfiguration(SystemConfigurationViewModel model,
                                                      HttpPostedFileBase DefaultProfilePicturefile,
                                                      HttpPostedFileBase DefaultNotePicturefile)
        {
            string DefaultProfilePictureFileName;
            string DefaultNotePictureFileName;
            SystemConfiguration supportEmail = db.SystemConfigurations.Where(x => x.Keys.Equals("SupportEmail")).FirstOrDefault();
            supportEmail.Value = model.SupportEmail;
            supportEmail.CreatedDate = DateTime.Now;
            supportEmail.CreatedBy = Convert.ToInt32(Session["ID"]);
            SystemConfiguration SupportPhoneNumber = db.SystemConfigurations.Where(x => x.Keys.Equals("SupportPhoneNumber")).FirstOrDefault();
            SupportPhoneNumber.Value = model.SupportContactNumber;
            SupportPhoneNumber.CreatedDate = DateTime.Now;
            SupportPhoneNumber.CreatedBy = Convert.ToInt32(Session["ID"]);
            if (model.Facebook != null)
            {
                SystemConfiguration Facebook = db.SystemConfigurations.Where(x => x.Keys.Equals("Facebook")).FirstOrDefault();
                Facebook.Value = model.Facebook;
                Facebook.CreatedDate = DateTime.Now;
                Facebook.CreatedBy = Convert.ToInt32(Session["ID"]);
            }
            if (model.Twitter != null)
            {
                SystemConfiguration Twitter = db.SystemConfigurations.Where(x => x.Keys.Equals("Twitter")).FirstOrDefault();
                Twitter.Value = model.Twitter;
                Twitter.CreatedDate = DateTime.Now;
                Twitter.CreatedBy = Convert.ToInt32(Session["ID"]);
            }
            if (model.Linkdin != null)
            {
                SystemConfiguration Linkedin = db.SystemConfigurations.Where(x => x.Keys.Equals("Linkedin")).FirstOrDefault();
                Linkedin.Value = model.Linkdin;
                Linkedin.CreatedDate = DateTime.Now;
                Linkedin.CreatedBy = Convert.ToInt32(Session["ID"]);
            }
            SystemConfiguration DefaultNotePicture = db.SystemConfigurations.Where(x => x.Keys.Equals("DefaultNotePicture")).FirstOrDefault();
            if (DefaultNotePicturefile != null)
            {
                var fileExtension = Path.GetExtension(DefaultNotePicturefile.FileName);
                DefaultNotePictureFileName = "DefaultNotePicture" + fileExtension;

                var File = CheckifPathExistForCurrentUser() + DefaultNotePicture.Value;

                if (System.IO.File.Exists(File))
                {
                    System.IO.File.Delete(File);
                }

                DefaultNotePicture.Value = DefaultNotePictureFileName;
                DefaultNotePicture.CreatedDate = DateTime.Now;
                DefaultNotePicture.CreatedBy = Convert.ToInt32(Session["ID"]);
                DefaultNotePictureFileName = CheckifPathExistForCurrentUser() + DefaultNotePictureFileName;

                DefaultNotePicturefile.SaveAs(DefaultNotePictureFileName);
                db.SaveChanges();
            }
            SystemConfiguration DefaultProfilePicture = db.SystemConfigurations.Where(x => x.Keys.Equals("DefaultProfilePicture")).FirstOrDefault();
            if (DefaultProfilePicturefile != null)
            {
                var fileExtension = Path.GetExtension(DefaultProfilePicturefile.FileName);
                DefaultProfilePictureFileName = "DefaultProfilePicture" + fileExtension;

                var File = CheckifPathExistForCurrentUser() + DefaultProfilePicture.Value;

                if (System.IO.File.Exists(File))
                {
                    System.IO.File.Delete(File);
                }

                DefaultProfilePicture.Value = DefaultProfilePictureFileName;
                DefaultProfilePicture.CreatedDate = DateTime.Now;
                DefaultProfilePicture.CreatedBy = Convert.ToInt32(Session["ID"]);
                DefaultProfilePictureFileName = CheckifPathExistForCurrentUser() + DefaultProfilePictureFileName;

                DefaultProfilePicturefile.SaveAs(DefaultProfilePictureFileName);
                db.SaveChanges();
            }

            return RedirectToAction("ManageSystemConfiguration", "SystemConfiguration");
        }

        public String CheckifPathExistForCurrentUser()
        {
            // Check Path for specific user exist or not. If not exists then create
            var pathofUploadImage = Server.MapPath(ConstantStrings.Notes_Upload_Path + ConstantStrings.Default_Picture_path);
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