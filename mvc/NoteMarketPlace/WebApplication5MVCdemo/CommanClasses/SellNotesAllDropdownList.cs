using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoteMarketPlace.Models;

namespace NoteMarketPlace.CommanClasses
{
    public class SellNotesAllDropdownList
    {
        public IEnumerable<NoteCategory> NoteCategories { get; set; }
        public SellNote SellNote { get; set; }
        public SellNoteAttachment NoteAttachments { get; set; }
        public IEnumerable<NoteType> NoteTypes { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        //public IEnumerable<Reference> references {
        //    get
        //    {
        //        List<Reference> ListDepartments = new List<Reference>()
        //        {
        //            new Reference() {ID = 1, Value="Free" },
        //            new Reference() {ID = 2, Value="Paid" }
                   
        //        };
        //        return ListDepartments;
        //    }
        //}
        public String IsPaidOrNot { get; set; }
    }
}