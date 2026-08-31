using Scheduler.Models.Enums;

namespace Scheduler.Models;

public class ExternalAccount
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ExternalPlatform Platform { get; set; }

    public string ExternalUserId { get; set; } = null!;

    public string? Username { get; set; }

    public User User { get; set; } = null!;
}