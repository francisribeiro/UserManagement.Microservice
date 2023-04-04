namespace UserManagement.Application.Exceptions;

public class PermissionNotFoundException : Exception
{
    public PermissionNotFoundException(Guid permissionId)
        : base($"Permission with ID '{permissionId}' was not found.")
    {
    }
}