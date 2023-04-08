namespace QueReal.PL.Models.Quest
{
    public class QuestGetAllResponse
    {
        public int TotalItemCount { get; set; }

        public IEnumerable<QuestGetResponse> Quests { get; set; }
    }
}
