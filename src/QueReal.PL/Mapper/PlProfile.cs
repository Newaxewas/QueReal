using System.Linq.Expressions;
using AutoMapper;
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
            CreateMap<QuestFormModel, Quest>();
            CreateMap<QuestItemFormModel, QuestItem>();

            CreateMap<Quest, QuestViewModel>().AddTransform(localizeTime);
            CreateMap<QuestItem, QuestItemViewModel>();
        }
    }
}
