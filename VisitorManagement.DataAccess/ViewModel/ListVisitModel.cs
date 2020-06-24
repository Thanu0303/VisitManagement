using System;
using System.Collections.Generic;
using System.Text;
using VisitorManagement.DataAccess.Models;


namespace VisitorManagement.DataAccess.ViewModel
{
    public class ListVisitModel
    {

        //public List<Visitor> Visitors { get; set; }

        //public VisitDetail VisitDetails { get; set; }
        public int VisitorId { get; set; }
        public string VisitorName { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string EmailAddress { get; set; }

        public string ContactPerson { get; set; }

        public int VisitDetailId { get; set; }
    
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public bool IsDeleted { get; set; }

        public int PrimaryParticipant { get; set; }

        public string PhotoUrl { get; set; }
        public int CreatedBy { get; set; }

        public int UserID { get; set; }

      public  bool IsOwner { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? ActualTimeIn { get; set; }
        public DateTime? ActualTimeOut { get; set; }




        public Visitor visit { get; set; }
        public VisitDetail detail { get; set; }
        public VisitArrangement arrangment { get; set; }
        public Arrangement arrg { get; set; }
        //  public VisitParticipant participent { get; set; }

        public List<ViewVisitorParticipant> participant { get; set; }


        public List<ViewVisitorArrangements> VisitArrangementsList { get; set; }
        public List<Arrangement> arrangementsList { get; set; }
    }



}
