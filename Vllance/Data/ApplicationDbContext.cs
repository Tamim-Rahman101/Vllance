using Microsoft.EntityFrameworkCore;

namespace Vllance.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet properties for each model
    public DbSet<Models.Admin> Admins { get; set; }
    public DbSet<Models.User> Users { get; set; }
    public DbSet<Models.Guard> Guards { get; set; }
    public DbSet<Models.Zone> Zones { get; set; }
    public DbSet<Models.Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships with cascade delete behavior

        // Admin -> Zones (One-to-Many)
        modelBuilder.Entity<Models.Admin>()
            .HasMany(a => a.Zones)
            .WithOne(z => z.Admin)
            .HasForeignKey(z => z.AdminId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        // Zone -> Guards (One-to-Many)
        modelBuilder.Entity<Models.Zone>()
            .HasMany(z => z.Guards)
            .WithOne(g => g.Zone)
            .HasForeignKey(g => g.ZoneId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        // User -> Vehicles (One-to-Many)
        modelBuilder.Entity<Models.User>()
            .HasMany(u => u.Vehicles)
            .WithOne(v => v.User)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Allow cascade delete for vehicles

        // Configure indexes for better query performance
        modelBuilder.Entity<Models.Admin>()
            .HasIndex(a => a.Email)
            .IsUnique();

        modelBuilder.Entity<Models.User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Models.Guard>()
            .HasIndex(g => g.Email)
            .IsUnique();

        modelBuilder.Entity<Models.Zone>()
            .HasIndex(z => z.Name);

        modelBuilder.Entity<Models.Vehicle>()
            .HasIndex(v => v.Identifier);
    }
}
