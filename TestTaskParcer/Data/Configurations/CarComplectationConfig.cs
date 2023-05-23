using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Data.Configurations;

public class CarComplectationConfig : IEntityTypeConfiguration<CarComplectation>
{
    public void Configure(EntityTypeBuilder<CarComplectation> builder)
    {
        builder.ToTable("Complectation");
        builder.HasKey(k => k.Id);
        builder.HasIndex(i => i.ComplectationId);

        builder.Property(p => p.Id).UseHiLo();

        builder.Property(p => p.Gear).HasColumnName("Gear Shift Type");
        builder.Property(p => p.ATM).HasColumnName("ATM,MTM");
        builder.Property(p => p.Position).HasColumnName("Driver's Position");
        builder.Property(p => p.Doors).HasColumnName("No of Doors");

        builder.HasOne(o => o.Car)
            .WithMany(m => m.Complectations)
            .HasForeignKey(k => k.CarId);
        builder.HasMany(m => m.PartGroups)
            .WithMany()
            .UsingEntity(e => e.ToTable("CarComplectationPartGroups"));
    }
}
