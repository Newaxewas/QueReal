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
            CreateMap<QuestCreateRequest, QuestCreateDto>();
            CreateMap<QuestItemCreateRequest, QuestItemCreateDto>();

            CreateMap<Quest, QuestGetResponse>();
            CreateMap<QuestItem, QuestItemGetResponse>();

            CreateMap<QuestEditRequest, QuestEditDto>();
            CreateMap<QuestItemEditRequest, QuestItemEditDto>();
        }
    }
}
