using UserManagement.Domain.Enums;

namespace UserManagement.Application.Exceptions;
public class PermissionAlreadyExistsException : Exception
{
    public PermissionAlreadyExistsException(string message) : base(message)
    {
    }

    public PermissionAlreadyExistsException(Guid id) : base($"A permission with ID {id} already exists.")
    {
    }

    public PermissionAlreadyExistsException(PermissionType type) : base($"A permission with type {type} already exists.")
    {
    }
}
