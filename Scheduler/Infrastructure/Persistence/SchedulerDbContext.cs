using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Entities;

namespace Scheduler.Infrastructure.Persistence;

public sealed class SchedulerDbContext: DbContext
{ 
    public SchedulerDbContext(
        DbContextOptions<SchedulerDbContext> options) : base(options)
    {}
    public DbSet<User> Users => Set<User>();
    public DbSet<Provider> Providers => Set<Provider>();
    public DbSet<ExternalAccount> ExternalAccounts => Set<ExternalAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();
        });

        modelBuilder.Entity<Provider>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.DisplayName)
                .HasMaxLength(200)
                .IsRequired();
        });

        modelBuilder.Entity<ExternalAccount>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => new
            {
                x.Platform,
                x.ExternalUserId
            })
                .IsUnique();

            e.HasOne(x => x.User)
                .WithMany(x => x.ExternalAccounts)
                .HasForeignKey(x => x.UserId);
        });
    }
}