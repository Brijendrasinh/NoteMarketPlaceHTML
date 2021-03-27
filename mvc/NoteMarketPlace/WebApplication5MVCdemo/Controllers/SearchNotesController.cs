using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
using NoteMarketPlace.CommanClasses;
using WebApplication5MVCdemo.CommanClasses;

namespace WebApplication5MVCdemo.Controllers
{
    public class SearchNotesController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: SearchNotes
        public ActionResult Index()
        {
            SearchNotesViewModel SerachNotes = new SearchNotesViewModel();
            SerachNotes.countries = db.Countries.Where(x => x.IsActive == true).ToList();
            SerachNotes.noteTypes = db.NoteTypes.Where(x => x.IsActive == true).ToList();
            SerachNotes.noteCategories = db.NoteCategories.Where(x => x.IsActive == true).ToList();
            //var universities1 = db.SellNotes.Select(x => x.UniversityName).Distinct().ToList();
            //var courses1 = db.SellNotes.Select(x => x.Course).Distinct().ToList();
            int approve = Convert.ToInt32(Enums.ReferenceNoteStatus.Approved);
            
                SerachNotes.sellNotes = db.SellNotes.Where(x => x.Status == approve).ToList();
       

            SerachNotes.UniversityNames = db.SellNotes.Select(x => new SelectListItem()
            {
                Value = x.UniversityName,
                Text = x.UniversityName
            }).Distinct().ToList();
            
            SerachNotes.Courses = db.SellNotes.Select(x => new SelectListItem()
            {
                Value = x.Course,
                Text = x.Course
            }).Distinct().ToList();

            return View(SerachNotes);
        }

        [HttpGet]
        public JsonResult GetFilterSearchNotes(int FK_Type=0,int FK_Category=0,int FK_Country=0,string FK_University=null,string FK_Course=null)
        {

            if (string.IsNullOrEmpty(FK_University))
                FK_University = null;
            if (string.IsNullOrEmpty(FK_Course))
                FK_Course = null;
            var getSellNotes = db.GetSellerNotesDetails(FK_Type, FK_Category, FK_Country, FK_University, FK_Course).ToList();
            
            return Json(getSellNotes,JsonRequestBehavior.AllowGet);
            
        }

        
    }
}