using System.Linq.Expressions;
using AutoMapper;
using QueReal.BLL.DTO.Quest;
using QueReal.PL.Models.Quest;

namespace QueReal.PL.Mapper
{
    public class PlProfile : Profile
    {
        private static readonly Expression<Func<DateTime, DateTime>> localizeTime 
            = (DateTime time) => time.ToLocalTime();

        public PlProfile()
        {          
            CreateQuestMap();
        }

        private void CreateQuestMap()
        {
            CreateMap<QuestCreateModel, QuestCreateDto>();
            CreateMap<QuestItemCreateModel, QuestItemCreateDto>();

            CreateMap<Quest, QuestViewModel>().AddTransform(localizeTime);
            CreateMap<QuestItem, QuestItemViewModel>();

            CreateMap<Quest, QuestEditModel>();
            CreateMap<QuestItem, QuestItemEditModel>();

            CreateMap<QuestEditModel, QuestEditDto>();
			CreateMap<QuestItemEditModel, QuestItemEditDto>();
		}
    }
}
