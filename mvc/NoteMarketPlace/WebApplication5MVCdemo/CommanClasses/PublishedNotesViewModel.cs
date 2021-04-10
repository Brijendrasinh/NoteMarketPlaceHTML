using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class PublishedNotesViewModel
    {
        public IEnumerable<SellNote> sellNotes { get; set; }
        public IEnumerable<GetPublishedNotesData_Result> getPublishedNotesData_Results { get; set; }
        public List<SelectListItem> SellerName { get; set; }
        public int Seller { get; set; }
    }
}