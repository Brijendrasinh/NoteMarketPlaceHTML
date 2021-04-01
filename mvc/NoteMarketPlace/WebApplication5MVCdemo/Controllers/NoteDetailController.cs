using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.CommanClasses;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.Controllers
{
    public class NoteDetailController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: NoteDetail
        public ActionResult NoteDetail(int? NoteID)
        {
            NoteDetailViewModel model = new NoteDetailViewModel();

            model.sellNote = db.SellNotes.Where(x => x.ID == NoteID).FirstOrDefault();
            model.noteReviews = db.NoteReviews.Where(x => x.NoteID == NoteID).ToList();
            model.ReviewCount = model.noteReviews.Count();
            model.ReportCount = db.NoteReports.Where(x => x.NoteID == NoteID).Count();
            if (model.ReviewCount != 0)
            {
                model.Rating = (decimal)db.NoteReviews.Where(x => x.NoteID == NoteID).Select(x => x.Ratings).Average();
            }
            else
            {
                model.Rating = 0;
            }
            return View(model);

        }

        public ActionResult DownloadNote(int? noteId)
        {
            SellNote noteISPaidOrNot = db.SellNotes.Where(x => x.ID == noteId).FirstOrDefault();
            bool paidOrNot = noteISPaidOrNot.IsPaid;
            int DownloaderID = Convert.ToInt32(Session["ID"]);
            SellNoteAttachment noteAttachment = db.SellNoteAttachments.Where(x => x.NoteID == noteISPaidOrNot.ID).FirstOrDefault();
            var filePath = ConstantStrings.Notes_Upload_Path + noteISPaidOrNot.SellerID.ToString() + "/" + noteISPaidOrNot.ID.ToString() + ConstantStrings.Note_Attachment_Path + noteAttachment.FileName;

            string fullName = Server.MapPath(filePath);

            byte[] fileBytes = GetFile(fullName);

            return File(
                   fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
    }
}