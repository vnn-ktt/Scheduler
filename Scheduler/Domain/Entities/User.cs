namespace Scheduler.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Provider? Provider { get; set; }
    public ICollection<ExternalAccount> ExternalAccounts { get; set; }
        = new List<ExternalAccount>();
}