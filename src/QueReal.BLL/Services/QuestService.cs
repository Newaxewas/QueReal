using System.Linq.Expressions;
using QueReal.BLL.Exceptions;
using QueReal.DAL.Models;

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
			return GetQuestAsync(id);
		}

		public Task<IEnumerable<Quest>> GetAllAsync(int pageNumber, int pageSize)
		{
			var skipCount = (pageNumber - 1) * pageSize;

			return repository.GetAllAsync(accessFilterPredicate, OrderByRecentlyUpdated, skipCount, pageSize);
		}

		public async Task EditAsync(Quest quest)
		{
			await CheckAccessUserToQuestAsync(quest.Id);

			await CheckQuestCompletionNotApprovedAsync(quest.Id);

			await repository.UpdateAsync(quest);
		}

		public async Task DeleteAsync(Guid questId)
		{
			await CheckAccessUserToQuestAsync(questId);

			await CheckQuestCompletionNotApprovedAsync(questId);

			var quest = await GetQuestAsync(questId);

			await repository.DeleteAsync(quest);
		}

		public async Task SetProgressAsync(Guid questItemId, short progress)
		{
			var questItem = await itemRepository.GetAsync(questItemId);

			await CheckAccessUserToQuestAsync(questItem.QuestId);

			await CheckQuestCompletionNotApprovedAsync(questItem.QuestId);

			questItem.Progress = progress;
			await itemRepository.UpdateAsync(questItem);

			var quest = await GetQuestAsync(questItem.QuestId);
			quest.UpdateTime = DateTime.UtcNow;
			await repository.UpdateAsync(quest);
		}

		public async Task ApproveCompletionAsync(Guid questId)
		{
			await CheckAccessUserToQuestAsync(questId);

			await CheckQuestCompletionNotApprovedAsync(questId);

			var quest = await GetQuestAsync(questId);

			quest.ApprovedTime = DateTime.UtcNow;

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

		private async Task CheckAccessUserToQuestAsync(Guid questId)
		{
			var quest = await GetQuestAsync(questId);
			var currentUserId = currentUserService.UserId;

			if (currentUserId != quest.CreatorId)
			{
				throw new AccessDeniedException("You are not a creator");
			}
		}

		private async Task CheckQuestCompletionNotApprovedAsync(Guid questId) 
		{
			var quest = await GetQuestAsync(questId);

			if (quest.ApprovedTime != null) 
			{
				throw new BadRequestException("Quest already approved");
			}
		}

		private async Task<Quest> GetQuestAsync(Guid questId)
		{
			var quest = await repository.GetAsync(questId);

			return quest ?? throw new NotFoundException();
		}

	}
}
