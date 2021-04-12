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
        public string ErrorMessage { get; set; }
        public decimal filesize { get; set; }
        public string UploadUserFiles(HttpPostedFileBase[] file)
        {
            try
            {
                var supportedTypes = new[] {"pdf"};
                foreach(var files in file)
                {
                    var fileExt = System.IO.Path.GetExtension(files.FileName).Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ErrorMessage = "File Extension Is InValid - Only Upload WORD/PDF/EXCEL/TXT File";
                        return ErrorMessage;
                    }
                    else if (files.ContentLength > (filesize * 1024))
                    {
                        ErrorMessage = "File size Should Be UpTo " + filesize + "KB";
                        return ErrorMessage;
                    }
                    else
                    {
                        ErrorMessage = "File Is Successfully Uploaded";
                        return ErrorMessage;
                    }
                   
                }
                return ErrorMessage;
            }
            catch (Exception e)
            {
                ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
                return ErrorMessage;
            }
        }

    }
}