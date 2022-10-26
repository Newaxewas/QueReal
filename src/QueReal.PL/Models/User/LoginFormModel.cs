using System.ComponentModel.DataAnnotations;

namespace QueReal.PL.Models.User
{
    public class LoginFormModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(ModelConstants.User_Password_MinLength), MaxLength(ModelConstants.User_Password_MaxLength)]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
