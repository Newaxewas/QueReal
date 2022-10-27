using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QueReal.DAL.EF.ModelConfigs
{
    public class QuestItemModelConfig : IEntityTypeConfiguration<QuestItemModel>
    {
        public void Configure(EntityTypeBuilder<QuestItemModel> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(ModelConstants.Quest_Title_MaxLength)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(ModelConstants.QuestItem_Description_MaxLength)
                .IsRequired();
        }
    }
}
