using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;
using NoteMarketPlace.CommanClasses;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{

    public class SearchNotesViewModel
    {
        public int countryId { get; set; }
        public int categoryId { get; set; }
        public int typeId { get; set; }
        public int universityId { get; set; }
        public int courseId { get; set; }
        public IEnumerable<GetSellNoteDetails_Result> getSellNoteDetails_Results { get; set; }
        public IEnumerable<SellNote> sellNotes { get; set; }
        public IEnumerable<Country> countries { get; set; }
        public IEnumerable<NoteCategory> noteCategories { get; set; }
        public IEnumerable<NoteType> noteTypes { get; set; }
        
        public IEnumerable<SelectListItem> UniversityNames { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }
       
        //public IEnumerable<Course> courses { get; set; }
        //public IEnumerable<Rating> ratings { get; set; }
    }

        //public class Rating
        //{
        //    NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        //    public IEnumerable<Rating> GetUniversities()
        //    {
        //        var result = db.SellNotes.Select(x => x.).Distinct().ToList();
        //        return (IEnumerable<Course>)result;
        //    }
        //}

        //public class Course
        //{
        //    NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        //    public IEnumerable<Course> GetUniversities()
        //    {
        //        var result = db.SellNotes.Select(x => x.Course).Distinct().ToList();
        //        return (IEnumerable<Course>)result;
        //    }
        //}

        //public class University
        //{

        //}
}