﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VTBlog.WebApp.Models
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage = "First name is required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [DisplayName("Last Name")]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
