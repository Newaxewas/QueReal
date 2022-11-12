using System.Linq.Expressions;

namespace QueReal.BLL.Services
{
	public class QuestService : IQuestService
	{
		private readonly Expression<Func<Quest, bool>> accessFilterPredicate;

		private readonly IRepository<Quest> repository;
		private readonly IRepository<QuestItem> itemRepository;
		private readonly ICurrentUserService currentUserService;

		public QuestService(
			IRepository<Quest> repository,
			IRepository<QuestItem> itemRepository,
			ICurrentUserService currentUserService)
		{
			this.repository = repository;
			this.itemRepository = itemRepository;
			this.currentUserService = currentUserService;

			accessFilterPredicate = quest => quest.CreatorId == currentUserService.UserId;
		}

		public Task<Guid> CreateAsync(Quest questModel)
		{
			questModel.CreatorId = currentUserService.UserId.Value;

			return repository.CreateAsync(questModel);
		}
		public Task<Quest> GetAsync(Guid id)
		{
			return repository.GetAsync(id);
		}

		public Task<IEnumerable<Quest>> GetAllAsync(int pageNumber, int pageSize)
		{
			var skipCount = (pageNumber - 1) * pageSize;

			return repository.GetAllAsync(accessFilterPredicate, OrderByRecentlyUpdated, skipCount, pageSize);
		}

		public Task EditAsync(Quest quest)
		{
			quest.UpdateTime = DateTime.UtcNow;

			return repository.UpdateAsync(quest);
		}

		public async Task DeleteAsync(Guid questId)
		{
			var quest = await repository.GetAsync(questId);

			await repository.DeleteAsync(quest);
		}

		public async Task SetProgress(Guid questItemId, short progress)
		{
			var questItem = await itemRepository.GetAsync(questItemId);
			questItem.Progress = progress;
			await itemRepository.UpdateAsync(questItem);

			var quest = await repository.GetAsync(questItem.QuestId);
			quest.UpdateTime = DateTime.UtcNow;
			await repository.UpdateAsync(quest);
		}

		public Task<int> CountAsync(int pageNumber, int pageSize)
		{
			var skipCount = (pageNumber - 1) * pageSize;

			return repository.CountAsync(accessFilterPredicate, skipCount);
		}

		private static IOrderedQueryable<Quest> OrderByRecentlyUpdated(IQueryable<Quest> quests)
		{
			return quests.OrderByDescending(x => x.UpdateTime);
		}
	}
}
