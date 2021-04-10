using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebApplication5MVCdemo.CommanClasses;
using WebApplication5MVCdemo.Models;
using System.Net.Mail;
using System.Net;
using Ionic.Zip;

namespace NoteMarketPlace.Controllers
{
    public class DashboardController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["EmailID"] != null && Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                int reject = Convert.ToInt32(Enums.ReferenceNoteStatus.Rejected);
                var result = db.SellNotes.Where(x => x.SellerID == id).ToList();
                DashBoardViewModel data = new DashBoardViewModel();
                var sellerHasSoldNote = db.Downloads.Where(x => x.Seller == id).ToList();
                if (sellerHasSoldNote.Count != 0)
                {
                    data.BuyerRequest = db.Downloads.Where(x => x.Seller == id && x.IsPaid == true && x.IsSellerHasAllowedDownloads == false).Count();
                    data.MoneyEarned = (decimal)db.Downloads.Where(x => x.Seller == id && x.IsSellerHasAllowedDownloads == true).Sum(x => x.PurchasedPrice);
                    data.NumberOfNotesSold = db.Downloads.Where(x => x.Seller == id && x.IsSellerHasAllowedDownloads == true).Count();
                }
                else
                {
                    data.BuyerRequest = 0;
                    data.MoneyEarned = 0;
                    data.NumberOfNotesSold = 0;
                }
                data.MyDownloads = db.Downloads.Where(x => x.Downloader == id && x.IsAttachmentDownloaded == true ).Count();
                data.MyRejectedNotes = db.SellNotes.Where(x => x.SellerID == id && x.Status == reject).Count();
                data.SellNotes = result;
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public ActionResult AddNotes(int? noteId)
        {
            if (Session["EmailID"] != null && Session["ID"] != null)
            {
                var categories = db.NoteCategories.Where(x => x.IsActive == true).ToList();
                var countries = db.Countries.Where(x => x.IsActive == true).ToList();
                var noteTypes = db.NoteTypes.Where(x => x.IsActive == true).ToList();
                var viewModel = new SellNotesAllDropdownList
                {
                    NoteCategories = categories,
                    Countries = countries,
                    NoteTypes = noteTypes,

                };
                if (noteId != null)
                {
                    SellNote note = db.SellNotes.Where(x => x.ID == noteId).FirstOrDefault();
                    viewModel.SellNote = note;
                }
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }


        [HttpPost]
        public ActionResult AddNotes(SellNotesAllDropdownList user,
                                     HttpPostedFileBase DisplayPictureFile,
                                     HttpPostedFileBase[] UploadedPdfFile,
                                     HttpPostedFileBase PreviewFile)
        {

              // Settings.  

                SellNote note = db.SellNotes.Where(x => x.ID == user.SellNote.ID).FirstOrDefault();
                if (note != null)
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

                        note.Course = user.SellNote.Course;
                        note.CourseCode = user.SellNote.CourseCode;
                        note.Discription = user.SellNote.Discription;
                        note.NumberOfPages = user.SellNote.NumberOfPages;
                        note.Professor = user.SellNote.Professor;
                        note.SellingPrice = user.SellNote.SellingPrice;
                        note.Title = user.SellNote.Title;
                        note.UniversityName = user.SellNote.UniversityName;
                        note.IsPaid = PaidStatus;
                        note.Category = user.SellNote.Category;
                        note.Country = user.SellNote.Country;
                        note.NoteType = user.SellNote.NoteType;
                        note.Status = 3;
                        note.ActionedBy = 3;
                        note.SellerID = Convert.ToInt32(Session["ID"]);
                        note.IsActive = true;
                        note.CreatedDate = DateTime.Now;

                        db.SaveChanges();

                        if (DisplayPictureFile != null /*&& note.DisplayPicture != null*/)
                        {

                            fileExtension = Path.GetExtension(DisplayPictureFile.FileName);
                            DisplayPictureFileName = "DP_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                            var checkFile = CheckifPathExistForCurrentUser(note.ID) + note.DisplayPicture;
                            if (System.IO.File.Exists(checkFile))
                            {
                                System.IO.File.Delete(checkFile);
                            }
                            note.DisplayPicture = DisplayPictureFileName;

                            DisplayPictureFileName = CheckifPathExistForCurrentUser(note.ID) + DisplayPictureFileName;

                            DisplayPictureFile.SaveAs(DisplayPictureFileName);
                            db.SaveChanges();
                        }



                        if (PreviewFile != null /*&& note.NotesPreview != null*/)
                        {
                            fileExtension = Path.GetExtension(PreviewFile.FileName);
                            PreviewFileName = "PreviewFile_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                            var checkFile1 = CheckifPathExistForCurrentUser(note.ID) + note.NotesPreview;
                            if (System.IO.File.Exists(checkFile1))
                            {
                                System.IO.File.Delete(checkFile1);
                            }
                            note.NotesPreview = PreviewFileName;
                            PreviewFileName = CheckifPathExistForCurrentUser(note.ID) + PreviewFileName;

                            PreviewFile.SaveAs(PreviewFileName);
                            db.SaveChanges();
                        }



                        // logic is remain
                        if (UploadedPdfFile != null)
                        {
                            var FilePathForUploadedPdf = checkifPathExistForNoteAttachment(note.ID);
                            var attchedNote = db.SellNoteAttachments.Where(x => x.NoteID == note.ID).ToList();

                            DirectoryInfo di = new DirectoryInfo(FilePathForUploadedPdf);
                            foreach (FileInfo files in di.GetFiles())
                            {
                                files.Delete();
                            }
                            foreach (var file in attchedNote)
                            {
                                db.SellNoteAttachments.Remove(file);
                                db.SaveChanges();
                            }

                            long Length = 0;
                            foreach (HttpPostedFileBase file in UploadedPdfFile)
                            {
                                fileExtension = Path.GetExtension(file.FileName);
                                //Checking file is available to save.  
                                if (file != null)
                                {
                                    var InputFileName = Path.GetFileNameWithoutExtension(file.FileName);
                                    var attachment = new SellNoteAttachment()
                                    {
                                        NoteID = note.ID,
                                        FileName = InputFileName,
                                        FilePath = FilePathForUploadedPdf,
                                        IsActive = true
                                    };
                                    db.SellNoteAttachments.Add(attachment);
                                    db.SaveChanges();
                                    int AttachmentID = attachment.ID;
                                    fileExtension = Path.GetExtension(file.FileName);
                                    var UploadedPdfFileName = AttachmentID.ToString() + "_" + InputFileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                                    attachment.FileName = UploadedPdfFileName;
                                    var destinationFileLocation = Path.Combine(FilePathForUploadedPdf, UploadedPdfFileName);
                                    file.SaveAs(destinationFileLocation);
                                    Length = Length + file.ContentLength;
                                    db.SaveChanges();
                                }

                            }

                            note.AttachmentSize = FileSizeFormatter.FormatSize(Length);
                            db.SaveChanges();
                        }



                        //below code is for singlefile upload


                        //var FilePathForUploadedPdf = checkifPathExistForNoteAttachment(note.ID);
                        //SellNoteAttachment attchedNote = db.SellNoteAttachments.Where(x => x.NoteID == note.ID).FirstOrDefault();

                        //fileExtension = Path.GetExtension(UploadedPdfFile.FileName);

                        //if (attchedNote != null)
                        //{
                        //    string UploadedPdfFileName = attchedNote.ID.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                        //    var checkFile2 = Path.Combine(FilePathForUploadedPdf, attchedNote.FileName);
                        //    if (System.IO.File.Exists(checkFile2))
                        //    {
                        //        System.IO.File.Delete(checkFile2);
                        //    }
                        //    attchedNote.FileName = UploadedPdfFileName;

                        //    var destinationFileLocation = Path.Combine(FilePathForUploadedPdf, attchedNote.FileName);
                        //    UploadedPdfFile.SaveAs(destinationFileLocation);

                        //    db.SaveChanges();
                        //}

                        return RedirectToAction("AddNotes", "Dashboard");

                    }

                    else if (SubmitValue == "publish")
                    {
                        note.Status = Convert.ToInt32(Enums.ReferenceNoteStatus.SubmittedForReview);
                        note.PublishedDate = DateTime.Now;
                        db.SaveChanges();

                        // Mail Sending Code
                        int id = Convert.ToInt32(Session["ID"]);
                        User SendMail = db.Users.Where(x => x.ID == id).FirstOrDefault();

                        // Mail Sending Code
                        var sender = new MailAddress(ConstantStrings.supportEmail, ConstantStrings.supportName);
                        var receiver = new MailAddress("sedkeha1234@gmail.com", "Admin");
                        var password = ConstantStrings.supportPassword;
                        var body = string.Empty;
                        var subject = SendMail.FirstName + " sent his note for review";

                        //string URL = ConfigurationManager.AppSettings["Localhost"] + "Account/VefiryEmail?Email=" + user.EmailID + "";
                        //  string New_URL = ConfigurationManager.AppSettings["Localhost"] + Url.Action("VefiryEmail", "Account", new { Email = model.Email });
                        // verify link problem on email 

                        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/PublishNoteNotification.html")))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{SellerName}", SendMail.FirstName);
                        body = body.Replace("{NoteTitle}", note.Title);

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(sender.Address, password)
                        };

