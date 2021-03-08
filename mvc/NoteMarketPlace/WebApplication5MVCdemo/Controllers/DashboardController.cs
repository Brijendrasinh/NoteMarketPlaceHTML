using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using NoteMarketPlace.CommanClasses;
using NoteMarketPlace.Models;


namespace NoteMarketPlace.Controllers
{
    public class DashboardController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            return View(db.SellNotes.ToList());
        }

        //public ActionResult IndexProgress(string search)
        //{
        //    return View(db.SellNotes.Where(x => x.Title.Contains(search) /*|| x.Category.Contains(search) || x..Equals(Convert.ToDecimal(search))*/ || search == null).ToList());
        //}

        //public ActionResult IndexPublished(string search)
        //{
        //    return View(db.SellNotes.Where(x => x.Title.Contains(search) /*|| x.Category.Contains(search)*/ || x.SellingPrice.Equals(Convert.ToDecimal(search)) || search == null).ToList());
        //}

        public ActionResult AddNotes()
        {
            //if (Session["EmailID"] != null && Session["ID"] != null)
            //{
            var categories = db.NoteCategories.ToList();
            var countries = db.Countries.ToList();
            var noteTypes = db.NoteTypes.ToList();

            var viewModel = new SellNotesAllDropdownList
            {
                NoteCategories = categories,
                Countries = countries,
                NoteTypes = noteTypes,

            };
            return View(viewModel);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}

        }


        [HttpPost]
        public ActionResult AddNotes(SellNotesAllDropdownList user,
                                     HttpPostedFileBase DisplayPictureFile,
                                     HttpPostedFileBase UploadedPdfFile,
                                     HttpPostedFileBase PreviewFile)
        {
            var SubmitValue = Request.Form["SavePublish"];
            if (SubmitValue == "save")
            {
                string fileExtension;
                string DisplayPictureFileName;
                string PreviewFileName;

                // check paid status
                SellNotesEntity PaidOrNot = new SellNotesEntity();
                bool PaidStatus = PaidOrNot.CheckNoteStatusPaidOrNot(user.IsPaidOrNot);


                //var loginuserId = Convert.ToInt32(Session["ID"]);
                var addSellNoteDetail = new SellNote
                {

                    Course = user.SellNote.Course,
                    CourseCode = user.SellNote.CourseCode,
                    Discription = user.SellNote.Discription,
                    NumberOfPages = user.SellNote.NumberOfPages,
                    Professor = user.SellNote.Professor,
                    SellingPrice = user.SellNote.SellingPrice,
                    Title = user.SellNote.Title,
                    UniversityName = user.SellNote.UniversityName,
                    IsPaid = PaidStatus,
                    Category = user.SellNote.Category,
                    Country = user.SellNote.Country,
                    NoteType = user.SellNote.NoteType,
                    Status = 3,
                    ActionedBy = 3,
                    SellerID = Convert.ToInt32(Session["ID"]),
                    IsActive = true
                };

                try
                {
                    db.SellNotes.Add(addSellNoteDetail);
                    db.SaveChanges();
                    int NotesID = addSellNoteDetail.ID;


                    if (DisplayPictureFile != null)
                    {

                        fileExtension = Path.GetExtension(DisplayPictureFile.FileName);
                        DisplayPictureFileName = "DP_" + fileExtension;

                        DisplayPictureFileName = CheckifPathExistForCurrentUser(NotesID) + DisplayPictureFileName;
                        addSellNoteDetail.DisplayPicture = DisplayPictureFileName;
                        DisplayPictureFile.SaveAs(DisplayPictureFileName);
                        db.SaveChanges();
                    }
                    // db.SellNotes.Add(addSellNoteDetail);
                    // db.SaveChanges();


                    if (PreviewFile != null)
                    {
                        fileExtension = Path.GetExtension(PreviewFile.FileName);
                        PreviewFileName = "PreviewFile_" + fileExtension;

                        PreviewFileName = CheckifPathExistForCurrentUser(NotesID) + PreviewFileName;
                        addSellNoteDetail.NotesPreview = PreviewFileName;
                        PreviewFile.SaveAs(PreviewFileName);
                        db.SaveChanges();
                    }
                    // db.SellNotes.Add(addSellNoteDetail);



                    var FilePathForUploadedPdf = checkifPathExistForNoteAttachment(NotesID);
                    var sellNotesAttachment = new SellNoteAttachment()
                    {
                        NoteID = NotesID,
                        FileName = "Xyz",
                        FilePath = FilePathForUploadedPdf,
                        IsActive = true
                    };
                    db.SellNoteAttachments.Add(sellNotesAttachment);
                    db.SaveChanges();


                    int AttachmentID = sellNotesAttachment.ID;

                    fileExtension = Path.GetExtension(UploadedPdfFile.FileName);
                    var UploadedPdfFileName = AttachmentID.ToString() + fileExtension;

                    sellNotesAttachment.FileName = UploadedPdfFileName;

                    var destinationFileLocation = Path.Combine(FilePathForUploadedPdf, UploadedPdfFileName);
                    UploadedPdfFile.SaveAs(destinationFileLocation);
                    //db.SellNoteAttachments.Add(sellNotesAttachment);
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    throw;
                }
            }

            else if (SubmitValue == "publish")
            {
                
            }

            return RedirectToAction("AddNotes", "Dashboard");
        }

        public String CheckifPathExistForCurrentUser(int NotesID)
        {
            // Check Path for specific user exist or not. If not exists then create
            var pathofUploadImage = Server.MapPath(ConstantStrings.Notes_Upload_Path + Session["Id"].ToString() + "/" + NotesID.ToString() + "/");
            if (!System.IO.File.Exists(pathofUploadImage))
            {
                DirectorySecurity sec = new DirectorySecurity();
                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                Directory.CreateDirectory(pathofUploadImage, sec);

            }
            return pathofUploadImage;
        }

        public String checkifPathExistForNoteAttachment(int NotesID)
        {
            // Check Path for specific user exist or not. If not exists then create
            var pathofUploadImage = Server.MapPath(ConstantStrings.Notes_Upload_Path + Session["Id"].ToString() + "/" + NotesID.ToString() + "/" + ConstantStrings.Note_Attachment_Path);
            if (!System.IO.File.Exists(pathofUploadImage))
            {
                DirectorySecurity sec = new DirectorySecurity();
                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                Directory.CreateDirectory(pathofUploadImage, sec);
            }
            return pathofUploadImage;
        }
    }
}