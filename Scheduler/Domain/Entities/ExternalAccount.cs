namespace Scheduler.Domain.Entities;

public sealed class ExternalAccount
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Platform { get; set; } = null!;
    public string ExternalUserId { get; set; } = null!;
    public string? Username { get; set; }
    public User User { get; set; } = null!;
}
