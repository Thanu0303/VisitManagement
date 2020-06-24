using System;
using System.Collections.Generic;
using System.Text;
using VisitorManagement.DataAccess.Models;

namespace VisitorManagement.DataAccess.ViewModel
{
    public class ViewVisitorArrangements
    {

        public int VisitArrangementId { get; set; }
        public int VisitDetailId { get; set; }
        public int ArrangementId { get; set; }
        public string Description { get; set; }
        public int? DelegateContactId { get; set; }
        public Arrangement Arrangement { get; set; }
        public VisitDetail VisitDetail { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
