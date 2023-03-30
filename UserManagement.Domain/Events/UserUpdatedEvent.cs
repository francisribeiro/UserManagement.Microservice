using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Events;

public class UserUpdatedEvent: IDomainEvent
{
    public User User { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public UserUpdatedEvent(User user)
    {
        User = user;
    }
}
