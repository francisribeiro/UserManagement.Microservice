using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Events;

public class UserLoggedInEvent : IDomainEvent
{
    public User User { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public UserLoggedInEvent(User user)
    {
        User = user;
    }
}