using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteMarketPlace.Models;
using NoteMarketPlace.CommanClasses;

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
            return View();
        }

        public ActionResult SearchNotes()
        {
            return View();
        }  

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult BuyerRequest(string search)
        {
            //var query = from u in db.Downloads
            //            join b in db.Users
            //            on u.Downloader equals b.ID
            //            select b.EmailID;
            //ViewBag.ControllerSide = query;
            return View(db.Downloads.Where(x => x.NoteTitle.Contains(search) || x.NoteCategory.Contains(search) || x.SellNote.SellingPrice.Equals(Convert.ToDecimal(search)) || search == null).ToList());
           

            //if (Session["EmailID"] != null)
            //{
            //return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
        }

        public ActionResult userProfile()
        {
            if (Session["EmailID"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
        }

        [HttpPost]
        public ActionResult userProfile(UserProfile user,User userid)
        { 
            return View();
        }
    }
}