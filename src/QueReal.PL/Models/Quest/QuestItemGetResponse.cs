﻿namespace QueReal.PL.Models.Quest
{
    public class QuestItemGetResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public byte Progress { get; set; }
    }
}