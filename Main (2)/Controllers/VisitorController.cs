using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VisitorManagement.DataAccess;
using VisitorManagement.DataAccess.Models;
using VisitorManagement.DataAccess.ViewModel;
using static OutLookEvents.OutLookEvents;

namespace Main.Controllers
{
    public class VisitorController : Controller
    {
        private VisitorManagementContext _context;
        private readonly IVisitorRepository repository;
        private OutLookEvents.OutLookEvents outLookEvents = new OutLookEvents.OutLookEvents();

        public VisitorController(IVisitorRepository visitorRepository)
        {
            repository = visitorRepository;
        }

        public List<ListVisitModel> FilterUser(bool isDeleted)
        {
            _context = new VisitorManagementContext();


            HttpContext.Session.GetString("UserID");

            int userid = int.Parse(HttpContext.Session.GetString("UserID"));

            var isAdmin = _context.User
                .SingleOrDefault(m => m.UserID == userid).IsAdmin;

            if (isAdmin == true)
            {
                var vistors = repository.GetVisits();
                if (isDeleted)
                {
                    return vistors;
                }
                else
                {
                    return vistors.Where(x => x.IsDeleted == false).ToList();
                }
            }
            else
            {
                HttpContext.Session.GetString("UserID");

                int userid1 = int.Parse(HttpContext.Session.GetString("UserID"));
                DataBaseHelper dataBaseHelper = new DataBaseHelper(_context);

                var rs = dataBaseHelper.UserVisits(userid1).OrderByDescending(r => r.VisitDetailId);
                if (isDeleted)
                {
                    return rs.ToList();
                }
                else
                {
                    return rs.Where(x => x.IsDeleted == false).ToList();
                }
            }
        }
        //public IActionResult Index()
        //{
        //    _context = new VisitorManagementContext();
        //    HttpContext.Session.GetString("UserID");
        //    int userid = int.Parse(HttpContext.Session.GetString("UserID"));

        //    var isAdmin = _context.User
        //        .SingleOrDefault(m => m.UserID == userid).IsAdmin;

        //    if (isAdmin == true)
        //    {
        //        return View(repository.GetVisits().Where(x => x.IsDeleted == false).OrderByDescending(r => r.VisitDetailId));
        //    }
        //    else
        //    {
        //        int userid1 = int.Parse(HttpContext.Session.GetString("UserID"));

        //        DataBaseHelper dataBaseHelper = new DataBaseHelper(_context);

        //        var rs = dataBaseHelper.UserVisits(userid1).OrderByDescending(r => r.VisitDetailId);
        //        return View(rs.Where(x => x.IsDeleted == false));
        //    }
        //}

