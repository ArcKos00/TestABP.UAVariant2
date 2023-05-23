using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Data.Configurations;

public class SubGroupConfig : IEntityTypeConfiguration<SubGroup>
{
    public void Configure(EntityTypeBuilder<SubGroup> builder)
    {
        builder.ToTable("SubGroup");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).UseHiLo();

        builder.HasMany(m => m.Details)
            .WithMany()
            .UsingEntity(e => e.ToTable("SubGroupDetail"));
    }
}
