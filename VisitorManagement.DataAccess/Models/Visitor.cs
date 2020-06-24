using System;
using System.Collections.Generic;

namespace VisitorManagement.DataAccess.Models
{
    public partial class Visitor
    {
        public Visitor()
        {
            VisitDetail = new HashSet<VisitDetail>();
        }

        public int VisitorId { get; set; }
        public string VisitorName { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }

        public ICollection<VisitDetail> VisitDetail { get; set; }
    }
}
