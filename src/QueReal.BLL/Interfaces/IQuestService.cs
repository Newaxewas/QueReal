namespace QueReal.BLL.Interfaces
{
    public interface IQuestService
    {
        public Task<Guid> CreateAsync(Quest questModel);

        public Task<Quest> GetAsync(Guid id);

        public Task<IEnumerable<Quest>> GetAllAsync(int pageNumber, int takeCount);

        public Task EditAsync(Quest quest);

        public Task DeleteAsync(Guid questId);

        public Task SetProgress(Guid questId, Guid questItemId, short progress);

        Task<int> CountAsync(int pageNumber, int pageSize);
    }
}
