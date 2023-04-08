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

        [HttpPost("/create")]
        public async Task<ActionResult<QuestCreateResponse>> Create(QuestCreateRequest request)
        {
            var questCreateDto = mapper.Map<QuestCreateDto>(request);

            var questId = await questService.CreateAsync(questCreateDto);

            var response = new QuestCreateResponse
            {
                QuestId = questId
            };

            return Ok(response);
        }

        [HttpGet("/get")]
        public async Task<ActionResult<QuestGetResponse>> Get(QuestGetRequest request)
        {
            var quest = await questService.GetAsync(request.Id);

            var response = mapper.Map<QuestGetResponse>(quest);

            return Ok(response);
        }

        [HttpPut("/edit")]
        public async Task<ActionResult> Edit(QuestEditRequest request)
        {
            var questEditDto = mapper.Map<QuestEditDto>(request);

            await questService.EditAsync(questEditDto);

            return Ok();
        }

        [HttpDelete("/delete")]
        public async Task<ActionResult> Delete(QuestDeleteRequest request)
        {
            await questService.DeleteAsync(request.Id);

            return Ok();
        }

        [HttpPut("/setProgress")]
        public async Task<ActionResult> SetProgress(QuestSetProgressRequest request)
        {
            await questService.SetProgressAsync(request.QuestItemId, request.Progress);

            return Ok();
        }

        [HttpPost("/approveCompletion")]
        public async Task<ActionResult> ApproveCompletion(QuestApproveCompletionRequest request)
        {
            await questService.ApproveCompletionAsync(request.Id);

            return Ok();
        }

        [HttpGet("/getAll")]
        public async Task<ActionResult<QuestGetAllResponse>> GetAll(QuestGetAllRequest request)
        {
            var quests = await questService.GetAllAsync(request.PageNumber, request.PageSize);
            var questViews = mapper.Map<IEnumerable<QuestGetResponse>>(quests);

            var totalCount = await questService.CountAsync(request.PageNumber, request.PageSize);

            var response = new QuestGetAllResponse
            {
                TotalItemCount = totalCount,
                Quests = questViews
            };

            return Ok(response);
        }
    }
}
