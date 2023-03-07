using AutoMapper;
using QueReal.BLL.DTO.Quest;

namespace QueReal.BLL.Mapper
{
	public class BllProfile : Profile
	{
		public BllProfile()
		{
			CreateQuestMap();
		}

		private void CreateQuestMap()
		{
			CreateMap<QuestCreateDto, Quest>();
			CreateMap<QuestItemCreateDto, QuestItem>();

			CreateMap<QuestEditDto, Quest>();
			CreateMap<QuestEditDto, QuestItem>();
		}
	}
}
