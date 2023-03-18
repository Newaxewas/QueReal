using System.Security.Claims;

namespace QueReal.BLL.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        private readonly Lazy<Guid?> lazyUserId;

        private bool isInitted = false;
        private ClaimsPrincipal principal;

        public CurrentUserService()
        {
            lazyUserId = new Lazy<Guid?>(GetUserId);
        }

        public Guid? UserId => isInitted ? lazyUserId.Value : null;

        public void Init(ClaimsPrincipal principal)
        {
            if (!isInitted)
            {
                this.principal = principal;

                isInitted = true;
            }
            else
            {
                throw new InvalidOperationException($"Already initted {nameof(CurrentUserService)}");
            }

        }

        private Guid? GetUserId()
        {
            var userIdString = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return null;
            }

            return Guid.Parse(userIdString);
        }
    }
}
