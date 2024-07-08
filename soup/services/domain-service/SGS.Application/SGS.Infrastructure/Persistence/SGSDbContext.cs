using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SGS.Common.Domain.Events;
using SGS.Domain.Common.Abstractions;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Domain.IRepositories;
using SGS.Domain.ValueObject;
using SGS.Infrastructure.Common.Data;
using SGS.Infrastructure.Persistence.Repositories;

namespace SGS.Infrastructure.Persistence;

public class SGSDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher = null!;
    private IDbContextTransaction? _transaction;

    /// <summary>
    /// For production and development use
    /// </summary>
    /// <param name="options"></param>
    public SGSDbContext(DbContextOptions<SGSDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    /// <summary>
    /// For migrations
    /// </summary>
    public SGSDbContext(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public SGSDbContext() { }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Kiosk> Kiosks { get; set; }
    public DbSet<PriceList> Prices { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PriceListDetail> PriceListDetail { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfig());
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new PriceListConfig());
        modelBuilder.ApplyConfiguration(new ProductKioskConfig());
        base.OnModelCreating(modelBuilder);
    }
    private Task UpdateAuditableEntities()
    {
        var entries = ChangeTracker.Entries();

        List<EntityEntry> auditableEntries =
            entries is null ? [] :
            entries.Where(entry => entry.Entity is IAuditableEntity).ToList();

        foreach (var entry in auditableEntries)
        {
            if (entry.State == EntityState.Added)
            {
                var createdOnUtcProperty = entry.Entity.GetType().GetProperty("CreatedAt");
                createdOnUtcProperty?.SetValue(entry.Entity, DateTimeOffset.UtcNow, null);
            }

            if (entry.State == EntityState.Modified)
            {
                var modifiedOnUtcProperty = entry.Entity.GetType().GetProperty("ModifiedOn");
                modifiedOnUtcProperty?.SetValue(entry.Entity, DateTimeOffset.UtcNow, null);
            }
        }
        return Task.CompletedTask;
    }

    private Task UpdateSoftDeletableEntities()
    {
        var entries = ChangeTracker.Entries();
        List<EntityEntry> deletableEntries = entries is null ? [] :
            entries.Where(entry => entry.Entity is ISoftDeletableEntity).ToList();

        foreach (var entry in deletableEntries)
        {
            if (entry.State != EntityState.Deleted)
            {
                continue;
            }

            var deletedOn = entry.Entity.GetType().GetProperty("DeletedOn");
            deletedOn?.SetValue(entry.Entity, DateTimeOffset.UtcNow, null);

            var statusActive = entry.Entity.GetType().GetProperty("Status");
            statusActive?.SetValue(entry.Entity, CommonStatuses.DELETED, null);
        }
        return Task.CompletedTask;
    }

    private Task PublishDomainEvents()
    {
        var entities = ChangeTracker.Entries<AggregateRoot>()
             .Select(e => e.Entity)
             .Where(e => e.DomainEvents.Count != 0).ToList();
        List<IDomainEvent> domainEvents = [];

        foreach (var entity in entities)
        {
            domainEvents.AddRange(entity.DomainEvents);

            entity.ClearDomainEvents();
        }

        foreach (var de in domainEvents)
        {
            _ = _publisher.Publish(de).ConfigureAwait(false);
        }

        return Task.CompletedTask;
        // IEnumerable<Task> tasks = domainEvents.Select(domainEvent => _publisher.Publish(domainEvent, cancellationToken));
        // await Task.WhenAll(tasks);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // var connectionForMigration = "Server=54.179.64.70;Database=SmartGate.Dev;User=sa;Password=letmein;";
        var connectionForMigration = "Server=103.107.183.204;Database=kioskdata;User=kiosk-dev;Password=kiosk-dev;";

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(connectionForMigration, ServerVersion.AutoDetect(connectionForMigration));
        }

    }

    // private static DbContextOptions GetOptions(string connectionString)
    // {
    //     try
    //     {
    //         return new DbContextOptionsBuilder()
    //             .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    //             .LogTo(Console.WriteLine, LogLevel.Information)
    //             .EnableSensitiveDataLogging()
    //             .EnableDetailedErrors()
    //             .Options;
    //     }
    //     catch (MySqlException ex)
    //     {
    //         throw new DbCustomException(ex.Message);
    //     }
    // }

    public async Task SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        Task[] tasks = [UpdateAuditableEntities(), UpdateSoftDeletableEntities()];
        await Task.WhenAll(tasks);
        try
        {
            await base.SaveChangesAsync(cancellationToken);
            IsSaved = true;
            _ = PublishDomainEvents();
        }
        catch (DbUpdateException)
        {
            throw;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
    }
    public bool IsSaved { get; private set; }
    public void SetIsSave(bool isSave)
    {
        IsSaved = isSave;
    }
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await this.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_transaction != null)
                await _transaction.CommitAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            if (_transaction != null)
                await _transaction.RollbackAsync(cancellationToken);
            throw;
        }

    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
            await _transaction.RollbackAsync(cancellationToken);
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
    {
        return new Repository<TEntity>(this);
    }

    public bool IsInTransaction => _transaction != null;


}