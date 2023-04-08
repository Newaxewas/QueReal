using System.ComponentModel.DataAnnotations;

namespace QueReal.PL.Models.Quest
{
    public class QuestItemCreateRequest
    {
        [Required]
        [MaxLength(ModelConstants.QuestItem_Title_MaxLength), MinLength(ModelConstants.QuestItem_Title_MinLength)]
        public string Title { get; set; }
    }
}
