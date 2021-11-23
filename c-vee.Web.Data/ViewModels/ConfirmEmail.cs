using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace c_vee.Web.Data.ViewModels
{
    public class ConfirmEmail
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
