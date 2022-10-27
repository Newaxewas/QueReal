using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QueReal.PL.Models.Quest;

namespace QueReal.PL.Controllers
{
    public class QuestController : Controller
    {
        private const int pageSize = 10;

        private readonly IQuestService questService;
        private readonly IMapper mapper;

        public QuestController(IQuestService questService, IMapper mapper)
        {
            this.questService = questService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(null);
        }

        [HttpPost]
        public async Task<ActionResult> Create(QuestFormModel questForm)
        {
            if (ModelState.IsValid)
            {
                var quest = mapper.Map<Quest>(questForm);

                var questId = await questService.CreateAsync(quest);

                return RedirectToAction("Details", "Quest", new { questId });
            }

            return View(questForm);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid questId)
        {
            var quest = await questService.GetAsync(questId);

            var questView = mapper.Map<QuestViewModel>(quest);

            return View(questView);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(int pageNumber = 1, int pageSize = pageSize)
        {
            var quests = await questService.GetAllAsync(pageNumber, pageSize);
            var questViews = mapper.Map<IEnumerable<QuestViewModel>>(quests);

            var totalCount = await questService.CountAsync(pageNumber, pageSize);

            var viewModel = new QuestGetAllViewModel 
            { 
                PageNumber = pageNumber,
                PageSize = pageSize,    
                TotalItemCount = totalCount,
                Quests = questViews
            };

            return View(viewModel);
        }
    }
}
