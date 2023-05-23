using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Data.Configurations;

public class CarModelConfig : IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder.ToTable("Model");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).UseHiLo();

        builder.HasMany(m => m.Complectations)
            .WithOne(o => o.Car).HasForeignKey(k => k.CarId);
    }
}
