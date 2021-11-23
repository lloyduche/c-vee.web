using System;
using System.Collections.Generic;
using System.Text;

namespace c_vee.Web.Data.ViewModels
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isActive { get; set; }

    }
}
