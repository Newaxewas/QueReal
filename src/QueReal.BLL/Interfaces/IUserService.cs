namespace QueReal.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<bool> RegisterAsync(string email, string password);

        public Task<bool> SignInAsync(string email, string password, bool remember);

        public Task SignOutAsync();
    }
}
