using System.ComponentModel.DataAnnotations;

namespace QueReal.PL.Models.Quest
{
    public class QuestSetProgressRequest
    {
        public Guid QuestItemId { get; set; }

        [Range(ModelConstants.QuestItem_Progress_MinValue, ModelConstants.QuestItem_Progress_MaxValue)]
        public byte Progress { get; set; }
    }
}
