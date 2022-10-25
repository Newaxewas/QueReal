using Microsoft.AspNetCore.Identity;

namespace QueReal.DAL.Models
{
    internal class User : IdentityUser
    {
        public DateTime RegisterDate { get; set; }
    }
}
