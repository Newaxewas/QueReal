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

        public Task<bool> RegisterAsync(string email, string password)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                RegisterDate = DateTime.UtcNow,
            };

            return userManager.CreateAsync(user, password).ContinueWith(task => task.Result.Succeeded);
        }

        public async Task<bool> SignInAsync(string email, string password, bool remember)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var result = await signInManager.PasswordSignInAsync(user, password, remember, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public Task SignOutAsync()
        {
            return signInManager.SignOutAsync();
        }
    }
}
