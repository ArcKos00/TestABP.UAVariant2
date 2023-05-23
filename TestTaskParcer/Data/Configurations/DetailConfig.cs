using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Data.Configurations;

public class DetailConfig : IEntityTypeConfiguration<Detail>
{
    public void Configure(EntityTypeBuilder<Detail> builder)
    {
        builder.ToTable("Detail");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasColumnName("TreeCode");
        builder.Property(p => p.Name).HasColumnName("Tree");
        builder.HasMany(m => m.Infos)
            .WithMany()
            .UsingEntity(e => e.ToTable("DetailInfos"));
    }
}
