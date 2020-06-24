using System;
using System.Collections.Generic;
using System.Text;
using VisitorManagement.DataAccess.Models;

namespace VisitorManagement.DataAccess.ViewModel
{
 

        public class ViewVisitorModel
        {

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

