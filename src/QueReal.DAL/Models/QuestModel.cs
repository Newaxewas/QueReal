namespace QueReal.DAL.Models
{
    public class QuestModel
    {
        public string Title { get; set; }

        public IEnumerable<QuestItemModel> QuestItems { get; set; }
    }
}
