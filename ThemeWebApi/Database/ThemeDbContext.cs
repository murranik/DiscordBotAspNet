using Microsoft.EntityFrameworkCore;
using ThemeWebApi.Database.Models;

namespace ThemeWebApi.Database;

public class ThemeDbContext : DbContext
{
    public ThemeDbContext() { }
    public ThemeDbContext(DbContextOptions<ThemeDbContext> options) : base(options) { }
    public DbSet<ThemeData> Themes => Set<ThemeData>();
    public DbSet<DataTableCellColors> DataTableCellColors => Set<DataTableCellColors>();
    public DbSet<DropdownButtonColors> DropdownButtonColors => Set<DropdownButtonColors>();
    public DbSet<FloatingBoxColors> FloatingBoxColors => Set<FloatingBoxColors>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ThemeData>()
            .HasKey(x => x.Name);
        modelBuilder.Entity<ThemeData>()
            .HasOne(x => x.DataTableCellColors)
            .WithOne()
            .HasForeignKey<DataTableCellColors>(x => x.ParentName)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ThemeData>()
            .HasOne(x => x.DropdownButtonColors)
            .WithOne()
            .HasForeignKey<DropdownButtonColors>(x => x.ParentName)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ThemeData>()
            .HasOne(x => x.FloatingBoxColors)
            .WithOne()
            .HasForeignKey<FloatingBoxColors>(x => x.ParentName)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DataTableCellColors>()
            .Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<DropdownButtonColors>()
            .Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<FloatingBoxColors>()
            .Property(x => x.Id).ValueGeneratedOnAdd();
    }
}