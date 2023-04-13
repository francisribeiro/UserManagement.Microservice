using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Contracts;

namespace UserManagement.Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool Authorize(User user, PermissionType requiredPermission)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userPermissions = GetUserPermissions(user);

            return userPermissions.Contains(requiredPermission);
        }

        private static IEnumerable<PermissionType> GetUserPermissions(User user)
        {
            return user.Roles.SelectMany(r => r.Permissions).Select(p => p.Type).Distinct();
        }
    }
}