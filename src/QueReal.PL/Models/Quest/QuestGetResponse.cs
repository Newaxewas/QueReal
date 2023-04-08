namespace QueReal.PL.Models.Quest
{
    public class QuestGetResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime? ApprovedTime { get; set; }

        public List<QuestItemGetResponse> QuestItems { get; set; }
    }
}
