namespace QueReal.BLL.Interfaces
{
    public interface IDatabaseService
    {
        public Task InitDatabaseAsync();

        public Task SaveChangesAsync();
    }
}
