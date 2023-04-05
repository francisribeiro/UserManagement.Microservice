using UserManagement.Domain.Enums;

namespace UserManagement.Application.Exceptions.Role;

public class RoleInUseException : Exception
{
    public UserRoleType RoleType { get; }

    public RoleInUseException(UserRoleType roleType)
        : base($"The role with the type '{roleType}' is currently in use and cannot be deleted or modified.")
    {
        RoleType = roleType;
    }
}
