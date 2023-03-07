namespace QueReal.DAL.Models
{
    public class Quest : BaseModel
    {
        public string Title { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public DateTime UpdateTime { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovedTime { get; set; }

        public List<QuestItem> QuestItems { get; set; }

        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
