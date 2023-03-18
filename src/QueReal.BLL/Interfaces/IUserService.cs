namespace QueReal.BLL.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string email, string password);

        Task<bool> SignInAsync(string email, string password, bool remember);

        Task SignOutAsync();
    }
}
