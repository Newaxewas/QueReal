namespace QueReal.DAL.Models
{
    public class QuestItem : BaseModel
    {
        public string Title { get; set; }

        public short Progress { get; set; } = 0;
    }
}
