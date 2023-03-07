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
		public async Task<ActionResult> Create(QuestCreateModel questForm)
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
		public async Task<ActionResult> Edit(Guid questId)
		{
			var quest = await questService.GetAsync(questId);

			var questView = mapper.Map<QuestEditModel>(quest);

			return View(questView);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(Guid questId, QuestEditModel questEdit)
		{
			if (ModelState.IsValid)
			{
				var quest = await questService.GetAsync(questId);

				quest.Title = questEdit.Title;

				var newQuestItemIds = questEdit.QuestItems.Select(x => x.Id).ToHashSet();
				quest.QuestItems = quest.QuestItems.Where(item => newQuestItemIds.Contains(item.Id)).ToList();

				var questItems = quest.QuestItems.ToDictionary(item => item.Id, item => item);
				foreach (var newItem in questEdit.QuestItems)
				{
					if (questItems.TryGetValue(newItem.Id, out var existingItem))
					{
						questItems[newItem.Id].Title = newItem.Title;
					}
					else
					{
						quest.QuestItems.Add(new QuestItem { Title = newItem.Title });
					}
				}

				await questService.EditAsync(quest);

				return RedirectToAction("Details", "Quest", new { questId });
			}

			return View(questEdit);
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
			await questService.SetProgressAsync(model.QuestItemId, model.Progress);

			return Ok();
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
