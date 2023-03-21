using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Events;

public class PasswordResetEvent : IDomainEvent
{
    public User User { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public PasswordResetEvent(User user)
    {
        User = user;
    }
}