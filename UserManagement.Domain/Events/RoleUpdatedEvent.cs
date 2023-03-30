using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Events;

public class RoleUpdatedEvent: IDomainEvent
{
    public Role Role { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public RoleUpdatedEvent(Role role)
    {
        Role = role;
    }
}