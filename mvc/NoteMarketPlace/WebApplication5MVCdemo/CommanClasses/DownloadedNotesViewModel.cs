using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class DownloadedNotesViewModel
    {
        public List<SelectListItem> SellerName { get; set; }
        public int Seller { get; set; }
        public List<SelectListItem> BuyerName { get; set; }
        public int Buyer { get; set; }
        public List<SelectListItem> NoteTitle { get; set; }
        public int NoteID { get; set; }
        public IEnumerable<GetDownloadedNotesData_Result> getDownloadedNotesData_Results { get; set; }
    }
}