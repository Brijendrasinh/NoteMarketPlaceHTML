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
    using System.Collections.Generic;
    
    public partial class Download
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Download()
        {
            this.NoteReports = new HashSet<NoteReport>();
            this.NoteReviews = new HashSet<NoteReview>();
        }
    
        public int ID { get; set; }
        public int NoteID { get; set; }
        public int Seller { get; set; }
        public int Downloader { get; set; }
        public bool IsSellerHasAllowedDownloads { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsAttachmentDownloaded { get; set; }
        public Nullable<System.DateTime> AttachmentDownloadDate { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<decimal> PurchasedPrice { get; set; }
        public string NoteTitle { get; set; }
        public string NoteCategory { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual SellNote SellNote { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoteReport> NoteReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoteReview> NoteReviews { get; set; }
        public virtual Download Downloads1 { get; set; }
        public virtual Download Download1 { get; set; }
    }
}
