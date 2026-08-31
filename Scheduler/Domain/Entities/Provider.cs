namespace Scheduler.Domain.Entities;
public class Provider
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string DisplayName { get; set; } = null!;
    public User User { get; set; } = null!;
}