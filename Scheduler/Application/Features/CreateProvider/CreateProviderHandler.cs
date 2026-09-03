using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Entities;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Application.Features.Providers.CreateProvider;

public sealed class CreateProviderHandler
{
    private readonly SchedulerDbContext _db;

    public CreateProviderHandler(
        SchedulerDbContext db)
    {
        _db = db;
    }

    public async Task<Provider> HandleAsync(
        CreateProviderCommand command,
        CancellationToken cancellationToken)
    {
        var externalUserId =
            command.TelegramUserId.ToString();

        var account = await _db.ExternalAccounts
            .Include(x => x.User)
            .ThenInclude(x => x.Provider)
            .FirstOrDefaultAsync(
                x =>
                    x.Platform == "telegram" &&
                    x.ExternalUserId == externalUserId,
                cancellationToken
            );

        if (account?.User.Provider is not null)
        {
            return account.User.Provider;
        }

        User user;

        if (account is null)
        {
            user = new User
            {
                Id = Guid.NewGuid(),
                Name = command.DisplayName
            };

            account = new ExternalAccount
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Platform = "telegram",
                ExternalUserId = externalUserId,
                Username = command.TelegramUsername,
                User = user
            };

            _db.Users.Add(user);
            _db.ExternalAccounts.Add(account);
        }
        else
        {
            user = account.User;
        }

        var provider = new Provider
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            DisplayName = command.DisplayName,
            User = user
        };

        _db.Providers.Add(provider);

        await _db.SaveChangesAsync(
            cancellationToken
        );

        return provider;
    }
}