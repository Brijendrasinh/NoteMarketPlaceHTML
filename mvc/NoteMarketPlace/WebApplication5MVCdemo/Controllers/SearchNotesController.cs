﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;
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
        public ActionResult GetFilterSearchNotes(int FK_Type = 0, int FK_Category = 0, int FK_Country = 0, string FK_University = null, string FK_Course = null,
                                                int PageNumber = 1, int PageSize = 9, string search = null, string rating = null)
        {
            SearchNotesViewModel Model = new SearchNotesViewModel();
            if (string.IsNullOrEmpty(FK_University))
                FK_University = null;
            if (string.IsNullOrEmpty(FK_Course))
                FK_Course = null;
            if (string.IsNullOrEmpty(search))
                search = null;
            if (string.IsNullOrEmpty(rating))
                rating = null;

            Model.PageNumber = PageNumber;
            Model.PageSize = PageSize;
            
            List<NewGetSellerNotesDetails_Result> getSellNotes = db.NewGetSellerNotesDetails(FK_Type, FK_Category, FK_Country, FK_University, FK_Course, PageSize, PageNumber, search, rating).ToList();

            Model.NewGetSellerNotesDetails_Result = getSellNotes;
            foreach (var data in getSellNotes)
            {
                Model.TotalRecords = (int)data.TotalCount;
            }
            return PartialView("_SellNotes", Model);

        }


    }
}