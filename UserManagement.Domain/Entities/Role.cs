using UserManagement.Domain.Enums;
using UserManagement.Domain.Events;

namespace UserManagement.Domain.Entities;

public class Role
{
    public Guid Id { get; protected set; }
    public UserRoleType Type { get; protected set; }
    public virtual ICollection<Permission> Permissions { get; protected set; }
    public List<IDomainEvent> DomainEvents { get; } = new();
    public object Users { get; set; }

    protected Role()
    {
    }

    public Role(UserRoleType type)
    {
        Id = Guid.NewGuid();
        Type = type;
        Permissions = new HashSet<Permission>();
        
        DomainEvents.Add(new RoleCreatedEvent(this));
    }

    public void UpdateRoleType(UserRoleType newRoleType)
    {
        Type = newRoleType;
        DomainEvents.Add(new RoleUpdatedEvent(this));
    }

    public void AddPermission(Permission permission)
    {
        if (permission == null)
            throw new ArgumentNullException(nameof(permission));

        Permissions.Add(permission);
        DomainEvents.Add(new RoleUpdatedEvent(this));
    }

    public void RemovePermission(Permission permission)
    {
        if (permission == null)
            throw new ArgumentNullException(nameof(permission));

        Permissions.Remove(permission);
        DomainEvents.Add(new RoleUpdatedEvent(this));
    }

    public bool HasPermission(PermissionType permissionType)
    {
        return Permissions.Any(p => p.Type == permissionType);
    }
}