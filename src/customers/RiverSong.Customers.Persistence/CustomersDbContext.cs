using Microsoft.EntityFrameworkCore;
using RiverSong.Customers.Domain.Entities;
using RiverSong.Shared.Application.Contracts;
using RiverSong.Shared.Domain.Common;

namespace RiverSong.Customers.Persistence;

public sealed class CustomersDbContext : DbContext
{
    private readonly IUserAccessor _userAccessor;

    public CustomersDbContext(DbContextOptions<CustomersDbContext> options, IUserAccessor userAccessor) : base(options)
    {
        _userAccessor = userAccessor;

        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Customer> Customers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        await AuditChangedEntities();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private async Task AuditChangedEntities()
    {
        ChangeTracker.DetectChanges();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State is EntityState.Detached or EntityState.Unchanged)
            {
                continue;
            }

            if (entry.Entity is not AuditableEntityBase auditEntity)
            {
                continue;
            }

            var changer = await _userAccessor.GetCurrentUserName();
            if (entry.State == EntityState.Added)
            {
                auditEntity.CreatedAt = DateTimeOffset.UtcNow;
                auditEntity.CreatedBy = changer;
            }
            else
            {
                auditEntity.UpdatedAt = DateTimeOffset.UtcNow;
                auditEntity.UpdatedBy = changer;
            }
        }
    }
}