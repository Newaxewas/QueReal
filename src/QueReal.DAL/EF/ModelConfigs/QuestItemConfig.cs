using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QueReal.DAL.EF.ModelConfigs
{
    public class QuestItemConfig : IEntityTypeConfiguration<QuestItem>
    {
        public void Configure(EntityTypeBuilder<QuestItem> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(ModelConstants.Quest_Title_MaxLength)
                .IsRequired();
        }
    }
}
