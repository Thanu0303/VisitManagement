using System;
using System.Collections.Generic;

namespace VisitorManagement.DataAccess.Models
{
    public partial class VisitParticipant
    {
        public int VisitParticipantId { get; set; }
        public int VisitDetailId { get; set; }
        public int ParticipantId { get; set; }
        public bool IsPrimary { get; set; }

        public User Participant { get; set; }

        public VisitDetail VisitDetail { get; set; }
    }
}
