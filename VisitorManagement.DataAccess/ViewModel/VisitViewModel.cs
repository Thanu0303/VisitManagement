using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VisitorManagement.DataAccess.Models;

namespace VisitorManagement.DataAccess.ViewModel
{
    public class VisitViewModel
    {
        public int VisitorId { get; set; }
        [Required]
        public string VisitorName { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime VisitingDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public DateTime? ActualTimeIn { get; set; }
        public DateTime? ActualTimeOut { get; set; }
        public bool IsDeleted { get; set; }
        public int PrimaryParticipant { get; set; }
        public List<int> SecondaryParticipant { get; set; }
        public List<ArrangementViewModel> VisitArrangement { get; set; }
        public List<UserViewModel> Users { get; set; }
        public Visitor visit { get; set; }
        public VisitDetail visitDetails { get; set; }
        public VisitParticipant visitParticipants { get; set; }
        public VisitArrangement visitArrangements { get; set; }

        public int CreatedBy { get; set; }
    }
}



