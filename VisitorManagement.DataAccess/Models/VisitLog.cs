using System;
using System.Collections.Generic;

namespace VisitorManagement.DataAccess.Models
{
    public partial class VisitLog
    {
        public int VisitLogId { get; set; }
        public int VisitDetailId { get; set; }
        public string LogXml { get; set; }
        public string Action { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedDateUtc { get; set; }

        public VisitDetail VisitDetail { get; set; }
    }
}
