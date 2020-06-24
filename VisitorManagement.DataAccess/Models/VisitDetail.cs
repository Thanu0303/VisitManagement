using System;
using System.Collections.Generic;

namespace VisitorManagement.DataAccess.Models
{
    public partial class VisitDetail
    {
        public VisitDetail()
        {
            VisitArrangement = new HashSet<VisitArrangement>();
            VisitLog = new HashSet<VisitLog>();
        }

        public int VisitDetailId { get; set; }
        public int VisitorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? ActualTimeIn { get; set; }
        public DateTime? ActualTimeOut { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public Visitor Visitor { get; set; }
        public ICollection<VisitArrangement> VisitArrangement { get; set; }
        public ICollection<VisitLog> VisitLog { get; set; }

        public ICollection<VisitParticipant> VisitParticipant { get; set; }
    }
}
