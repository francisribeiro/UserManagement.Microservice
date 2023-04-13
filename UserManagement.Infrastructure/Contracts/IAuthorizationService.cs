using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Infrastructure.Contracts;

public interface IAuthorizationService
{
    bool Authorize(User user, PermissionType requiredPermission);
}