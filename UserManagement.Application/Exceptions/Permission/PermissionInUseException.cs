using System;

namespace UserManagement.Application.Exceptions.Permission;

public class PermissionInUseException : Exception
{
    public PermissionInUseException(Guid permissionId)
        : base($"Permission with ID '{permissionId}' is currently in use and cannot be deleted.")
    {
    }
}