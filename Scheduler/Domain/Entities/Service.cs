namespace Scheduler.Domain.Entities;
public class Service
{
    public Guid Id { get; set; }
    public Guid ProviderId { get; set; }
    public string Name { get; set; } = null!;
    public int DurationMinutes { get; set; }
    public decimal? Price { get; set; }
    public Provider Provider { get; set; } = null!;
}