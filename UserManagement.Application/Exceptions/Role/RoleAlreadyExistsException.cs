using UserManagement.Domain.Enums;

namespace UserManagement.Application.Exceptions.Role;

public class RoleAlreadyExistsException : Exception
{
    public UserRoleType RoleType { get; }

    public RoleAlreadyExistsException(UserRoleType roleType)
        : base($"A role with the type '{roleType}' already exists.")
    {
        RoleType = roleType;
    }
}