                        using (var messege = new MailMessage(sender, receiver)
                        {
                            Body = body,
                            Subject = subject,
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(messege);

                        }
                    }
                }


                // Below Code is For New Note Added 

                else
                {

                    var SubmitValue = Request.Form["SavePublish"];
                    string fileExtension;
                    string DisplayPictureFileName;
                    string PreviewFileName;
                    if (SubmitValue == "save")
                    {
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
                            IsActive = true,
                            CreatedDate = DateTime.Now
                        };

                        try
                        {
                            db.SellNotes.Add(addSellNoteDetail);
                            db.SaveChanges();
                            int NotesID = addSellNoteDetail.ID;


                            if (DisplayPictureFile != null)
                            {

                                fileExtension = Path.GetExtension(DisplayPictureFile.FileName);
                                DisplayPictureFileName = "DP_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                                addSellNoteDetail.DisplayPicture = DisplayPictureFileName;
                                DisplayPictureFileName = CheckifPathExistForCurrentUser(NotesID) + DisplayPictureFileName;

                                DisplayPictureFile.SaveAs(DisplayPictureFileName);
                                db.SaveChanges();
                            }
                            // db.SellNotes.Add(addSellNoteDetail);
                            // db.SaveChanges();


                            if (PreviewFile != null)
                            {
                                fileExtension = Path.GetExtension(PreviewFile.FileName);
                                PreviewFileName = "PreviewFile_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                                addSellNoteDetail.NotesPreview = PreviewFileName;
                                PreviewFileName = CheckifPathExistForCurrentUser(NotesID) + PreviewFileName;

                                PreviewFile.SaveAs(PreviewFileName);
                                db.SaveChanges();
                            }
                            // db.SellNotes.Add(addSellNoteDetail);


                            var FilePathForUploadedPdf = checkifPathExistForNoteAttachment(NotesID);
                            long Length = 0;
                            foreach (HttpPostedFileBase file in UploadedPdfFile)
                            {
                                //Checking file is available to save.  
                                if (file != null)
                                {
                                    var InputFileName = Path.GetFileNameWithoutExtension(file.FileName);
                                    var attachment = new SellNoteAttachment()
                                    {
                                        NoteID = NotesID,
                                        FileName = InputFileName,
                                        FilePath = FilePathForUploadedPdf,
                                        IsActive = true
                                    };
                                    db.SellNoteAttachments.Add(attachment);
                                    db.SaveChanges();
                                    int AttachmentID = attachment.ID;
                                    fileExtension = Path.GetExtension(file.FileName);
                                    var UploadedPdfFileName = AttachmentID.ToString() + "_" + InputFileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                                    attachment.FileName = UploadedPdfFileName;
                                    var destinationFileLocation = Path.Combine(FilePathForUploadedPdf, UploadedPdfFileName);
                                    file.SaveAs(destinationFileLocation);
                                    Length = Length + file.ContentLength;
                                    db.SaveChanges();
                                }

                            }

                            addSellNoteDetail.AttachmentSize = FileSizeFormatter.FormatSize(Length);
                            db.SaveChanges();

                            // Below code is for first time single file uploading

                            //var FilePathForUploadedPdf = checkifPathExistForNoteAttachment(NotesID);
                            //var sellNotesAttachment = new SellNoteAttachment()
                            //{
                            //    NoteID = NotesID,
                            //    FileName = "Xyz",
                            //    FilePath = FilePathForUploadedPdf,
                            //    IsActive = true
                            //};
                            //db.SellNoteAttachments.Add(sellNotesAttachment);
                            //db.SaveChanges();
                            //int AttachmentID = sellNotesAttachment.ID;
                            //fileExtension = Path.GetExtension(UploadedPdfFile.FileName);
                            //var UploadedPdfFileName = AttachmentID.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;

                            //sellNotesAttachment.FileName = UploadedPdfFileName;

                            //var destinationFileLocation = Path.Combine(FilePathForUploadedPdf, UploadedPdfFileName);
                            //UploadedPdfFile.SaveAs(destinationFileLocation);
                            ////db.SellNoteAttachments.Add(sellNotesAttachment);
                            //db.SaveChanges();

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    else if (SubmitValue == "publish")
                    {
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
                            Status = 4,
                            ActionedBy = 3,
                            SellerID = Convert.ToInt32(Session["ID"]),
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            PublishedDate = DateTime.Now
                        };

                        try
                        {
                            db.SellNotes.Add(addSellNoteDetail);
                            db.SaveChanges();
                            int NotesID = addSellNoteDetail.ID;


                            if (DisplayPictureFile != null)
                            {

                                fileExtension = Path.GetExtension(DisplayPictureFile.FileName);
                                DisplayPictureFileName = "DP_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                                addSellNoteDetail.DisplayPicture = DisplayPictureFileName;
                                DisplayPictureFileName = CheckifPathExistForCurrentUser(NotesID) + DisplayPictureFileName;

                                DisplayPictureFile.SaveAs(DisplayPictureFileName);
                                db.SaveChanges();
                            }
                            // db.SellNotes.Add(addSellNoteDetail);
                            // db.SaveChanges();


                            if (PreviewFile != null)
                            {
                                fileExtension = Path.GetExtension(PreviewFile.FileName);
                                PreviewFileName = "PreviewFile_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                                addSellNoteDetail.NotesPreview = PreviewFileName;
                                PreviewFileName = CheckifPathExistForCurrentUser(NotesID) + PreviewFileName;

                                PreviewFile.SaveAs(PreviewFileName);
                                db.SaveChanges();
                            }
                            // db.SellNotes.Add(addSellNoteDetail);

                            var FilePathForUploadedPdf = checkifPathExistForNoteAttachment(NotesID);
                            long Length = 0;
                            foreach (HttpPostedFileBase file in UploadedPdfFile)
                            {
                                //Checking file is available to save.  
                                if (file != null)
                                {
                                    var InputFileName = Path.GetFileNameWithoutExtension(file.FileName);
                                    var attachment = new SellNoteAttachment()
                                    {
                                        NoteID = NotesID,
                                        FileName = InputFileName,
                                        FilePath = FilePathForUploadedPdf,
                                        IsActive = true
                                    };
                                    db.SellNoteAttachments.Add(attachment);
                                    db.SaveChanges();
                                    int AttachmentID = attachment.ID;
                                    fileExtension = Path.GetExtension(file.FileName);
                                    var UploadedPdfFileName = AttachmentID.ToString() + "_" + InputFileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                                    attachment.FileName = UploadedPdfFileName;
                                    var destinationFileLocation = Path.Combine(FilePathForUploadedPdf, UploadedPdfFileName);
                                    file.SaveAs(destinationFileLocation);
                                    Length = Length + file.ContentLength;
                                    db.SaveChanges();
                                }

                            }
                            addSellNoteDetail.AttachmentSize = FileSizeFormatter.FormatSize(Length);
                            db.SaveChanges();

                            //Below code is for single note

                            //var FilePathForUploadedPdf = checkifPathExistForNoteAttachment(NotesID);
                            //var sellNotesAttachment = new SellNoteAttachment()
                            //{
                            //    NoteID = NotesID,
                            //    FileName = "Xyz",
                            //    FilePath = FilePathForUploadedPdf,
                            //    IsActive = true
                            //};
                            //db.SellNoteAttachments.Add(sellNotesAttachment);
                            //db.SaveChanges();


                            //int AttachmentID = sellNotesAttachment.ID;

                            //fileExtension = Path.GetExtension(UploadedPdfFile.FileName);
                            //var UploadedPdfFileName = AttachmentID.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;

                            //sellNotesAttachment.FileName = UploadedPdfFileName;

                            //var destinationFileLocation = Path.Combine(FilePathForUploadedPdf, UploadedPdfFileName);
                            //UploadedPdfFile.SaveAs(destinationFileLocation);
                            ////db.SellNoteAttachments.Add(sellNotesAttachment);
                            //db.SaveChanges();

                        }
                        catch (Exception)
                        {
                            throw;
                        }


                        db.SaveChanges();
                        int id = Convert.ToInt32(Session["ID"]);
                        User SendMail = db.Users.Where(x => x.ID == id).FirstOrDefault();

                        // Mail Sending Code
                        var sender = new MailAddress(ConstantStrings.supportEmail, ConstantStrings.supportName);
                        var receiver = new MailAddress("sedkeha1234@gmail.com", "Admin");
                        var password = ConstantStrings.supportPassword;
                        var body = string.Empty;
                        var subject = SendMail.FirstName + " sent his note for review";

                        //string URL = ConfigurationManager.AppSettings["Localhost"] + "Account/VefiryEmail?Email=" + user.EmailID + "";
                        //  string New_URL = ConfigurationManager.AppSettings["Localhost"] + Url.Action("VefiryEmail", "Account", new { Email = model.Email });
                        // verify link problem on email 

                        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/PublishNoteNotification.html")))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{SellerName}", SendMail.FirstName);
                        body = body.Replace("{NoteTitle}", addSellNoteDetail.Title);

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(sender.Address, password)
                        };

                        using (var messege = new MailMessage(sender, receiver)
                        {
                            Body = body,
                            Subject = subject,
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(messege);

                        }

                    }
                }
            return RedirectToAction("Index", "Dashboard");

        }


        public static class FileSizeFormatter
        {
            // Load all suffixes in an array  
            static readonly string[] suffixes =
            { "Bytes", "KB", "MB", "GB", "TB", "PB" };
            public static string FormatSize(Int64 bytes)
            {
                int counter = 0;
                decimal number = (decimal)bytes;
                while (Math.Round(number / 1024) >= 1)
                {
                    number = number / 1024;
                    counter++;
                }
                return string.Format("{0:n1} {1}", number, suffixes[counter]);
            }
        }
        public String CheckifPathExistForCurrentUser(int NotesID)
        {
            // Check Path for specific user exist or not. If not exists then create
            var pathofUploadImage = Server.MapPath(ConstantStrings.Notes_Upload_Path + Session["ID"].ToString() + "/" + NotesID.ToString() + "/");
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
            var pathofUploadImage = Server.MapPath(ConstantStrings.Notes_Upload_Path + Session["ID"].ToString() + "/" + NotesID.ToString() + "/" + ConstantStrings.Note_Attachment_Path);
            if (!System.IO.File.Exists(pathofUploadImage))
            {
                DirectorySecurity sec = new DirectorySecurity();
                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                Directory.CreateDirectory(pathofUploadImage, sec);
            }
            return pathofUploadImage;
        }

        public ActionResult NoteDetail(int? noteId)
        {
            NoteDetailViewModel model = new NoteDetailViewModel();

            model.sellNote = db.SellNotes.Where(x => x.ID == noteId).FirstOrDefault();
            model.noteReviews = db.NoteReviews.Where(x => x.NoteID == noteId).ToList();
            model.ReviewCount = model.noteReviews.Count();
            model.ReportCount = db.NoteReports.Where(x => x.NoteID == noteId).Count();
            if (model.ReviewCount != 0)
            {
                model.Rating = (decimal)db.NoteReviews.Where(x => x.NoteID == noteId).Select(x => x.Ratings).Average();
            }
            else
            {
                model.Rating = 0;
            }
            return View(model);
        }

        public ActionResult DownloadNote(int? noteId)
        {
            int DownloaderID = Convert.ToInt32(Session["ID"]);
            SellNote noteISPaidOrNot = db.SellNotes.Where(x => x.ID == noteId).FirstOrDefault();
            bool paidOrNot = noteISPaidOrNot.IsPaid;
            var noteAttachment = db.SellNoteAttachments.Where(x => x.NoteID == noteISPaidOrNot.ID).ToList();
            var filePath = ConstantStrings.Notes_Upload_Path + noteISPaidOrNot.SellerID.ToString() + "/" + noteISPaidOrNot.ID.ToString() + ConstantStrings.Note_Attachment_Path;/*+ noteAttachment.FileName;*/
            if(noteISPaidOrNot.SellerID != DownloaderID)
            {
                var DownloadEntryExistForPaidAndFreeBoth = db.Downloads.Where(x => x.Downloader == DownloaderID && x.NoteID == noteId 
                                                           && x.IsSellerHasAllowedDownloads == true).FirstOrDefault();
                if(DownloadEntryExistForPaidAndFreeBoth == null)
                {
                    if (paidOrNot == true)
                    {
                        // Buyer Request has one entry for this
                        Download data = new Download()
                        {
                            NoteID = noteISPaidOrNot.ID,
                            Seller = noteISPaidOrNot.SellerID,
                            Downloader = DownloaderID,
                            IsSellerHasAllowedDownloads = false,
                            //AttachmentDownloadDate = DateTime.Now,
                            AttachmentPath = filePath,
                            IsAttachmentDownloaded = false,
                            IsPaid = true,
                            PurchasedPrice = noteISPaidOrNot.SellingPrice,
                            NoteTitle = noteISPaidOrNot.Title,
                            NoteCategory = noteISPaidOrNot.NoteCategory.Name,
                            CreatedDate = DateTime.Now,
                        };
                        db.Downloads.Add(data);
                        db.SaveChanges();
                        data.CreatedBy = DownloaderID;
                        db.SaveChanges();

                        User SendMail = db.Users.Where(x => x.ID == DownloaderID).FirstOrDefault();

                        // Email sending
                        var sender = new MailAddress(ConstantStrings.supportEmail, ConstantStrings.supportName);
                        var receiver = new MailAddress(noteISPaidOrNot.User.EmailID, noteISPaidOrNot.User.FirstName);
                        var password = ConstantStrings.supportPassword;
                        var body = string.Empty;
                        var subject = SendMail.FirstName + " wants to purchase your notes";

                        //string URL = ConfigurationManager.AppSettings["Localhost"] + "Account/VefiryEmail?Email=" + user.EmailID + "";
                        //  string New_URL = ConfigurationManager.AppSettings["Localhost"] + Url.Action("VefiryEmail", "Account", new { Email = model.Email });
                        // verify link problem on email 

                        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/DownloadPaidNotesNotification.html")))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{SellerName}", noteISPaidOrNot.User.FirstName);
                        body = body.Replace("{BuyerName}", SendMail.FirstName);


                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(sender.Address, password)
                        };

                        using (var messege = new MailMessage(sender, receiver)
                        {
                            Body = body,
                            Subject = subject,
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(messege);

                        }

                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        //below two line of code is for single attachment

                        //string fullName = Server.MapPath(filePath);
                        //byte[] fileBytes = GetFile(fullName);

                        var outputStream = new MemoryStream();

                        using (var zip = new ZipFile())
                        {
                            zip.AddDirectory(Server.MapPath(filePath));
                            zip.Save(outputStream);
                        }

                        outputStream.Position = 0;

                        //Entry in Downloads table for Downloader
                        Download data = new Download()
                        {
                            NoteID = noteISPaidOrNot.ID,
                            Seller = noteISPaidOrNot.SellerID,
                            Downloader = DownloaderID,
                            IsSellerHasAllowedDownloads = true,
                            AttachmentDownloadDate = DateTime.Now,
                            AttachmentPath = filePath,
                            IsAttachmentDownloaded = true,
                            IsPaid = false,
                            PurchasedPrice = 0,
                            NoteTitle = noteISPaidOrNot.Title,
                            NoteCategory = noteISPaidOrNot.NoteCategory.Name,
                            CreatedDate = DateTime.Now,
                        };
                        db.Downloads.Add(data);
                        db.SaveChanges();
                        data.CreatedBy = DownloaderID;
                        db.SaveChanges();

                        //return File(
                        //    fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);


                        return File(outputStream.ToArray(), "application/zip", noteISPaidOrNot.Title + ".zip");

                    }
                }
                else 
                {
                    var outputStream = new MemoryStream();

                    using (var zip = new ZipFile())
                    {
                        zip.AddDirectory(Server.MapPath(filePath));
                        zip.Save(outputStream);
                    }

                    outputStream.Position = 0;
                    DownloadEntryExistForPaidAndFreeBoth.IsAttachmentDownloaded = true;
                    DownloadEntryExistForPaidAndFreeBoth.AttachmentDownloadDate = DateTime.Now;
                    db.SaveChanges();
                    return File(outputStream.ToArray(), "application/zip", noteISPaidOrNot.Title + ".zip");
                }
               
            }
            else
            {
                var outputStream = new MemoryStream();

                using (var zip = new ZipFile())
                {
                    zip.AddDirectory(Server.MapPath(filePath));
                    zip.Save(outputStream);
                }

                outputStream.Position = 0;
                return File(outputStream.ToArray(), "application/zip", noteISPaidOrNot.Title + ".zip");
            }
            //return RedirectToAction("Index", "Dashboard");
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

        public ActionResult DownloadNoteHasEntry(int? DownloadTableID)
        {
            int downloderid = Convert.ToInt32(Session["ID"]);
            Download data = db.Downloads.Where(x => x.ID == DownloadTableID).FirstOrDefault();
            if(data.Seller != downloderid)
            {
                if (data.IsSellerHasAllowedDownloads == true)
                {
                    //string fullName = Server.MapPath(data.AttachmentPath);
                    //byte[] fileBytes = GetFile(fullName);

                    var outputStream = new MemoryStream();

                    using (var zip = new ZipFile())
                    {
                        zip.AddDirectory(Server.MapPath(data.AttachmentPath));
                        zip.Save(outputStream);
                    }

                    outputStream.Position = 0;

                    data.AttachmentDownloadDate = DateTime.Now;
                    data.IsAttachmentDownloaded = true;
                    db.SaveChanges();

                    
                    return File(outputStream.ToArray(), "application/zip", data.NoteTitle + ".zip");
                    //return File(
                    //    fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, data.AttachmentPath);
                }
                else
                {
                    return RedirectToAction("MyDownloads", "Profile");
                }
            }
            else
            {
                var outputStream = new MemoryStream();

                using (var zip = new ZipFile())
                {
                    zip.AddDirectory(Server.MapPath(data.AttachmentPath));
                    zip.Save(outputStream);
                }

                outputStream.Position = 0;
                return File(outputStream.ToArray(), "application/zip", data.NoteTitle + ".zip");
            }
            

        }

        public ActionResult PaymentReceived(int? noteId)
        {
            Download data = db.Downloads.Where(x => x.ID == noteId).FirstOrDefault();
            data.IsSellerHasAllowedDownloads = true;
            db.SaveChanges();

            // Email sending
            var sender = new MailAddress(ConstantStrings.supportEmail, ConstantStrings.supportName);
            var receiver = new MailAddress(data.User1.EmailID, data.User1.FirstName);
            var password = ConstantStrings.supportPassword;
            var body = string.Empty;
            var subject = data.User.FirstName + " Allows you to download a note ";

            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/AllowDownloadEmail.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{SellerName}", data.User.FirstName);
            body = body.Replace("{BuyerName}", data.User1.FirstName);


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sender.Address, password)
            };

            using (var messege = new MailMessage(sender, receiver)
            {
                Body = body,
                Subject = subject,
                IsBodyHtml = true
            })
            {
                smtp.Send(messege);

            }

            return RedirectToAction("BuyerRequest", "Home");
        }

        public ActionResult DeleteNote(int? noteId)
        {
            SellNote DeleteNote = db.SellNotes.Where(x => x.ID == noteId).FirstOrDefault();
            SellNoteAttachment deleteAttachment = db.SellNoteAttachments.Where(x => x.NoteID == noteId).FirstOrDefault();

            if (DeleteNote != null)
            {
                db.SellNotes.Remove(DeleteNote);
            }
            if (deleteAttachment != null)
            {
                db.SellNoteAttachments.Remove(deleteAttachment);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}