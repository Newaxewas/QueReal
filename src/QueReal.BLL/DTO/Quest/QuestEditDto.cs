namespace QueReal.BLL.DTO.Quest
{
    public class QuestEditDto
    {
		public Guid Id { get; set; }

		public string Title { get; set; }

		public List<QuestItemEditDto> QuestItems { get; set; }
	}
}
