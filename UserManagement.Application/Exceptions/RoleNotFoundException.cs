namespace UserManagement.Application.Exceptions;

public class RoleNotFoundException : Exception
{
    public Guid RoleId { get; }

    public RoleNotFoundException(Guid roleId)
        : base($"Role with ID '{roleId}' was not found.")
    {
        RoleId = roleId;
    }
}