using Microsoft.AspNetCore.Identity;

namespace QueReal.DAL.Models
{
    public class User : IdentityUser<Guid>
    {
        public DateTime RegisterDate { get; set; }
    }
}
