namespace QueReal.DAL.Models
{
    public class QuestItem : BaseModel
    {
        public string Title { get; set; }

        public byte Progress { get; set; } = 0;

        public Guid QuestId { get; set; }
        public Quest Quest { get; set; }
    }
}
