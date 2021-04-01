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
            AdminDashboardViewModel Model = new AdminDashboardViewModel();
            var data = db.getDahboardData().ToList();
            Model.getDahboardData_Results = data;
            return View(Model);
        }
    }
}