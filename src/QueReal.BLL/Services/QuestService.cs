namespace QueReal.BLL.Services
{
    public class QuestService : IQuestService
    {
        private readonly IRepository<Quest> repository;
        private readonly ICurrentUserService currentUserService;

        public QuestService(IRepository<Quest> repository, ICurrentUserService currentUserService)
        {
            this.repository = repository;
            this.currentUserService = currentUserService;
        }

        public Task<Guid> CreateAsync(Quest questModel)
        {
            questModel.CreatorId = currentUserService.UserId.Value;

            return repository.CreateAsync(questModel);
        }

        public Task<IEnumerable<Quest>> GetAllAsync(int numberPage, int pageSize)
        {
            var skipCount = (numberPage - 1) * pageSize;

            return repository.GetAllAsync(
                predicate: quest => quest.CreatorId == currentUserService.UserId,
                orderFunc: quests => quests.OrderByDescending(x => x.UpdateTime),
                skipCount, 
                pageSize);
        }

        public Task<Quest> GetAsync(Guid id)
        {
            return repository.GetAsync(quest => quest.Id == id);
        }
    }
}
