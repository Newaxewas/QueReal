using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QueReal.DAL.EF.ModelConfigs
{
    public class QuestModelConfig : IEntityTypeConfiguration<QuestModel>
    {
        public void Configure(EntityTypeBuilder<QuestModel> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(ModelConstants.Quest_Title_MaxLength)
                .IsRequired();

            builder.Property(x => x.QuestItems)
                .IsRequired();
        }
    }
}
