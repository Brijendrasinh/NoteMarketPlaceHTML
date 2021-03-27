using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class NoteDetailViewModel
    {
        public IEnumerable<NoteReview> noteReviews { get; set; }
        public int ReviewCount { get; set; }
        public int ReportCount { get; set; }
        public Decimal? Rating { get; set; }
        public SellNote sellNote { get; set; }
    }
}