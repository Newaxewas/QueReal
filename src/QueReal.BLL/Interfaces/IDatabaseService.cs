namespace QueReal.BLL.Interfaces
{
    public interface IDatabaseService
    {
        Task InitDatabaseAsync();

        Task SaveChangesAsync();
    }
}
