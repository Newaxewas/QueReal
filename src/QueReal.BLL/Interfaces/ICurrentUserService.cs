using System.Security.Claims;

namespace QueReal.BLL.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }

        void Init(ClaimsPrincipal principal);
    }
}
