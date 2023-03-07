﻿using System.Linq.Expressions;
using AutoMapper;
using QueReal.BLL.DTO.Quest;
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
		private readonly IMapper mapper;

		public QuestService(
			IRepository<Quest> repository,
			IRepository<QuestItem> itemRepository,
			ICurrentUserService currentUserService,
			IMapper mapper)
		{
			this.repository = repository;
			this.itemRepository = itemRepository;
			this.currentUserService = currentUserService;
			this.mapper = mapper;

			accessFilterPredicate = quest => quest.CreatorId == currentUserService.UserId;
		}

		public Task<Guid> CreateAsync(QuestCreateDto questCreateDto)
		{
			var quest = mapper.Map<Quest>(questCreateDto);

			quest.CreatorId = currentUserService.UserId.Value;

			return repository.CreateAsync(quest);
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
			CheckAccessUserToQuest(quest);
			CheckQuestCompletionNotApproved(quest);

			await repository.UpdateAsync(quest);
		}

		public async Task DeleteAsync(Guid questId)
		{	
			var quest = await GetQuestAsync(questId);

			CheckAccessUserToQuest(quest);

			await repository.DeleteAsync(quest);
		}

		public async Task SetProgressAsync(Guid questItemId, short progress)
		{
			var questItem = await itemRepository.GetAsync(questItemId);
			var quest = await GetQuestAsync(questItem.QuestId);

			CheckAccessUserToQuest(quest);
			CheckQuestCompletionNotApproved(quest);

			questItem.Progress = progress;
			await itemRepository.UpdateAsync(questItem);

			quest.UpdateTime = DateTime.UtcNow;
			await repository.UpdateAsync(quest);
		}

		public async Task ApproveCompletionAsync(Guid questId)
		{
			var quest = await GetQuestAsync(questId);

			CheckAccessUserToQuest(quest);
			CheckQuestCompletionNotApproved(quest);

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

		private void CheckAccessUserToQuest(Quest quest)
		{
			var currentUserId = currentUserService.UserId;

			if (currentUserId != quest.CreatorId)
			{
				throw new AccessDeniedException("You are not a creator");
			}
		}

		private void CheckQuestCompletionNotApproved(Quest quest) 
		{
			if (quest.ApprovedTime != null) 
			{
				throw new BadRequestException("Quest already approved");
			}
		}

		private void CheckAllQuestItemsHaveFullProgress(Quest quest) 
		{
				
		}

		private async Task<Quest> GetQuestAsync(Guid questId)
		{
			var quest = await repository.GetAsync(questId);

			return quest ?? throw new NotFoundException();
		}

	}
}
