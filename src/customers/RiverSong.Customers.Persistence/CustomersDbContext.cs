using Microsoft.EntityFrameworkCore;
using RiverSong.Customers.Domain.Entities;
using RiverSong.Shared.Application.Contracts;
using RiverSong.Shared.Domain.Common;

namespace RiverSong.Customers.Persistence;

public sealed class CustomersDbContext : DbContext, IUnitOfWork
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
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State is EntityState.Detached or EntityState.Unchanged)
            {
                continue;
            }

            var changer = await _userAccessor.GetCurrentUserName();
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedBy = changer;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.UpdatedBy = changer;
                    break;
            }
        }
    }
}