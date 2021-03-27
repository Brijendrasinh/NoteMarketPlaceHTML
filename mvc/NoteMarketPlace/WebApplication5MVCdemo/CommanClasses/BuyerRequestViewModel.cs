using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class BuyerRequestViewModel
    {
        public string PhoneNumber { get; set; }
        public int NoteID { get; set; }
        public String NoteTitle { get; set; }
        public string NoteCategory { get; set; }
        public string EmailID { get; set; }
        public int ID { get; set; }
        public string CountryCode { get; set; }
        public bool IsPaid { get; set; }
        public Decimal PurchasedPrice { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}