using Microsoft.AspNetCore.Identity;

namespace QueReal.DAL.Models
{
    internal class User : IdentityUser<Guid>
    {
        public DateTime RegisterDate { get; set; }
    }
}
