//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication5MVCdemo.Models
{
    using System;
    
    public partial class GetDownloadedNotesData_Result
    {
        public int ID { get; set; }
        public int NoteID { get; set; }
        public string NoteTitle { get; set; }
        public string NoteCategory { get; set; }
        public int BuyerID { get; set; }
        public int SellerID { get; set; }
        public bool SellType { get; set; }
        public Nullable<decimal> PurchasedPrice { get; set; }
        public Nullable<System.DateTime> AttachmentDownloadDate { get; set; }
        public bool IsAttachmentDownloaded { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
    }
}