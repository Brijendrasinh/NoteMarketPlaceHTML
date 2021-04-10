using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.CommanClasses;
using WebApplication5MVCdemo.Models;
using Ionic.Zip;

namespace WebApplication5MVCdemo.Controllers
{
    public class NoteDetailController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: NoteDetail
        public ActionResult NoteDetail(int? NoteID)
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int RoleMember = Convert.ToInt32(Enums.UserRoleId.Member);
                User user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user.RoleID != RoleMember)
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
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult DownloadNote(int? noteId)
        {
            SellNote noteISPaidOrNot = db.SellNotes.Where(x => x.ID == noteId).FirstOrDefault();
            bool paidOrNot = noteISPaidOrNot.IsPaid;
            int DownloaderID = Convert.ToInt32(Session["ID"]);
            var noteAttachment = db.SellNoteAttachments.Where(x => x.NoteID == noteISPaidOrNot.ID).ToList();

            var filePath = ConstantStrings.Notes_Upload_Path + noteISPaidOrNot.SellerID.ToString() + "/" + noteISPaidOrNot.ID.ToString() + ConstantStrings.Note_Attachment_Path;


            var outputStream = new MemoryStream();

            using (var zip = new ZipFile())
            {
                zip.AddDirectory(Server.MapPath(filePath));

                //zip.AddEntry("file1.txt", "content1");
                //zip.AddEntry("file2.txt", "content2");
                zip.Save(outputStream);
            }

            outputStream.Position = 0;
            return File(outputStream.ToArray(), "application/zip", noteISPaidOrNot.Title + ".zip");

            //for single note download code below

            //string fullName = Server.MapPath(filePath);

            //byte[] fileBytes = GetFile(fullName);

            //return File(
            //       fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
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

        public ActionResult DeleteReview(int? ID)
        {
            if (ID != null)
            {
                NoteReview noteReview = db.NoteReviews.Where(x => x.ID == (int)ID).FirstOrDefault();
                db.NoteReviews.Remove(noteReview);
                db.SaveChanges();
            }
            return RedirectToAction("PublishedNotes", "Admin");
        }
    }
}