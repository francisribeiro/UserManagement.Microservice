namespace UserManagement.Application.Exceptions.Role;

public class RoleNotFoundException : Exception
{
    public Guid RoleId { get; }

    public RoleNotFoundException(Guid roleId)
        : base($"Role with ID '{roleId}' was not found.")
    {
        RoleId = roleId;
    }
}