using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Data.Configurations;

public class InfosConfig : IEntityTypeConfiguration<Infos>
{
    public void Configure(EntityTypeBuilder<Infos> builder)
    {
        builder.ToTable("Infos");
        builder.HasKey(k => k.Id);
    }
}
