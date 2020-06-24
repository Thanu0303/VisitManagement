using System;
using System.Collections.Generic;
using System.Text;
using VisitorManagement.DataAccess.Models;

namespace VisitorManagement.DataAccess.ViewModel
{
   public class DeleteViewModel
    {
        public Visitor visit { get; set; }
        public VisitDetail detail { get; set; }
        public VisitArrangement arrangment { get; set; }
        public Arrangement arrg { get; set; }
        public VisitParticipant participent { get; set; }

    }
}
