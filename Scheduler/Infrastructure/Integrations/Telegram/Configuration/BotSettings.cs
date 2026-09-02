namespace Scheduler.Infrastructure.Integrations.Telegram.Configuration;

public sealed class BotSettings
{
    public const string SectionName = "Telegram";
    public required string BotToken { get; init; }
}