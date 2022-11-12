using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QueReal.DAL.EF.ModelConfigs
{
    public class QuestConfig : IEntityTypeConfiguration<Quest>
    {
        public void Configure(EntityTypeBuilder<Quest> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(ModelConstants.Quest_Title_MaxLength)
                .IsRequired();

            builder.HasMany(x => x.QuestItems).WithOne(x=> x.Quest).IsRequired();
            builder.HasOne(x => x.Creator).WithMany();

            builder.Navigation(x => x.QuestItems).AutoInclude();
        }
    }
}
