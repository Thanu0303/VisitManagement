using Microsoft.AspNetCore.Http;
using OutLookEvents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using VisitorManagement.DataAccess.Models;
using VisitorManagement.DataAccess.ViewModel;

namespace VisitorManagement.DataAccess
{
    public class DataBaseHelper : IVisitorRepository
    {
        private VisitorManagementContext context;
        OutLookEvents.OutLookEvents outLookEvents = new OutLookEvents.OutLookEvents();

        public DataBaseHelper(VisitorManagementContext dbcontext)
        {
            context = dbcontext;
        }     

        public VisitViewModel AddVisitor(VisitViewModel vmodel)
        {
            context = new VisitorManagementContext();
            try
            {
                Visitor visitor = new Visitor();
                var existingvisitor = context.Visitor.Where(r => r.VisitorName.ToUpper() == vmodel.VisitorName.ToUpper() && r.Designation.ToUpper() == vmodel.Designation.ToUpper() && r.Company.ToUpper() == vmodel.Company.ToUpper() && r.EmailAddress.ToUpper() == vmodel.EmailAddress.ToUpper() && r.Phone.ToUpper() == vmodel.Phone.ToUpper());
                if (existingvisitor.Count() > 0)
                {
                    visitor.VisitorId = existingvisitor.LastOrDefault().VisitorId;
                }
                else
                {                   
                    visitor.VisitorName = vmodel.VisitorName;
                    visitor.Designation = vmodel.Designation;
                    visitor.Company = vmodel.Company;
                    visitor.EmailAddress = vmodel.EmailAddress;
                    visitor.Phone = vmodel.Phone;               
                    visitor.CreatedBy = vmodel.CreatedBy;
                    context.Visitor.Add(visitor);
                    context.SaveChanges();
                }

                VisitDetail visitDetail = new VisitDetail();
                visitDetail.VisitorId = visitor.VisitorId;
                DateTime visitDate = vmodel.VisitingDate;
                DateTime startTime = vmodel.StartTime;
                DateTime endTime = vmodel.EndTime;          
                visitDetail.StartTime = visitDate.Date.Add(startTime.TimeOfDay);
                visitDetail.EndTime = visitDate.Date.Add(endTime.TimeOfDay);           
                visitDetail.CreatedBy = vmodel.CreatedBy;
                context.VisitDetail.Add(visitDetail);
                context.SaveChanges();

                VisitParticipant participant = new VisitParticipant();
                participant.VisitDetailId = visitDetail.VisitDetailId;
                participant.ParticipantId = vmodel.PrimaryParticipant;
                participant.IsPrimary = true;
                context.VisitParticipant.Add(participant);
                context.SaveChanges();

                if (vmodel.SecondaryParticipant != null)
                {
                    foreach (var item in vmodel.SecondaryParticipant)
                    {
                        participant = new VisitParticipant();
                        participant.VisitDetailId = visitDetail.VisitDetailId;
                        participant.ParticipantId = item;
                        participant.IsPrimary = false;
                        context.VisitParticipant.Add(participant);
                    }
                    context.SaveChanges();
                }             
                //
                foreach (var item in vmodel.VisitArrangement.Where(r => r.IsSelected == true))
                {                   
                    VisitArrangement visitArrangement = new VisitArrangement();
                    visitArrangement.ArrangementId = item.ArrangementId;
                    visitArrangement.VisitDetailId = visitDetail.VisitDetailId;
                    visitArrangement.DelegateContactId = item.DelegateContactID;
                    visitArrangement.Description = item.Description;
                    context.VisitArrangement.Add(visitArrangement);
                }
                context.SaveChanges();

                return vmodel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public VisitViewModel CreateGet()
        {
            context = new VisitorManagementContext();


            VisitViewModel visitViewModel = new VisitViewModel();
            VisitDetail visitDetail = new VisitDetail();

            var result = context.Arrangement.ToList();
            Common<Arrangement, ArrangementViewModel> common = new Common<Arrangement, ArrangementViewModel>();
            var resultViewModel = common.List_Source_Target(result);
            VisitArrangement visitArrangement = new VisitArrangement();
            visitArrangement.VisitDetailId = visitDetail.VisitDetailId;
            visitViewModel.VisitArrangement = resultViewModel;

            var participants = context.VisitParticipant.ToList();
            Common<VisitParticipant, ParticipantViewModel> commonParticipants = new Common<VisitParticipant, ParticipantViewModel>();
            var participantViewModel = commonParticipants.List_Source_Target(participants);
            // visitViewModel.VisitParticipant = participantViewModel;


            var user = context.User.ToList();
            Common<User, UserViewModel> commonUser = new Common<User, UserViewModel>();
            visitViewModel.Users = commonUser.List_Source_Target(user);

            return visitViewModel;
        }

        public VisitViewModel EditGet(int id)
        {
            VisitViewModel visitViewModel = new VisitViewModel();

           
            visitViewModel.visitDetails = context.VisitDetail.FirstOrDefault(x => x.VisitDetailId == id);
            visitViewModel.visit = context.Visitor.Find(visitViewModel.visitDetails.VisitorId);

            visitViewModel.PrimaryParticipant = context.VisitParticipant.Where(x => x.VisitDetailId == visitViewModel.visitDetails.VisitDetailId).FirstOrDefault().ParticipantId;
            var secondaryParticipant = context.VisitParticipant.Where(x => x.VisitDetailId == visitViewModel.visitDetails.VisitDetailId && x.IsPrimary == false).ToList();
            List<int> secondaryParticipantIds = new List<int>();
            foreach (var item in secondaryParticipant)
            {
                secondaryParticipantIds.Add(item.ParticipantId);
            }
            visitViewModel.SecondaryParticipant = secondaryParticipantIds;

            VisitDetail visitDetail = new VisitDetail();
            var SaveArrangments = context.VisitArrangement.Where(r => r.VisitDetailId == visitViewModel.visitDetails.VisitDetailId).ToList();
            var result = context.Arrangement.ToList();
            Common<Arrangement, ArrangementViewModel> common = new Common<Arrangement, ArrangementViewModel>();
            var resultViewModel = common.List_Source_Target(result);
            VisitArrangement visitArrangement = new VisitArrangement();
            visitArrangement.VisitDetailId = visitDetail.VisitDetailId;
            visitViewModel.VisitArrangement = resultViewModel;

            

            foreach (ArrangementViewModel arrangement in visitViewModel.VisitArrangement)
            {
                var _visitarrangment = SaveArrangments.Where(r => r.ArrangementId == arrangement.ArrangementId).FirstOrDefault();
                if (_visitarrangment != null)
                {
                    arrangement.IsSelected = true;
                    arrangement.ArrangementId = _visitarrangment.ArrangementId;
                    arrangement.DelegateContactID = _visitarrangment.DelegateContactId;
                    arrangement.Description = _visitarrangment.Description;
                }
            }


            var participants = context.VisitParticipant.ToList();
            Common<VisitParticipant, ParticipantViewModel> commonParticipants = new Common<VisitParticipant, ParticipantViewModel>();
            var participantViewModel = commonParticipants.List_Source_Target(participants);
            VisitParticipant visitParticipant = new VisitParticipant();
            visitParticipant.VisitDetailId = visitDetail.VisitDetailId;


            var user = context.User.ToList();
            Common<User, UserViewModel> commonUser = new Common<User, UserViewModel>();
            visitViewModel.Users = commonUser.List_Source_Target(user);
            return visitViewModel;
        }

        public VisitViewModel EditGetByName(string Name)
        {
            VisitViewModel visitViewModel = new VisitViewModel();

            visitViewModel.visit = context.Visitor.Where(r => r.VisitorName.ToUpper().Trim() == Name.TrimStart().TrimEnd().ToUpper()).LastOrDefault();
            visitViewModel.visitDetails = context.VisitDetail.FirstOrDefault(x => x.VisitorId == visitViewModel.visit.VisitorId);

            visitViewModel.PrimaryParticipant = context.VisitParticipant.Where(x => x.VisitDetailId == visitViewModel.visitDetails.VisitDetailId).FirstOrDefault().ParticipantId;
            var secondaryParticipant = context.VisitParticipant.Where(x => x.VisitDetailId == visitViewModel.visitDetails.VisitDetailId && x.IsPrimary == false).ToList();
            List<int> secondaryParticipantIds = new List<int>();
            foreach (var item in secondaryParticipant)
            {
                secondaryParticipantIds.Add(item.ParticipantId);
            }
            visitViewModel.SecondaryParticipant = secondaryParticipantIds;

            VisitDetail visitDetail = new VisitDetail();
            var SaveArrangments = context.VisitArrangement.Where(r => r.VisitDetailId == visitViewModel.visitDetails.VisitDetailId).ToList();
            var result = context.Arrangement.ToList();
            Common<Arrangement, ArrangementViewModel> common = new Common<Arrangement, ArrangementViewModel>();
            var resultViewModel = common.List_Source_Target(result);
            VisitArrangement visitArrangement = new VisitArrangement();
            visitArrangement.VisitDetailId = visitDetail.VisitDetailId;
            visitViewModel.VisitArrangement = resultViewModel;

            foreach (ArrangementViewModel arrangement in visitViewModel.VisitArrangement)
            {
                var _visitarrangment = SaveArrangments.Where(r => r.ArrangementId == arrangement.ArrangementId).FirstOrDefault();
                if (_visitarrangment != null)
                {
                    arrangement.IsSelected = true;
                    arrangement.ArrangementId = _visitarrangment.ArrangementId;
                    arrangement.DelegateContactID = _visitarrangment.DelegateContactId;
                    arrangement.Description = _visitarrangment.Description;
                }
            }


            var participants = context.VisitParticipant.ToList();
            Common<VisitParticipant, ParticipantViewModel> commonParticipants = new Common<VisitParticipant, ParticipantViewModel>();
            var participantViewModel = commonParticipants.List_Source_Target(participants);
            VisitParticipant visitParticipant = new VisitParticipant();
            visitParticipant.VisitDetailId = visitDetail.VisitDetailId;


            var user = context.User.ToList();
            Common<User, UserViewModel> commonUser = new Common<User, UserViewModel>();
            visitViewModel.Users = commonUser.List_Source_Target(user);
            return visitViewModel;
        }

        public VisitViewModel UpdateVisitor(VisitViewModel updatedModel)
        {
            try
            {
                var visitor = context.Visitor.Where(c => c.VisitorId == updatedModel.visit.VisitorId).FirstOrDefault();
                visitor.VisitorId = updatedModel.visit.VisitorId;
                visitor.VisitorName = updatedModel.VisitorName;
                visitor.Company = updatedModel.Company;
                visitor.Designation = updatedModel.Designation;
                visitor.EmailAddress = updatedModel.EmailAddress;
                visitor.Phone = updatedModel.Phone;
                //visitor.PhotoUrl = updatedModel.PhotoUrl.TrimStart().TrimEnd();
                context.Visitor.Update(visitor);
                context.SaveChanges();

                var visitorDetails = context.VisitDetail.Where(c => c.VisitDetailId == updatedModel.visitDetails.VisitDetailId).FirstOrDefault();
                visitorDetails.VisitorId = visitor.VisitorId;
                DateTime visitDate = updatedModel.VisitingDate;
                DateTime startTime = updatedModel.StartTime;
                DateTime endTime = updatedModel.EndTime;
               
               
                visitorDetails.StartTime = visitDate.Date.Add(startTime.TimeOfDay);
                visitorDetails.EndTime = visitDate.Date.Add(endTime.TimeOfDay);
                context.VisitDetail.Update(visitorDetails);
                context.SaveChanges();

                VisitParticipant participant = new VisitParticipant();
                var visitoPrimaryParticipants = context.VisitParticipant.Where(c => c.VisitDetailId == visitorDetails.VisitDetailId && c.IsPrimary == true).FirstOrDefault();

                if (visitoPrimaryParticipants == null)
                {
                    visitoPrimaryParticipants = new VisitParticipant();
                    visitoPrimaryParticipants.VisitDetailId = visitorDetails.VisitDetailId;
                    visitoPrimaryParticipants.ParticipantId = updatedModel.PrimaryParticipant;
                    visitoPrimaryParticipants.IsPrimary = true;
                    context.VisitParticipant.Add(visitoPrimaryParticipants);
                    context.SaveChanges();
                }
                else
                {
                    visitoPrimaryParticipants.VisitDetailId = visitorDetails.VisitDetailId;
                    visitoPrimaryParticipants.ParticipantId = updatedModel.PrimaryParticipant;
                    visitoPrimaryParticipants.IsPrimary = true;
                    context.VisitParticipant.Update(visitoPrimaryParticipants);
                    context.SaveChanges();
                }

                var visitorSecondaryParticipants = context.VisitParticipant.Where(c => c.VisitDetailId == visitorDetails.VisitDetailId && c.IsPrimary == false).ToList();

                if (visitorSecondaryParticipants != null)
                {
                    foreach (var item in visitorSecondaryParticipants)
                    {
                        context.VisitParticipant.Remove(item);
                    }
                }

                if (updatedModel.SecondaryParticipant != null)
                {
                    foreach (var item in updatedModel.SecondaryParticipant)
                    {
                        participant = new VisitParticipant();
                        participant.VisitDetailId = visitorDetails.VisitDetailId;
                        participant.ParticipantId = item;
                        participant.IsPrimary = false;
                        context.VisitParticipant.Add(participant);
                    }
                    context.SaveChanges();
                }


                var existingarrangments = context.VisitArrangement.Where(m => m.VisitDetailId == visitorDetails.VisitDetailId).ToList();

                foreach (var item in existingarrangments)
                {
                    context.VisitArrangement.Remove(item);
                }
                context.SaveChanges();

                foreach (var item in updatedModel.VisitArrangement.Where(r => r.IsSelected == true))
                {

                    VisitViewModel visitViewModel = new VisitViewModel();
                    VisitArrangement visitArrangement = new VisitArrangement();
                    visitArrangement.ArrangementId = item.ArrangementId;
                    visitArrangement.VisitDetailId = visitorDetails.VisitDetailId;
                    visitArrangement.DelegateContactId = item.DelegateContactID;
                    visitArrangement.Description = item.Description;                   
                    context.VisitArrangement.Add(visitArrangement);
                   
                }
                context.SaveChanges();


                return updatedModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Delete
        public bool DeleteVisitor(int visitDetailId)
        {
            context = new VisitorManagementContext();
           
            try
            {
                //visitDetail = context.VisitDetail.Find(visitDetailId);

                //if (visitDetail != null)
                //{
                //    visitDetail.IsDeleted = true;

                //    context.VisitDetail.Update(visitDetail);

                //    context.SaveChanges();



                context = new VisitorManagementContext();

                var vd = context.VisitDetail.FirstOrDefault(x => x.VisitDetailId == visitDetailId);

                if (vd != null)
                {
                    vd.IsDeleted = true;

                    context.SaveChanges();
                }


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }



        }

        //Gets all the Visit List for Admin
        public List<ListVisitModel> GetVisits()
        {
            context = new VisitorManagementContext();

            var list = context.VisitDetail.ToList();
            List<ListVisitModel> listVisitModel = new List<ListVisitModel>();

            foreach (var item in list)
            {
                ListVisitModel visit = new ListVisitModel();

                visit.VisitorId = item.VisitorId;

                var Visit = context.Visitor.FirstOrDefault(x => x.VisitorId == item.VisitorId);

                visit.VisitorName = Visit.VisitorName;
                visit.Company = Visit.Company;
                visit.Designation = Visit.Designation;
                visit.EmailAddress = Visit.EmailAddress;
                visit.StartTime = item.StartTime;
                visit.EndTime = item.EndTime;
                visit.IsDeleted = item.IsDeleted;
                visit.PhotoUrl = ImageReader(Visit.PhotoUrl);
                visit.VisitDetailId = item.VisitDetailId;
                visit.ActualTimeIn = item.ActualTimeIn;
                visit.ActualTimeOut = item.ActualTimeOut;
                if (context.VisitParticipant.Where(r => r.VisitDetailId == item.VisitDetailId && r.IsPrimary == true).Count() > 0) {

                    int contactPersonId = context.VisitParticipant.Where(r => r.VisitDetailId == item.VisitDetailId && r.IsPrimary == true).FirstOrDefault().ParticipantId;

                    visit.ContactPerson = context.User.Where(r => r.UserID == contactPersonId).FirstOrDefault().FirstName;
                }

              

                visit.IsOwner = true;


                listVisitModel.Add(visit);
            }
            return listVisitModel;
        }

        public HashSet<ListVisitModel> UserVisits(int uID)
        {

            context = new VisitorManagementContext();
            HashSet<ListVisitModel> listVisitModel = new HashSet<ListVisitModel>();


            var participentsRes = context.VisitParticipant.Where(x => x.ParticipantId == uID).ToList();

            var res = context.VisitDetail.Where(x => x.CreatedBy == uID).ToList();

            foreach (var item1 in res)
            {

                if (item1.CreatedBy == uID)
                {

                    if (participentsRes.Where(r => r.VisitDetailId == item1.VisitDetailId).Count() > 0)
                    {
                        var list = participentsRes.Where(r => r.VisitDetailId == item1.VisitDetailId).ToList();
                        foreach (VisitParticipant visitParticipant in list)
                        {
                            participentsRes.Remove(visitParticipant);
                        }
                    }

                    ListVisitModel visit = new ListVisitModel();
                    visit.VisitDetailId = item1.VisitDetailId;
                    visit.VisitorId = item1.VisitorId;

                    var Visit = context.Visitor.FirstOrDefault(x => x.VisitorId == item1.VisitorId);

                    visit.VisitorName = Visit.VisitorName;

                    visit.Company = Visit.Company;
                    visit.Designation = Visit.Designation;
                    visit.EmailAddress = Visit.EmailAddress;

                    visit.StartTime = item1.StartTime;
                    visit.EndTime = item1.EndTime;
                    visit.IsDeleted = item1.IsDeleted;
                    visit.PhotoUrl = ImageReader(Visit.PhotoUrl);
                    visit.VisitDetailId = item1.VisitDetailId;
                    visit.ActualTimeIn = item1.ActualTimeIn;
                    visit.ActualTimeOut = item1.ActualTimeOut;
                    
                    visit.IsOwner = true;
                    if (context.VisitParticipant.Where(r => r.VisitDetailId == item1.VisitDetailId && r.IsPrimary == true).Count() > 0)
                    {

                        int contactPersonId = context.VisitParticipant.Where(r => r.VisitDetailId == item1.VisitDetailId && r.IsPrimary == true).FirstOrDefault().ParticipantId;

                        visit.ContactPerson = context.User.Where(r => r.UserID == contactPersonId).FirstOrDefault().FirstName;
                    }

                    listVisitModel.Add(visit);

                }
            }

            foreach (var item in participentsRes)
            {
                ListVisitModel visit1 = new ListVisitModel();

                visit1.VisitDetailId = item.VisitDetailId;

                var visitDetail = context.VisitDetail.FirstOrDefault(x => x.VisitDetailId == item.VisitDetailId);

                visit1.VisitorId = visitDetail.VisitorId;

                var Visit = context.Visitor.FirstOrDefault(x => x.VisitorId == visitDetail.VisitorId);

                visit1.VisitorName = Visit.VisitorName;

                visit1.Company = Visit.Company;
                visit1.Designation = Visit.Designation;
                visit1.EmailAddress = Visit.EmailAddress;

                visit1.StartTime = visitDetail.StartTime;
                visit1.EndTime = visitDetail.EndTime;
                visit1.PhotoUrl = ImageReader(Visit.PhotoUrl);
                visit1.VisitDetailId = item.VisitDetailId;
                visit1.ActualTimeIn = visitDetail.ActualTimeIn;

                if (context.VisitParticipant.Where(r => r.VisitDetailId == item.VisitDetailId && r.IsPrimary == true).Count() > 0)
                {

                    int contactPersonId = context.VisitParticipant.Where(r => r.VisitDetailId == item.VisitDetailId && r.IsPrimary == true).FirstOrDefault().ParticipantId;

                    visit1.ContactPerson = context.User.Where(r => r.UserID == contactPersonId).FirstOrDefault().FirstName;
                }

                visit1.IsOwner = false;



                listVisitModel.Add(visit1);
            }

            return listVisitModel;
        }

        //Details
        public ViewModel.ListVisitModel GetVisitorById(int id)
        {
            ViewModel.ListVisitModel model = new ViewModel.ListVisitModel();

            List<Arrangement> arrange = new List<Arrangement>();
            model.detail = context.VisitDetail.Where(x => x.VisitDetailId == id).FirstOrDefault();
            string time = string.Format("{0:hh:mm:ss tt}", model.detail.StartTime);

          
            model.visit = context.Visitor.Find(model.detail.VisitorId);
            model.visit.PhotoUrl = ImageReader(model.visit.PhotoUrl);


            var results = context.VisitArrangement.Where(x => x.VisitDetailId == model.detail.VisitDetailId).ToList();
            List<ViewVisitorArrangements> list = new List<ViewVisitorArrangements>();

            OutLookEvents.OutLookEvents outLookEvents = new OutLookEvents.OutLookEvents();

            foreach (var item in results)
            {
                ViewVisitorArrangements view_arrangements = new ViewVisitorArrangements();
                view_arrangements.ArrangementId = item.ArrangementId;
                view_arrangements.Arrangement = context.Arrangement.FirstOrDefault(x => x.ArrangementId == item.ArrangementId);
                view_arrangements.VisitDetail = context.VisitDetail.FirstOrDefault(x => x.VisitDetailId == item.VisitDetailId);
                var result = context.VisitParticipant.Where(x => x.VisitDetailId == item.VisitDetailId).ToList();
                List<ViewVisitorParticipant> participantList = new List<ViewVisitorParticipant>();




                foreach (var item1 in result)
                {
                    ViewVisitorParticipant View_Participant = new ViewVisitorParticipant();
                    //var users = context.User.FirstOrDefault(x => x.UserID == item.DelegateContactId);

                    View_Participant.FirstName = context.User.FirstOrDefault(x => x.UserID == item1.ParticipantId).FirstName;
                    View_Participant.EmailAddress = context.User.FirstOrDefault(x => x.UserID == item1.ParticipantId).EmailAddress;
                    View_Participant.LastName = context.User.FirstOrDefault(x => x.UserID == item1.ParticipantId).LastName;
                    View_Participant.VisitParticipantId = item1.VisitParticipantId;

                    View_Participant.VisitDetailId = item1.VisitDetailId;
                    View_Participant.ParticipantId = item1.ParticipantId;
                    View_Participant.IsPrimary = item1.IsPrimary;
                    // View_Participant.EmailAddress = item1.em
                    participantList.Add(View_Participant);
                    var VisitDetail = context.VisitDetail.Where(x => x.VisitDetailId == View_Participant.VisitDetailId).ToList();


                    view_arrangements.VisitArrangementId = item.VisitArrangementId;
                    view_arrangements.VisitDetailId = item.VisitDetailId;


                }



                model.participant = participantList;

                var users = context.User.FirstOrDefault(x => x.UserID == item.DelegateContactId);
                view_arrangements.FirstName = context.User.FirstOrDefault(x => x.UserID == item.DelegateContactId).FirstName;
                view_arrangements.LastName = context.User.FirstOrDefault(x => x.UserID == item.DelegateContactId).LastName;

                if (users != null)
                {
                    view_arrangements.EmailAddress = users.EmailAddress;
                }
                view_arrangements.Description = item.Description;
                view_arrangements.DelegateContactId = item.DelegateContactId;
                view_arrangements.ArrangementId = item.ArrangementId;
                view_arrangements.VisitArrangementId = item.VisitArrangementId;
                view_arrangements.VisitDetailId = item.VisitDetailId;

                list.Add(view_arrangements);


            }
            model.VisitArrangementsList = list;

            foreach (var item in model.VisitArrangementsList)
            {

                model.arrg = context.Arrangement.FirstOrDefault(x => x.ArrangementId == item.ArrangementId);
                //model.arrg.UserName var results= context.User.FirstOrDefault(x => x.UserId == item.DelegateContactId);
                //  model.EmailAddress = results.EmailAddress;
                //  item.EmailAddress = results.EmailAddress;
                arrange.Add(model.arrg);

                //model.VisitArrangementsList.Add(model.arrangment);
                /*model.arrangementsList.Add(model.arrg)*/
            }
            model.arrangementsList = arrange;





            //List<VisitParticipant> Visitorparticipant = new List<VisitParticipant>();
            //model.detail = context.VisitDetail.FirstOrDefault(x => x.VisitorId == model.visit.VisitorId);
            //model.participant = context.VisitParticipant.Where(x => x.VisitDetailId==model.detail.VisitDetailId).ToList();
            //foreach(var item in model.participant)
            //{
            //    model.participent = context.VisitParticipant.FirstOrDefault(x => x.VisitDetailId == item.VisitDetailId);
            //    VisitParticipant.Add(model.participant);
            //}
            //model.participent = Visitorparticipant;



            //model.arrangment = context.VisitArrangement.FirstOrDefault(x => x.VisitArrangementId == model.VisitArrangeme);
            //model.arrg = context.Arrangement.FirstOrDefault(x => x.ArrangementId == model.arrg.ArrangementId);
            //model.participent = context.VisitParticipant.FirstOrDefault(x => x.ParticipantId == model.detail.VisitDetailId);


            if (model == null)
            {
                // return HttpNotFound();
            }

          
            return model;
        }


        public string ImageReader(string ImagePath)
        {
            string ImageData;
            try
            {
                if (ImagePath != "" & ImagePath != null)
                {
                    Byte[] Image = File.ReadAllBytes(ImagePath.TrimStart().TrimEnd());
                    ImageData = Convert.ToBase64String(Image);
                }
                else
                {
                    Byte[] Image = File.ReadAllBytes(@"C:\Users\thanushrees\Desktop\New folder (2)\DefaultUser.png");
                    ImageData = Convert.ToBase64String(Image);
                }
            }
            catch
            {
                ImageData = string.Empty;
            }
            return ImageData;
        }

        //Edit
        public int  UpdateSigninSignOut(int Visitdetailid , string signIn , string signOut, string photo)
        {
            try
            {
                var visitorDetails = context.VisitDetail.Where(c => c.VisitDetailId == Visitdetailid).FirstOrDefault();
                
                if (string.IsNullOrEmpty(signIn)) {
                
                    visitorDetails.ActualTimeIn = visitorDetails.ActualTimeIn;
                
                }
                else
                {

                 //string[] arr =  signIn.Split(' ')[0].Split('/');
                   // TimeSpan timeSpan = new TimeSpan(4, 06, 36);
                    //   string date = arr[1] + "/" + arr[0] + "/" + arr[2]+" " + signIn.Split(' ')[1]+" "+ signIn.Split(' ')[2];


                    var timeIn = signIn.Split(':');
                    var timeInformat = timeIn[2].Split(' ');
                    int hours = 0;
                    if (timeInformat[1] == "PM"  )
                        
                    {
                        if (int.Parse(timeIn[0]) == 12)
                        {
                            hours = int.Parse(timeIn[0]);
                        }
                        else
                        {
                            hours = (int.Parse(timeIn[0]) + 12);
                        }
                       
                    }
                    TimeSpan timeSpan1 = new TimeSpan(hours, int.Parse(timeIn[1]), int.Parse(timeInformat[0]));

                    var currentTime1 = DateTime.Parse(DateTime.Now.ToString("dd MMM yyyy"));
                    visitorDetails.ActualTimeIn = currentTime1.Add(timeSpan1);


                    //visitorDetails.ActualTimeIn = DateTime.Now.Add(timeSpan);

                   
                }

                if (string.IsNullOrEmpty(signOut))
                {
                    visitorDetails.ActualTimeOut = null;
                }
                else
                {
                    //string[] arr = signOut.Split(' ')[0].Split('/');
                    // string date = arr[1] + "/" + arr[0] + "/" + arr[2] + " " + signOut.Split(' ')[1] + " " + signOut.Split(' ')[2];
                   
                    var time = signOut.Split(':');
                   var timeformat = time[2].Split(' ');
                    int hours = 0;

                    if (timeformat[1] == "PM")
                    {
                        if (int.Parse(time[0]) == 12)
                        {
                            hours = int.Parse(time[0]);
                        }
                        else
                        {
                            hours = (int.Parse(time[0]) + 12);
                        }

                    }
                    TimeSpan timeSpan = new TimeSpan(hours, int.Parse(time[1]), int.Parse(timeformat[0]));

                    var currentTime = DateTime.Parse( DateTime.Now.ToString("dd MMM yyyy"));
                    visitorDetails.ActualTimeOut = currentTime.Add(timeSpan);
                }
               
                context.VisitDetail.Update(visitorDetails);
                 context.SaveChanges();

                var visitor = context.Visitor.Where(r => r.VisitorId == visitorDetails.VisitorId).FirstOrDefault();
                if (!string.IsNullOrEmpty(photo))
                {
                    visitor.PhotoUrl = photo;
                }
               
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
