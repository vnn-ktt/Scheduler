using Scheduler.Models.Enums;

namespace Scheduler.Models;

public class Booking
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public Guid ProviderId { get; set; }

    public Guid ServiceId { get; set; }

    public DateTime StartsAt { get; set; }

    public BookingStatus Status { get; set; }

    public User Client { get; set; } = null!;

    public Provider Provider { get; set; } = null!;

    public Service Service { get; set; } = null!;
}