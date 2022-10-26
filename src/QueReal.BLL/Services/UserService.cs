using Microsoft.AspNetCore.Identity;

namespace QueReal.BLL.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task SignInAsync(string email, string password, bool remember)
        {
            var user = await userManager.FindByEmailAsync(email);

            await signInManager.PasswordSignInAsync(user, password, remember, lockoutOnFailure: false);
        }

        public Task RegisterAsync(string email, string password)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                RegisterDate = DateTime.UtcNow,
            };

            return userManager.CreateAsync(user, password);
        }

        public Task SignOutAsync()
        {
            return signInManager.SignOutAsync();
        }
    }
}
