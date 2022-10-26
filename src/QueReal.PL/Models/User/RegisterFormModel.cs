﻿using System.ComponentModel.DataAnnotations;

namespace QueReal.PL.Models.User
{
    public class RegisterFormModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(ModelConstants.User_Password_MinLength), MaxLength(ModelConstants.User_Password_MaxLength)]
        public string Password { get; set; }
    }
}
