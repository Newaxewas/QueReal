﻿using Microsoft.EntityFrameworkCore;
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

            builder.Property(x => x.QuestItems)
                .IsRequired();

            builder.HasMany(x => x.QuestItems).WithOne();
            builder.HasOne(x => x.Creator).WithMany();

            builder.Navigation(x => x.QuestItems).AutoInclude();
        }
    }
}