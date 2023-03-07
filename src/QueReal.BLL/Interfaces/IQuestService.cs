namespace QueReal.BLL.Interfaces
{
	public interface IQuestService
	{
		Task<Guid> CreateAsync(Quest questModel);

		Task<Quest> GetAsync(Guid id);

		Task<IEnumerable<Quest>> GetAllAsync(int pageNumber, int takeCount);

		Task EditAsync(Quest quest);

		Task DeleteAsync(Guid questId);

		Task SetProgressAsync(Guid questItemId, short progress);

		Task ApproveCompletion(Guid questId);

		Task<int> CountAsync(int pageNumber, int pageSize);
	}
}
