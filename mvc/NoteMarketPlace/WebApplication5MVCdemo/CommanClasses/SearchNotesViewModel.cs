using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

using System.Web.Mvc;


namespace WebApplication5MVCdemo.CommanClasses
{

    public class SearchNotesViewModel
    {
        public int countryId { get; set; }
        public int categoryId { get; set; }
        public int typeId { get; set; }
        public int universityId { get; set; }
        public int courseId { get; set; }
        public IEnumerable<NewGetSellerNotesDetails_Result> NewGetSellerNotesDetails_Result { get; set; }
        public IEnumerable<SellNote> sellNotes { get; set; }
        public IEnumerable<Country> countries { get; set; }
        public IEnumerable<NoteCategory> noteCategories { get; set; }
        public IEnumerable<NoteType> noteTypes { get; set; }
        
        public IEnumerable<SelectListItem> UniversityNames { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

       
}