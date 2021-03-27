using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class DashBoardViewModel
    {
        public int NumberOfNotesSold { get; set; }
        public Decimal MoneyEarned { get; set; }
        public int MyDownloads { get; set; }
        public int MyRejectedNotes { get; set; }
        public int BuyerRequest { get; set; }
        public IEnumerable<SellNote> SellNotes { get; set; }
    }
}