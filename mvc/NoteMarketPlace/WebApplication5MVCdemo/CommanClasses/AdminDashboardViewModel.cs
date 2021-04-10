using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class AdminDashboardViewModel
    {
        public int ID { get; set; }
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string AttachmentSize { get; set; }
        public bool IsPaid { get; set; }
        public Decimal Price { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public int NumberOfDownload { get; set; }
        public IEnumerable<NewGetDashboadrd_Result> getDahboardData_Results { get; set; } 
        public int NotesUnderReviewCount { get; set; }
        public int NewDownloadCount { get; set; }
        public int NewRegistrationCount { get; set; }
    }
}