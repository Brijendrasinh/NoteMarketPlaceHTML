using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class SellNotesAllDropdownList
    {
        public IEnumerable<NoteCategory> NoteCategories { get; set; }
        public SellNote SellNote { get; set; }
        public SellNoteAttachment NoteAttachments { get; set; }
        public IEnumerable<NoteType> NoteTypes { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public String IsPaidOrNot { get; set; }
        public decimal filesize { get; set; }
        public String displayPicture { get; set; }
        public String uploadedPdf { get; set; }
        public String Preview { get; set; }
    }
}