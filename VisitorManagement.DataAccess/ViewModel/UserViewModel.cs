using System;
using System.Collections.Generic;
using System.Text;

namespace VisitorManagement.DataAccess.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDeleted { get; set; }
    
    }
}
