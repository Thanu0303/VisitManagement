using System;
using System.Collections.Generic;

namespace VisitorManagement.DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            VisitParticipant = new HashSet<VisitParticipant>();
        }

        public int UserID { get; set; }
        public string UserLogin { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsInactive { get; set; }
        public DateTime? InactivateDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string PreWindows2000Username { get; set; }
        public string Password { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<VisitParticipant> VisitParticipant { get; set; }
    }
}
