using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.DTOs;

/// <summary>
/// Data transfer object for the Role entity.
/// </summary>
public class RoleDto
{
    public Guid Id { get; set; }
    public UserRoleType Type { get; set; }
    public ICollection<PermissionDto> Permissions { get; set; }

    /// <summary>
    /// Initializes a new instance of the RoleDto class using the given Role entity.
    /// </summary>
    /// <param name="role">The Role entity to be converted into a RoleDto.</param>
    public RoleDto(Role role)
    {
        Id = role.Id;
        Type = role.Type;
        Permissions = role.Permissions.Select(p => new PermissionDto(p)).ToList();
    }

    /// <summary>
    /// Initializes a new instance of the RoleDto class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <param name="type">The type of the role.</param>
    /// <param name="permissions">A collection of permissions associated with the role.</param>
    public RoleDto(Guid id, UserRoleType type, ICollection<PermissionDto>? permissions)
    {
        Id = id;
        Type = type;
        Permissions = permissions ?? new List<PermissionDto>();
    }
}