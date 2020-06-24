using System;
using System.Collections.Generic;
using System.Text;

namespace VisitorManagement.DataAccess.ViewModel
{
   public class ViewVisitorParticipant
    {
        public int VisitParticipantId { get; set; }
        public int VisitDetailId { get; set; }
        public int ParticipantId { get; set; }
        public bool IsPrimary { get; set; }

        public string FirstName { get;set;}
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
    }
}
