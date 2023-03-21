using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities;

public class Role
{
    public Guid Id { get; protected set; }
    public UserRoleType Type { get; protected set; }
    public virtual ICollection<UserRole> UserRoles { get; protected set; }

    protected Role()
    {
    }

    public Role(UserRoleType type)
    {
        Id = Guid.NewGuid();
        Type = type;
        UserRoles = new List<UserRole>();
    }
}