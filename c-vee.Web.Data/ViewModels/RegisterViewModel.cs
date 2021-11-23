using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace c_vee.Web.Data.ViewModels
{
    public class RegisterViewModel
    {
        [StringLength(maximumLength: 50, ErrorMessage = "The property should have more than one character")]
        public string FirstName { get; set; }


        [StringLength(maximumLength: 50, ErrorMessage = "The property should have more than one character")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 5 to 255 character")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        public string ConfirmPassword { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "First name cannot be more than 50 character")]
        public string JobTitle { get; set; }

        [Phone]
        public string Phone { get; set; }
     
    }
}
