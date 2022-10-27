namespace QueReal.PL.Models.Quest
{
    public class QuestViewModel
    {
        public string Title { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public List<QuestItemViewModel> QuestItems { get; set; }
    }
}
