using QueReal.BLL.DTO.Quest;

namespace QueReal.BLL.Interfaces
{
	public interface IQuestService
	{
		Task<Guid> CreateAsync(QuestCreateDto questCreateDto);

		Task<Quest> GetAsync(Guid id);

		Task<IEnumerable<Quest>> GetAllAsync(int pageNumber, int takeCount);

		Task EditAsync(QuestEditDto questEditDto);

		Task DeleteAsync(Guid questId);

		Task SetProgressAsync(Guid questItemId, byte progress);

		Task ApproveCompletionAsync(Guid questId);

		Task<int> CountAsync(int pageNumber, int pageSize);
	}
}
