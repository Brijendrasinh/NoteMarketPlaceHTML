using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using NoteMarketPlace.CommanClasses;
using WebApplication5MVCdemo.CommanClasses;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace NoteMarketPlace.Controllers
{
    public class HomeController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["ID"] != null)
            {
                User user = new User();
                user.EmailID = Session["EmailID"].ToString();

                var result = db.Users.Where(x => x.EmailID.Equals(user.EmailID)).FirstOrDefault();
                //var fullname = SELECT FirstName +' ' + LastName FROM Users AS FullName;
                var fullName = result.FirstName + ' ' + result.LastName;
                ContactPageLogedUSer model = new ContactPageLogedUSer();
                model.FullName = fullName;
                ViewBag.fullName = fullName;
                model.EmailId = Session["EmailID"].ToString();
                ViewBag.email = model.EmailId;
                return View(model);
            }
            else
            {
                ViewBag.fullName = null;
                ViewBag.email = null;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Contact(ContactPageLogedUSer user)
        {
            if (user.EmailId == null || user.FullName == null)
            {
                user.EmailId = Session["EmailID"].ToString();

                var result = db.Users.Where(x => x.EmailID.Equals(user.EmailId)).FirstOrDefault();
                //var fullname = SELECT FirstName +' ' + LastName FROM Users AS FullName;
                var fullName = result.FirstName + ' ' + result.LastName;

                user.FullName = fullName;
            }

            //send Email
            var sender = new MailAddress("brijendrasinhchavda2018@gmail.com", "Brijendrasinh");
            var receiver = new MailAddress("brij2457@gmail.com", "Admin");
            var password = "b1k2chavda";
            var body = string.Empty;
            var subject = user.FullName + "-" + user.Subject;

            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/ContactUsMail.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserComment}", user.Comment);
            body = body.Replace("{UserName}", user.FullName);

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

            return RedirectToAction("Contact", "Home");
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult BuyerRequest()
        {
            if (Session["EmailID"] != null)
            {
                
                List<BuyerRequestViewModel> model = new List<BuyerRequestViewModel>();
                NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
                int Id = Convert.ToInt32(Session["ID"]);
                 model = (from d in db.Downloads
                          join up in db.UserProfiles on d.Downloader equals up.UserID
                          where d.Seller == Id && d.IsSellerHasAllowedDownloads == false
                          select new BuyerRequestViewModel()
                          {
                              ID = d.ID,
                              NoteID = d.NoteID,
                              NoteTitle = d.NoteTitle,
                              NoteCategory = d.NoteCategory,
                              EmailID = d.User1.EmailID,
                              CountryCode = up.CountryCode,
                              PhoneNumber = up.PhoneNumber,
                              IsPaid = d.IsPaid,
                              PurchasedPrice = (decimal)d.PurchasedPrice,
                              CreatedDate = (DateTime)d.CreatedDate
                          }).ToList();

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
  
    }
}