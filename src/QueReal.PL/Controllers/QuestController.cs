using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QueReal.BLL.DTO.Quest;
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
		public async Task<ActionResult> Create(QuestCreateModel questCreateModel)
		{
			if (ModelState.IsValid)
			{
				var questCreateDto = mapper.Map<QuestCreateDto>(questCreateModel);

				var questId = await questService.CreateAsync(questCreateDto);

				return RedirectToAction("Details", "Quest", new { questId });
			}

			return View(questCreateModel);
		}

		[HttpGet]
		public async Task<ActionResult> Edit(Guid questId)
		{
			var quest = await questService.GetAsync(questId);

			var questView = mapper.Map<QuestEditModel>(quest);

			return View(questView);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(Guid questId, QuestEditModel questEditModel)
		{
			if (ModelState.IsValid)
			{
				var questEditDto = mapper.Map<QuestEditDto>(questEditModel);
				questEditDto.Id = questId;

				await questService.EditAsync(questEditDto);

				return RedirectToAction("Details", "Quest", new { questId });
			}

			return View(questEditModel);
		}

		[HttpDelete]
		public async Task<ActionResult> Delete(Guid questId)
		{
			await questService.DeleteAsync(questId);

			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> SetProgress([FromBody] QuestSetProgressModel model)
		{
			if (ModelState.IsValid)
			{
				await questService.SetProgressAsync(model.QuestItemId, model.Progress);

				return Ok();
			}

			return BadRequest();
		}

		[HttpGet]
		public async Task<ActionResult> Details(Guid questId)
		{
			var quest = await questService.GetAsync(questId);

			var questView = mapper.Map<QuestViewModel>(quest);

			return View(questView);
		}

		[HttpPut]
		public async Task<ActionResult> ApproveCompletion(Guid questId)
		{
			await questService.ApproveCompletionAsync(questId);

			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult> Index(int pageNumber = 1, int pageSize = pageSize)
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
