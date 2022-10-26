namespace QueReal.BLL.Interfaces
{
    public interface IUserService
    {
        public Task RegisterAsync(string email, string password);

        public Task SignInAsync(string email, string password, bool remember);

        public Task SignOutAsync();
    }
}
