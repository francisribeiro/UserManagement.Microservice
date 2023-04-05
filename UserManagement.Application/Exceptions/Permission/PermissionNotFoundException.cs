namespace UserManagement.Application.Exceptions.Permission;

public class PermissionNotFoundException : Exception
{
    public PermissionNotFoundException(Guid permissionId)
        : base($"Permission with ID '{permissionId}' was not found.")
    {
    }
}