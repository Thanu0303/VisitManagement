using System;
using System.Collections.Generic;

namespace VisitorManagement.DataAccess.Models
{
    public partial class VisitArrangement
    {
        public int VisitArrangementId { get; set; }
        public int VisitDetailId { get; set; }
        public int ArrangementId { get; set; }
        public string Description { get; set; }
        public int? DelegateContactId { get; set; }

        public Arrangement Arrangement { get; set; }
        public VisitDetail VisitDetail { get; set; }
    }
}