        public IActionResult Index(bool? isDeleted = false)
        {
            _context = new VisitorManagementContext();

            HttpContext.Session.GetString("UserID");
            int userid = int.Parse(HttpContext.Session.GetString("UserID"));

            var isAdmin = _context.User
                .SingleOrDefault(m => m.UserID == userid).IsAdmin;

            if (isDeleted == true)
            {
                if (isAdmin == true)
                {
                    var vistors = repository.GetVisits();
                    if (isDeleted == true)
                    {
                        return View(vistors.Where(x => x.IsDeleted == true));
                    }
                    else
                    {
                        return View(vistors.Where(x => x.IsDeleted == false).ToList());
                    }
                }
                else
                {
                    HttpContext.Session.GetString("UserID");
                    int userid1 = int.Parse(HttpContext.Session.GetString("UserID"));
                    DataBaseHelper dataBaseHelper = new DataBaseHelper(_context);
                    var rs = dataBaseHelper.UserVisits(userid1).OrderByDescending(r => r.VisitDetailId);
                    if (isDeleted == false)
                    {
                        return View(rs.ToList());
                    }
                    else
                    {
                        return View(rs.Where(x => x.IsDeleted == false).ToList());
                    }
                }
            }
            if (isAdmin == true)
            {
                return View(repository.GetVisits().Where(x => x.IsDeleted == false).OrderByDescending(r => r.VisitDetailId));
            }
            else
            {
                int userid1 = int.Parse(HttpContext.Session.GetString("UserID"));
                DataBaseHelper dataBaseHelper = new DataBaseHelper(_context);
                var rs = dataBaseHelper.UserVisits(userid1).OrderByDescending(r => r.VisitDetailId);
                return View(rs.Where(x => x.IsDeleted == false));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var viewModel = repository.EditGet(id);
                var res = repository.DeleteVisitor(id);
                //mail notification
            
                TimeSpan ts = viewModel.EndTime - viewModel.StartTime;
                eAppointment appointment = new eAppointment();
                appointment.Subject = "Visit Cancel Notification";
                appointment.Body = "Your visit which has been scheduled on " + viewModel.visitDetails.StartTime.ToShortDateString() + " from " + viewModel.visitDetails.StartTime.ToShortTimeString() + " to " + viewModel.visitDetails.EndTime.ToShortTimeString() + " has been cancelled.";
                appointment.Location = "Canarys Automation";
                appointment.Duration = Convert.ToInt32(ts.TotalMinutes);
                DateTime dt = viewModel.VisitingDate;
                TimeSpan timeSpan = new TimeSpan(viewModel.StartTime.Hour, viewModel.StartTime.Minute, viewModel.StartTime.Second);
                viewModel.VisitingDate = viewModel.VisitingDate.Add(timeSpan);
                appointment.Start = Convert.ToDateTime(viewModel.VisitingDate);

                _context = new VisitorManagementContext();
                List<string> EmailIDs = new List<string>();

                if (viewModel.PrimaryParticipant != 0)
                {
                    string mailid = _context.User.Where(r => r.UserID == viewModel.PrimaryParticipant).FirstOrDefault().EmailAddress;
                    EmailIDs.Add(mailid);
                }
                if (viewModel.SecondaryParticipant != null && viewModel.SecondaryParticipant.Count > 0)
                {
                    for (int i = 0; viewModel.SecondaryParticipant.Count > i; i++)
                    {
                        string EmailAddres = _context.User.Where(r => r.UserID == viewModel.SecondaryParticipant[i]).FirstOrDefault().EmailAddress;
                        EmailIDs.Add(EmailAddres);
                    }
                }
                if (viewModel.VisitArrangement != null && viewModel.VisitArrangement.Where(r => r.IsSelected == true).Count() > 0)
                {
                    for (int i = 0; viewModel.VisitArrangement.Where(r => r.IsSelected == true).Count() > i; i++)
                    {
                        string mailid = _context.User.Where(r => r.UserID == viewModel.VisitArrangement.Where(l => l.IsSelected == true).ElementAt(i).DelegateContactID).FirstOrDefault().EmailAddress;
                        EmailIDs.Add(mailid);
                    }
                }
                EmailIDs.Add(viewModel.visit.EmailAddress);
                appointment.Email = EmailIDs.ToArray();
                appointment.MeetingStatus = Microsoft.Office.Interop.Outlook.OlMeetingStatus.olMeeting;
                outLookEvents.OutLookEvent(appointment, true);
                return Json(res);
            }
            catch (System.Exception)
            {
                //throw ex;
                return Json(false);
            }
        }
        //Details
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            ListVisitModel visitor = repository.GetVisitorById(id);
            if (visitor == null)
            {
                return NotFound();
            }
            return PartialView("DetailsPopUp", visitor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var result = repository.CreateGet();
            return View(result);
        }

        [HttpPost]
        public IActionResult Create(VisitViewModel viewModel, string SecondaryValues, int PrimaryValues)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.CreatedBy = int.Parse(HttpContext.Session.GetString("UserID"));
                    if (!string.IsNullOrEmpty(SecondaryValues))
                    {
                        string[] array = SecondaryValues.Split(",");
                        viewModel.SecondaryParticipant = new List<int>();
                        for (int i = 0; array.Length > i; i++)
                        {
                            viewModel.SecondaryParticipant.Add(Convert.ToInt32(array[i]));
                        }
                    }

                    if (PrimaryValues != 0)
                    {
                        viewModel.PrimaryParticipant = PrimaryValues;
                    }
                    repository.AddVisitor(viewModel);

                    string filepath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "EmailTemplate.html");
                    string template = System.IO.File.ReadAllText(filepath);

