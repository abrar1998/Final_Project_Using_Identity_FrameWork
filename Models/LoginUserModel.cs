﻿using System.ComponentModel.DataAnnotations;

namespace FullProjectUsingIdentity.Models
{
    public class LoginUserModel
    {
        [Required(ErrorMessage="Please Enter Your Email")]
        [EmailAddress(ErrorMessage ="Enter Valid Email")]

        public string Email { get; set; }

        [Required(ErrorMessage ="Please Enter Password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
