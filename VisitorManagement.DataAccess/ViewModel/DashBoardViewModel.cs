using System.Collections.Generic;

namespace VisitorManagement.DataAccess.ViewModel
{
    public class DashBoardViewModel
    {
       
        public int employees_count { get; set; }     
        public int visits_count { get; set; }
        public int upComingVisits_count { get; set; }
        public int completedVisits_count { get; set; }
        public int plannedVisits_count { get; set; }
        public int unPlannedVisits_count { get; set; }        
        public int primaryParticipant_count { get; set; }
        public int secondaryParticipants_count { get; set; }
        public int perweekvisits_count { get; set; }

    }
}
