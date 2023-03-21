using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Events;

public class UserCreatedEvent : IDomainEvent
{
    public User User { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public UserCreatedEvent(User user)
    {
        User = user;
    }
}