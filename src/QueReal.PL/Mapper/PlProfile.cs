using AutoMapper;
using QueReal.BLL.DTO.Quest;
using QueReal.PL.Models.Quest;

namespace QueReal.PL.Mapper
{
    public class PlProfile : Profile
    {
        public PlProfile()
        {
            CreateQuestMap();
        }

        private void CreateQuestMap()
        {
            CreateMap<QuestCreateModel, QuestCreateDto>();
            CreateMap<QuestItemCreateModel, QuestItemCreateDto>();

            CreateMap<Quest, QuestViewModel>();
            CreateMap<QuestItem, QuestItemViewModel>();

            CreateMap<QuestEditModel, QuestEditDto>();
            CreateMap<QuestItemEditModel, QuestItemEditDto>();
        }
    }
}
