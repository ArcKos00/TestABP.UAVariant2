using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TestTaskParcer.Data.Configurations;
using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<CarModel> Cars { get; set; } = null!;
    public DbSet<CarComplectation> CarsComplectations { get; set; } = null!;
    public DbSet<PartGroup> PartGroups { get; set; } = null!;
    public DbSet<SubGroup> SubGroups { get; set; } = null!;
    public DbSet<Detail> Details { get; set; } = null!;
    public DbSet<Infos> Info { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.UseHiLo();
        builder.ApplyConfiguration(new CarModelConfig());
        builder.ApplyConfiguration(new CarComplectationConfig());
        builder.ApplyConfiguration(new PartGroupConfig());
        builder.ApplyConfiguration(new SubGroupConfig());
        builder.ApplyConfiguration(new DetailConfig());
        builder.ApplyConfiguration(new InfosConfig());
    }
}
