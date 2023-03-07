namespace QueReal.BLL.DTO.Quest
{
    public class QuestCreateDto
    {
		public string Title { get; set; }

		public IEnumerable<QuestItemCreateDto> QuestItems { get; set; }
	}
}
