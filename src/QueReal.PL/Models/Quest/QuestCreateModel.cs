﻿using System.ComponentModel.DataAnnotations;

namespace QueReal.PL.Models.Quest
{
    public class QuestCreateModel
    {
        [Required]
        [MaxLength(ModelConstants.Quest_Title_MaxLength), MinLength(ModelConstants.Quest_Title_MinLength)]
        public string Title { get; set; }

        [Required, MinLength(ModelConstants.Quest_QuestItems_MinLength)]
        [Display(Name = "Items")]
        public IEnumerable<QuestItemCreateModel> QuestItems { get; set; }
    }
}