                    template = template.Replace("#USERNAME#", " All");
                    template = template.Replace("#LINKTOREPLACE#", "");
                    template = template.Replace("#LINKADDRESS#", "");
                    template = template.Replace("#LINKTEXT#", "");
                    template = template.Replace("#CREATEDUSERNAME#", HttpContext.Session.GetString("UserName"));
                    string body = template;
                    body = body.Replace("#BODY#", "A Visit has been created on " + viewModel.VisitingDate.ToShortDateString() + " from " + viewModel.StartTime.ToShortTimeString() + " to " + viewModel.EndTime.ToShortTimeString() + ". Please be available.");

                    TimeSpan ts = viewModel.EndTime - viewModel.StartTime;
                    eAppointment appointment = new eAppointment();
                    appointment.Subject = "Visit Create Notification";
                    appointment.Body = body;
                    appointment.Location = "Canarys Automation";
                    appointment.Duration = Convert.ToInt32(ts.TotalMinutes);
                    DateTime dt = viewModel.VisitingDate;
                    TimeSpan timeSpan = new TimeSpan(viewModel.StartTime.Hour, viewModel.StartTime.Minute, viewModel.StartTime.Second);
                    viewModel.VisitingDate = viewModel.VisitingDate.Add(timeSpan);
                    appointment.Start = Convert.ToDateTime(viewModel.VisitingDate);

                    _context = new VisitorManagementContext();
                    List<string> EmailIDs = new List<string>();
                    if (viewModel.PrimaryParticipant != 0)
                    {
                        string mailid = _context.User.Where(r => r.UserID == viewModel.PrimaryParticipant).FirstOrDefault().EmailAddress;
                        EmailIDs.Add(mailid);
                    }
                    if (viewModel.SecondaryParticipant != null && viewModel.SecondaryParticipant.Count > 0)
                    {
                        for (int i = 0; viewModel.SecondaryParticipant.Count > i; i++)
                        {
                            string EmailAddres = _context.User.Where(r => r.UserID == viewModel.SecondaryParticipant[i]).FirstOrDefault().EmailAddress;
                            EmailIDs.Add(EmailAddres);
                        }
                    }
                    EmailIDs.Add(viewModel.EmailAddress);
                    appointment.Body = body;
                    appointment.Email = EmailIDs.ToArray();
                    outLookEvents.SMTPEvent(appointment);

