namespace QueReal.BLL.Interfaces
{
    public interface IQuestService
    {
        public Task<Guid> CreateAsync(Quest questModel);

        public Task<Quest> GetAsync(Guid id);

        public Task<IEnumerable<Quest>> GetAllAsync(int pageNumber, int takeCount);

        Task<int> CountAsync(int pageNumber, int pageSize);
    }
}
