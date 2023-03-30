using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Events;

public class RoleCreatedEvent: IDomainEvent
{
    public Role Role { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public RoleCreatedEvent(Role role)
    {
        Role = role;
    }
}