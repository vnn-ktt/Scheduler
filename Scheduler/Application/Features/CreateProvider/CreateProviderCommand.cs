namespace Scheduler.Application.Features.Providers.CreateProvider;

public sealed record CreateProviderCommand(
    long TelegramUserId,
    string? TelegramUsername,
    string DisplayName
);
