using System;
using System.Collections.Generic;

namespace VisitorManagement.DataAccess.Models
{
    public partial class Arrangement
    {
        public Arrangement()
        {
            VisitArrangement = new HashSet<VisitArrangement>();
        }

        public int ArrangementId { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public ICollection<VisitArrangement> VisitArrangement { get; set; }
    }
}
