using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.CommanClasses;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.Controllers
{
    public class NotesUnderReviewController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: NotesUnderReview
        public ActionResult NotesUnderReview()
        {
            NotesUnderReviewViewModel Model = new NotesUnderReviewViewModel();
            Model.SellerName = db.SellNotes.Select(x => new SelectListItem()
            {
                Value = x.SellerID.ToString(),
                Text = x.User.FirstName + " " + x.User.LastName
            }).Distinct().ToList();

            return View(Model);
        }

        public ActionResult GetFilterSellerNUR(string SellerId=null)
        {
            if (string.IsNullOrEmpty(SellerId))
                SellerId = null;
            NotesUnderReviewViewModel Model = new NotesUnderReviewViewModel();
            List<GetNotesUnderReviewData_Result> getdata = db.GetNotesUnderReviewData(SellerId).ToList();
            Model.getNotesUnderReviewData_Results = getdata;
            return PartialView("_NotesUnderReview", Model);
        }

        public ActionResult Approve(int? NoteID)
        {
            SellNote getSellnote = db.SellNotes.Where(x => x.ID == NoteID).FirstOrDefault();
            getSellnote.Status = Convert.ToInt32(Enums.ReferenceNoteStatus.Approved);
            db.SaveChanges();
            return RedirectToAction("NotesUnderReview", "NotesUnderReview");
        }

        [HttpPost]
        public ActionResult Reject()
        {
            int id = Convert.ToInt32(Request.Form["hiddenId"]);
            SellNote getSellnote = db.SellNotes.Where(x => x.ID == id).FirstOrDefault();
            getSellnote.Status = Convert.ToInt32(Enums.ReferenceNoteStatus.Rejected);
            getSellnote.AdminRemarks = Request.Form["remarks"];
            db.SaveChanges();
            return RedirectToAction("NotesUnderReview", "NotesUnderReview");
        }

        public ActionResult InReview(int? NoteID)
        {
            SellNote getSellnote = db.SellNotes.Where(x => x.ID == NoteID).FirstOrDefault();
            if(getSellnote.Status == Convert.ToInt32(Enums.ReferenceNoteStatus.SubmittedForReview))
            {
                getSellnote.Status = Convert.ToInt32(Enums.ReferenceNoteStatus.InReview);
            }
            db.SaveChanges();
            return RedirectToAction("NotesUnderReview", "NotesUnderReview");
        }
    }
}