using System.ComponentModel.DataAnnotations;

namespace QueReal.PL.Models.Quest
{
    public class QuestEditRequest
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.Quest_Title_MaxLength), MinLength(ModelConstants.Quest_Title_MinLength)]
        public string Title { get; set; }

        [Required, MinLength(ModelConstants.Quest_QuestItems_MinLength)]
        public List<QuestItemEditRequest> QuestItems { get; set; }
    }
}
