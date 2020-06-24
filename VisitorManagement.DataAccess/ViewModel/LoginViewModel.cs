using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VisitorManagement.DataAccess.Models;


namespace VisitorManagement.DataAccess.ViewModel
{
    public class LoginViewModel
    {
       
        public string UserLogin { get; set; }

        public string Password { get; set; }
    }
}