                    EmailIDs = new List<string>();
                    if (viewModel.VisitArrangement != null && viewModel.VisitArrangement.Where(r => r.IsSelected == true).Count() > 0)
                    {
                        StringBuilder builder = new StringBuilder();
                        for (int i = 0; viewModel.VisitArrangement.Where(r => r.IsSelected == true).Count() > i; i++)
                        {
                            var arrangment = viewModel.VisitArrangement.Where(r => r.IsSelected == true).ElementAt(i);
                            string arrangmentname = _context.Arrangement.Where(r => r.ArrangementId == arrangment.ArrangementId).FirstOrDefault().Name;
                            var delegatecontact = _context.User.Where(r => r.UserID == viewModel.VisitArrangement.Where(l => l.IsSelected == true).ElementAt(i).DelegateContactID).FirstOrDefault();
                            EmailIDs.Add(delegatecontact.EmailAddress);
                            builder.AppendLine(arrangmentname + " Arrangements " + " for these description " + arrangment.Description + " by " + delegatecontact.FirstName);

                        }
                        body = template;
                        body = body.Replace("#BODY#", "A Visit has been created on " + viewModel.VisitingDate.ToShortDateString() + " from " + viewModel.StartTime.ToShortTimeString() + " to " + viewModel.EndTime.ToShortTimeString() + ". Please be available. <br /> Please Arrange the below items <br />  " + builder.ToString());

                        appointment.Body = body;
                        appointment.Email = EmailIDs.ToArray();
                        appointment.MeetingStatus = OlMeetingStatus.olMeeting;
                        outLookEvents.SMTPEvent(appointment);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
                }

            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = repository.EditGet(id);
            if (id == 0)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind] VisitViewModel editedVisitor, string SecondaryValues, int PrimaryValues)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (SecondaryValues != null)
                    {
                        string[] array = SecondaryValues.Split(",");
                        editedVisitor.SecondaryParticipant = new List<int>();
                        for (int i = 0; array.Length > i; i++)
                        {
                            editedVisitor.SecondaryParticipant.Add(Convert.ToInt32(array[i]));
                        }
                    }

                    //if (PrimaryValues != 0)
                    //{
                    //    editedVisitor.PrimaryParticipant = PrimaryValues;
                    //}

                    var previousresult = repository.EditGet(id);
                    _context = new VisitorManagementContext();



                    string filepath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "EmailTemplate.html");
                    string template = System.IO.File.ReadAllText(filepath);

                    string Bodymsg = string.Empty;

                    template = template.Replace("#USERNAME#", " All");
                    template = template.Replace("#LINKTOREPLACE#", "");
                    template = template.Replace("#LINKADDRESS#", "");
                    template = template.Replace("#LINKTEXT#", "");
                    template = template.Replace("#CREATEDUSERNAME#", HttpContext.Session.GetString("UserName"));
                    string body = template;
                    body = template;
                    if (previousresult.visit.VisitorName != editedVisitor.VisitorName)
                    {

                        Bodymsg += "Visitor Name has been Changed from " + previousresult.visit.VisitorName + " to " + editedVisitor.VisitorName + " .<br/>";
                        Console.WriteLine();
                    }
                    if (previousresult.visit.EmailAddress != editedVisitor.EmailAddress)
                    {
                        Bodymsg += "Visitor EmailAddress has been Changed from " + previousresult.visit.EmailAddress + " to " + editedVisitor.EmailAddress + " .<br/>";
                        Console.WriteLine();



                    }
                    if (previousresult.visit.Company != editedVisitor.Company)
                    {
                        Bodymsg += "Visitor Company has been Changed from " + previousresult.visit.Company + " to " + editedVisitor.Company + " .<br/>";
                        Console.WriteLine();
                    }
                    if (previousresult.visit.Designation != editedVisitor.Designation)
                    {
                        Bodymsg += "Visitor Designation has been Changed from " + previousresult.visit.Designation + " to " + editedVisitor.Designation + " .<br/>";
                        Console.WriteLine();
                    }
                    if (previousresult.visit.Phone != editedVisitor.Phone)
                    {
                        Bodymsg += "Visitor Phone number has been Changed from " + previousresult.visit.Phone + " to " + editedVisitor.Phone + " .<br/>";
                        Console.WriteLine();
                    }



                    if (previousresult.visitDetails.StartTime.ToShortDateString() != editedVisitor.VisitingDate.ToShortDateString())
                    {
                        Bodymsg += "Visit Date has been Changed from " + previousresult.visitDetails.StartTime.ToShortDateString() + " to " + editedVisitor.VisitingDate.ToShortDateString() + " .<br/>";
                        Console.WriteLine();
                    }
                    if (previousresult.visitDetails.StartTime.ToShortTimeString() != editedVisitor.StartTime.ToShortTimeString())
                    {
                        Bodymsg += "Visit Start Time has been Changed from " + previousresult.visitDetails.StartTime.ToShortTimeString() + " to " + editedVisitor.StartTime.ToShortTimeString() + " .<br/>";
                        Console.WriteLine();
                    }
                    if (previousresult.visitDetails.EndTime.ToShortTimeString() != editedVisitor.EndTime.ToShortTimeString())
                    {
                        Bodymsg += "Visit End Time has been Changed from " + previousresult.visitDetails.EndTime.ToShortTimeString() + " to " + editedVisitor.EndTime.ToShortTimeString() + " .<br/>";
                        Console.WriteLine();
                    }


                    for (int i = 0; editedVisitor.VisitArrangement.Count > i; i++)
                    {
                        if (previousresult.VisitArrangement.Where(x => x.ArrangementId == editedVisitor.VisitArrangement[i].ArrangementId).Count() > 0)
                        {
                            var oldvalue = previousresult.VisitArrangement.Where(x => x.ArrangementId == editedVisitor.VisitArrangement[i].ArrangementId).FirstOrDefault();
                            var newvalue = editedVisitor.VisitArrangement[i];
                            if (oldvalue.Description != newvalue.Description)
                            {
                                Bodymsg += oldvalue.Name + " Arrangements has been Changed from " + oldvalue.Description + " to " + newvalue.Description + " .<br/>";
                            }

                            if (oldvalue.DelegateContactID != newvalue.DelegateContactID)
                            {
                                var delegatecontact = _context.User.Where(r => r.UserID == newvalue.DelegateContactID).FirstOrDefault().FirstName;
                                var olddelegatecontact = _context.User.Where(r => r.UserID == oldvalue.DelegateContactID).FirstOrDefault().FirstName;



                                Bodymsg += " Delegate Contact has been changed form " + olddelegatecontact + " to " + delegatecontact + " .<br/>";
                            }
                        }
                    }


                    body = body.Replace("#BODY#", Bodymsg);
                    repository.UpdateVisitor(editedVisitor);



                    TimeSpan ts = editedVisitor.EndTime - editedVisitor.StartTime;
                    eAppointment appointment = new eAppointment();
                    appointment.Subject = "Visit Changes Notification";
                    appointment.Body = body;
                    appointment.Location = "Canarys Automation";
                    appointment.Duration = Convert.ToInt32(ts.TotalMinutes);
                    DateTime dt = editedVisitor.VisitingDate;
                    TimeSpan timeSpan = new TimeSpan(editedVisitor.StartTime.Hour, editedVisitor.StartTime.Minute, editedVisitor.StartTime.Second);
                    editedVisitor.VisitingDate = editedVisitor.VisitingDate.Add(timeSpan);
                    appointment.Start = Convert.ToDateTime(editedVisitor.VisitingDate);



                    _context = new VisitorManagementContext();
                    List<string> EmailIDs = new List<string>();
                    if (editedVisitor.PrimaryParticipant != 0)
                    {
                        string mailid = _context.User.Where(r => r.UserID == editedVisitor.PrimaryParticipant).FirstOrDefault().EmailAddress;
                        EmailIDs.Add(mailid);
                    }
                    if (editedVisitor.SecondaryParticipant != null && editedVisitor.SecondaryParticipant.Count > 0)
                    {
                        for (int i = 0; editedVisitor.SecondaryParticipant.Count > i; i++)
                        {
                            string EmailAddres = _context.User.Where(r => r.UserID == editedVisitor.SecondaryParticipant[i]).FirstOrDefault().EmailAddress;
                            EmailIDs.Add(EmailAddres);
                        }
                    }
                    if (editedVisitor.VisitArrangement != null && editedVisitor.VisitArrangement.Where(r => r.IsSelected == true).Count() > 0)
                    {
                        for (int i = 0; editedVisitor.VisitArrangement.Where(r => r.IsSelected == true).Count() > i; i++)
                        {
                            string mailid = _context.User.Where(r => r.UserID == editedVisitor.VisitArrangement.Where(l => l.IsSelected == true).ElementAt(i).DelegateContactID).FirstOrDefault().EmailAddress;
                            EmailIDs.Add(mailid);
                        }
                    }
                    EmailIDs.Add(editedVisitor.EmailAddress);



                    appointment.Body = body;
                    appointment.Email = EmailIDs.ToArray();
                    appointment.MeetingStatus = OlMeetingStatus.olMeeting;
                    outLookEvents.SMTPEvent(appointment);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to update changes. " +
                "Try again, and if the problem persists, " +
                "see your system administrator.");
                }
            }
            catch (DbUpdateException)
            {



            }
            return View(editedVisitor);
        }

        [HttpPost]
        public ActionResult SingInSignOut(IFormCollection formcollection)
        {
            string photo = formcollection["photo"];
            string Visitordetailid = formcollection["Visitordetailid"];
            string signIn = formcollection["signIn"];
            string signOut = formcollection["signOut"];
            if (!string.IsNullOrEmpty(photo))
            {
                var folderName = Path.Combine("Resources", "VisitorPhotos");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = Visitordetailid + "_Photo.jpg";
                var fullPath = Path.Combine(pathToSave, fileName);
                byte[] data = Convert.FromBase64String(photo.Substring(photo.IndexOf(',') + 1));
                System.IO.File.WriteAllBytes(fullPath, data);
                photo = fullPath;
            }
            repository.UpdateSigninSignOut(Convert.ToInt32(Visitordetailid), signIn, signOut, photo);
            return Json("1");
        }

        [HttpGet]
        public ActionResult GetDetailsByName(string UserName)
        {
            VisitViewModel visitViewModel;
            try
            {
                visitViewModel = repository.EditGetByName(UserName);
            }
            catch
            {
                visitViewModel = new VisitViewModel();
            }
            return Json(visitViewModel);
        }
    }
}
