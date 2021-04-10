using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WebApplication5MVCdemo.CommanClasses;
using System.Data.SqlClient;
using System.Web.Security;

namespace NoteMarketPlace.Controllers
{

    public class AccountController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();

        public ActionResult Login()
        {
            //User UserModel = new User();
            //UserModel.EmailID = "Abc@123gmail.com";
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {

                using (NoteMarketPlaceEntities db = new NoteMarketPlaceEntities())
                {
                    var passwordCorrectOrNot = db.Users.Where(a => a.EmailID.Equals(user.EmailID)).FirstOrDefault();
                    if (passwordCorrectOrNot.Password == user.Password)
                    {
                        var userExistOrNot = db.Users.Where(a => a.EmailID.Equals(user.EmailID) && a.Password.Equals(user.Password) && a.IsEmailVerified == true && a.IsActive == true).FirstOrDefault();
                        if (userExistOrNot != null)
                        {
                            Session["ID"] = userExistOrNot.ID.ToString();
                            Session["EmailID"] = userExistOrNot.EmailID.ToString();


                            //  Remember me functionality is remain 
                            //  FormsAuthentication.SetAuthCookie(user.EmailID,true);

                            UserProfile userProfileUpdated = db.UserProfiles.Where(x => x.UserID == userExistOrNot.ID).FirstOrDefault();
                            if (userExistOrNot.RoleID == Convert.ToInt32(Enums.UserRoleId.SuperAdmin) || userExistOrNot.RoleID == Convert.ToInt32(Enums.UserRoleId.Admin))
                            {
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else if (userExistOrNot.RoleID == Convert.ToInt32(Enums.UserRoleId.Member) && userProfileUpdated != null )
                            {
                                return RedirectToAction("Index", "SearchNotes");
                            }
                            else if (userExistOrNot.RoleID == Convert.ToInt32(Enums.UserRoleId.Member) && userProfileUpdated == null )
                            {
                                return RedirectToAction("MyProfile", "Profile");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("EmailID", "Please Verify your Email Or Sign up first");
                            return View(user);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Please enter correct password");
                        return View(user);
                    }
                }

            }
            return View(user);
        }



        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoginEntity userExist = new LoginEntity();
                    bool IsExist = userExist.EmailExistOrNot(user.EmailID);

                    if (IsExist == true)
                    {
                        //password Generator
                        string numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$%^&*()-=";
                        Random objrandom = new Random();
                        string passwordString = "";
                        string strrandom = string.Empty;
                        for (int i = 0; i < 15; i++)
                        {
                            int temp = objrandom.Next(0, numbers.Length);
                            passwordString = numbers.ToCharArray()[temp].ToString();
                            strrandom += passwordString;
                        }

                        //update password in database
                        var PasswordOfUser = db.Users.Where(x => x.EmailID.Equals(user.EmailID)).ToList().Select(x => x.Password = strrandom);
                        db.SaveChanges();

                        // Email sending
                        var sender = new MailAddress(ConstantStrings.supportEmail, ConstantStrings.supportName);
                        var receiver = new MailAddress(user.EmailID, user.FirstName);
                        var password = ConstantStrings.supportPassword;
                        var body = string.Empty;
                        var subject = "New Temporary Password has been created for you";

                        //string URL = ConfigurationManager.AppSettings["Localhost"] + "Account/VefiryEmail?Email=" + user.EmailID + "";
                        //  string New_URL = ConfigurationManager.AppSettings["Localhost"] + Url.Action("VefiryEmail", "Account", new { Email = model.Email });
                        // verify link problem on email 

                        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/ForgotPasswordMail.html")))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{newPassword}", strrandom);


                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(sender.Address, password)
                        };

                        using (var messege = new MailMessage(sender, receiver)
                        {
                            Body = body,
                            Subject = subject,
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(messege);

                        }

                        ModelState.AddModelError("EmailID", "Please Check your Email ID for Updated Password");
                        return View(user);

                    }
                    else
                    {
                        ModelState.AddModelError("EmailID", "Please Enter Valid Email ID");
                        return View(user);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Login","Account");
        }

        // GET: /Account/Signup
        //[AllowAnonymous]
        public ActionResult Signup()
        {

            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Signup(RegisterUser model)
        {
            // below code is for email sending
            try
            {
                if (ModelState.IsValid)
                {
                    LoginEntity userExist = new LoginEntity();
                    bool IsExist = userExist.CheckIfUserExistOrNot(model.Email);

                    if (IsExist == false)
                    {
                        var sender = new MailAddress(ConstantStrings.supportEmail, ConstantStrings.supportName);
                        var receiver = new MailAddress(model.Email, model.FirstName);
                        var password = ConstantStrings.supportPassword;
                        var body = string.Empty;
                        var subject = "Note Marketplace - Email Verification";

                        string URL = ConfigurationManager.AppSettings["Localhost"] + "Account/VefiryEmail?Email=" + model.Email + "";
                        //  string New_URL = ConfigurationManager.AppSettings["Localhost"] + Url.Action("VefiryEmail", "Account", new { Email = model.Email });
                        // verify link problem on email 

                        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/emailVerification.html")))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{UserName}", model.FirstName);

                        body = body.Replace("{confirmUrl}", URL);
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(sender.Address, password)
                        };

                        using (var messege = new MailMessage(sender, receiver)
                        {
                            Body = body,
                            Subject = subject,
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(messege);

                        }

                        var EmailExist = db.Users.Where(x => x.EmailID.Equals(model.Email) && x.IsActive == true).FirstOrDefault();

                        if (EmailExist == null)
                        {
                            var user = new User
                            {
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                EmailID = model.Email,
                                Password = model.Password,
                                RoleID = Convert.ToInt32(Enums.UserRoleId.Member),
                                CreatedDate = System.DateTime.Now,
                                ModifiedDate = System.DateTime.Now,
                                IsActive = true
                            };
                            //user.CreatedBy = user.ID;
                            //user.ModifiedBy = user.ID;

                            db.Users.Add(user);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email is Already Exist try with another Email ID");
                        return View(model);
                    }


                    return RedirectToAction("Login", "Account");

                }
            }
            catch (Exception)
            {

                throw;
            }

            // If we got this far, something failed, redisplay form
            // return View(model);
            return View();
        }


        public ActionResult VefiryEmail(string Email)
        {
            LoginEntity login = new LoginEntity();
            bool IsVerify = login.UpdateVerifyEmail(Email);

            if (IsVerify == false)
            {

                try
                {

                    var updateEmailVerifiedStatus = db.Users.Where(x => x.EmailID.Equals(Email)).ToList().Select(x => x.IsEmailVerified = true);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    String message = ex.Message;

                }

            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult ChangePassword()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           // return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ConfirmPasswordModel cp)
        {

            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["ID"]);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();

                if (user.Password == cp.OldPassword)
                {
                    user.Password = cp.NewPassword;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    return RedirectToAction("Login", "Account");
                }

                else
                {
                    ModelState.AddModelError("OldPassword", "Password is Not Match try Again");
                    return View(cp);
                }
  
            }
            return View(cp);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            // it will clear the session at the end of request
            return RedirectToAction("Login", "Account");
        }
    }
}