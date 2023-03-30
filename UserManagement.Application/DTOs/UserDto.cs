using UserManagement.Domain.Entities;

namespace UserManagement.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public bool Enabled { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public List<string> Roles { get; private set; }
        public List<string> Permissions { get; private set; }

        public UserDto()
        {
            Roles = new List<string>();
            Permissions = new List<string>();
        }

        public UserDto(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email.Value;
            Enabled = user.Enabled;
            CreatedAt = user.CreatedAt;
            UpdatedAt = user.UpdatedAt;
            LastLogin = user.LastLogin;

            // Create distinct List<string>`s for unique roles and permissions,
            // as permissions may belong to multiple roles.
            Roles = GetDistinctRoles(user);
            Permissions = GetDistinctPermissions(user);
        }

        private static List<string> GetDistinctRoles(User user)
        {
            return user.Roles.Select(role => role.Type.ToString())
                .Distinct()
                .ToList();
        }

        private static List<string> GetDistinctPermissions(User user)
        {
            return user.Roles
                .SelectMany(role => role.Permissions)
                .Where(permission => permission.Enabled)
                .Select(permission => permission.Type.ToString())
                .Distinct()
                .ToList();
        }
    }
}
