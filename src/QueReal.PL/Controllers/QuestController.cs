using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QueReal.BLL.DTO.Quest;
using QueReal.PL.Models.Quest;

namespace QueReal.PL.Controllers
{
    [ApiController, Route("api/quest")]
    public class QuestController : ControllerBase
    {
        private readonly IQuestService questService;
        private readonly IMapper mapper;

        public QuestController(IQuestService questService, IMapper mapper)
        {
            this.questService = questService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(QuestCreateModel questCreateModel)
        {
            var questCreateDto = mapper.Map<QuestCreateDto>(questCreateModel);

            var questId = await questService.CreateAsync(questCreateDto);

            return Ok(questId);
        }

        [HttpGet("{questId}")]
        public async Task<ActionResult<QuestViewModel>> Get(Guid questId)
        {
            var quest = await questService.GetAsync(questId);

            var questViewModel = mapper.Map<QuestViewModel>(quest);

            return Ok(questViewModel);
        }

        [HttpPut("{questId}")]
        public async Task<ActionResult> Edit(Guid questId, QuestEditModel questEditModel)
        {
            var questEditDto = mapper.Map<QuestEditDto>(questEditModel);
            questEditDto.Id = questId;

            await questService.EditAsync(questEditDto);

            return Ok();
        }

        [HttpDelete("{questId}")]
        public async Task<ActionResult> Delete(Guid questId)
        {
            await questService.DeleteAsync(questId);

            return Ok();
        }

        [HttpPut("setProgress")]
        public async Task<ActionResult> SetProgress(QuestSetProgressModel model)
        {
            await questService.SetProgressAsync(model.QuestItemId, model.Progress);

            return Ok();
        }

        [HttpPost("{questId}/approveCompletion")]
        public async Task<ActionResult> ApproveCompletion(Guid questId)
        {
            await questService.ApproveCompletionAsync(questId);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<QuestGetAllViewModel>> GetAll(int pageNumber, int pageSize)
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

            return Ok(viewModel);
        }
    }
}
