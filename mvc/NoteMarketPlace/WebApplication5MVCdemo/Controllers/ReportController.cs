using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using WebApplication5MVCdemo.CommanClasses;

namespace WebApplication5MVCdemo.Controllers
{
    public class ReportController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: Report
        public ActionResult SpamReport()
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
                {
                    List<NoteReport> Model = new List<NoteReport>();
                    Model = db.NoteReports.ToList();
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
        public ActionResult DeleteReport(int? ID)
        {
            NoteReport report = db.NoteReports.Where(x => x.ID == ID).FirstOrDefault();
            if (ID != null)
            {
                if (report != null)
                {
                    db.NoteReports.Remove(report);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("SpamReport", "Report");
        }
    }
}