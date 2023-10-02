using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Infrastructure.Data.Configurations;

namespace MyPeople.Services.Images.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Image> Images => Set<Image>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public override int SaveChanges()
    {
        SetProperties();

        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetProperties();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default
    )
    {
        SetProperties();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetProperties();

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ImageConfiguration());
    }

    private void SetProperties()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(
                e =>
                    e.Entity is Entity
                    && (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

        var now = DateTime.Now;

        foreach (var entry in entries)
        {
            ((Entity)entry.Entity).UpdatedAt = now;
            if (entry.State == EntityState.Added)
            {
                ((Entity)entry.Entity).CreatedAt = now;
            }
        }
    }
}
