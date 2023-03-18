using QueReal.PL.Models.Shared;

namespace QueReal.PL.Models.Quest
{
    public class QuestGetAllViewModel : PageSelectorViewModel
    {
        public IEnumerable<QuestViewModel> Quests { get; set; }

        protected override string UrlFormat => "/Quest/GetAll?pageNumber={0}&pageSize={1}";
    }
}
