using System.Linq.Expressions;
using AutoMapper;
using QueReal.BLL.DTO.Quest;
using QueReal.BLL.Exceptions;

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
            var skipCount = ServiceHelper.CalculateSkipCount(pageNumber, pageSize);

            return repository.GetAllAsync(accessFilterPredicate, OrderByRecentlyUpdated, skipCount, pageSize);
        }

        public async Task EditAsync(QuestEditDto questEditDto)
        {
            var quest = await GetQuestAsync(questEditDto.Id);

            AssertQuestCompletionNotApproved(quest);

            quest.Title = questEditDto.Title;
            quest.QuestItems = GetUpdatedQuestItems(quest.QuestItems, questEditDto.QuestItems);

            await repository.UpdateAsync(quest);
        }

        public async Task DeleteAsync(Guid questId)
        {
            var quest = await GetQuestAsync(questId);

            await repository.DeleteAsync(quest);
        }

        public async Task SetProgressAsync(Guid questItemId, byte progress)
        {
            var questItem = await itemRepository.GetAsync(questItemId);

            ServiceHelper.AssertObjectExists(questItem);

            var quest = await GetQuestAsync(questItem.QuestId);

            AssertQuestCompletionNotApproved(quest);

            questItem.Progress = progress;
            await itemRepository.UpdateAsync(questItem);

            quest.UpdateTime = DateTime.UtcNow;
            await repository.UpdateAsync(quest);
        }

        public async Task ApproveCompletionAsync(Guid questId)
        {
            var quest = await GetQuestAsync(questId);

            AssertQuestCompletionNotApproved(quest);
            AssertAllQuestItemsHaveFullProgress(quest);

            quest.ApprovedTime = DateTime.UtcNow;
            await repository.UpdateAsync(quest);
        }

        public Task<int> CountAsync(int pageNumber, int pageSize)
        {
            int skipCount = ServiceHelper.CalculateSkipCount(pageNumber, pageSize);

            return repository.CountAsync(accessFilterPredicate, skipCount);
        }

        private static IOrderedQueryable<Quest> OrderByRecentlyUpdated(IQueryable<Quest> quests)
        {
            return quests.OrderByDescending(x => x.UpdateTime);
        }

        private void AssertUserHasAccessToQuest(Quest quest)
        {
            var currentUserId = currentUserService.UserId;

            if (currentUserId != quest.CreatorId)
            {
                throw new AccessDeniedException("You are not a creator");
            }
        }

        private static void AssertQuestCompletionNotApproved(Quest quest)
        {
            if (quest.ApprovedTime != null)
            {
                throw new BadRequestException("Quest already approved");
            }
        }

        private static void AssertAllQuestItemsHaveFullProgress(Quest quest)
        {
            if (quest.QuestItems.Any(x => x.Progress != ModelConstants.QuestItem_Progress_MaxValue))
            {
                throw new BadRequestException("At least quest item has not full progress");
            }
        }

        private async Task<Quest> GetQuestAsync(Guid questId)
        {
            var quest = await repository.GetAsync(questId);

            ServiceHelper.AssertObjectExists(quest);
            AssertUserHasAccessToQuest(quest);

            return quest;
        }

        private static List<QuestItem> GetUpdatedQuestItems(IEnumerable<QuestItem> questItems, IEnumerable<QuestItemEditDto> newQuestItemsDtos)
        {
            var questItemsWithNewTitles = questItems
                .Join(
                    newQuestItemsDtos,
                    questItem => questItem.Id,
                    newQuestItemDto => newQuestItemDto.Id,
                    (questItem, newQuestItemDto) => (questItem, newQuestItemDto.Title))
                .Concat(
                    newQuestItemsDtos
                        .Where(x => x.Id == null)
                        .Select(x => ((QuestItem)null, x.Title)));

            var result = new List<QuestItem>();

            foreach (var (questItem, newTitle) in questItemsWithNewTitles)
            {
                var newQuestItem = questItem ?? new QuestItem();

                newQuestItem.Title = newTitle;

                result.Add(newQuestItem);
            }

            return result;
        }

    }
}
