using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Events;

public class PasswordChangeEvent: IDomainEvent
{
    public User User { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public PasswordChangeEvent(User user)
    {
        User = user;
    }
}