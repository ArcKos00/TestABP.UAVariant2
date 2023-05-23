using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Data.Configurations;

public class PartGroupConfig : IEntityTypeConfiguration<PartGroup>
{
    public void Configure(EntityTypeBuilder<PartGroup> builder)
    {
        builder.ToTable("PartGroup");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).UseHiLo();

        builder.HasMany(m => m.Groups)
            .WithMany()
            .UsingEntity(e => e.ToTable("PartGroupsSubGroups"));
    }
}
